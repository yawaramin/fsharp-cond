namespace Cond.Core

open FSharp.Quotations.Evaluator
open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.Patterns

module Cond =
  let private is_cons (cons : Reflection.UnionCaseInfo) =
    cons.Name = "Cons" &&
      (let cons_decltype = cons.DeclaringType in
      cons_decltype.IsGenericType &&
      cons_decltype.GetGenericTypeDefinition() = typedefof<_ list>)

  let rec cond (default_val : 'a) = function
    | NewUnionCase (cons, [NewTuple [condition; value]; tail])
      when is_cons cons ->
      if QuotationEvaluator.Evaluate <| Expr.Cast(condition)
        then QuotationEvaluator.Evaluate <| Expr.Cast<'a>(value)
        else cond default_val tail
        
    | _ -> raise <| MatchFailureException ("cond", 0, 0)
