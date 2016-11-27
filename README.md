# Multi-Way Conditional Expression for F#

Based on a
[request](https://github.com/fsharp/fslang-suggestions/issues/519) for
new F# syntax to support multi-way conditional expressions, this is a
function to implement them in the style of Lisp's and Scheme's cond
macros.

## Semantics

The function checks each boolean expression in the first item of a list
of pairs, and if the boolean expression is true, evaluates to the second
item of that pair. If the boolean is false, it continues with the next
pair in the list. If checking falls through to the end of the list
without finding any matches, it evaluates to the default value given as
the first argument.

Note that the use of quotations ensures lazy evaluation; this is shown
below where a case that can never be reached raises an exception. This
exception will never be thrown at runtime.

## Usage

```fsharp
module Test =
  open Microsoft.FSharp.Quotations
  open Cond.Core

  let input = 10
  let result =
    Cond.cond "Not found"
      <@@ [ input = 0, "Zero"
            input % 2 = 1, "One"
            input * 2 = 20, "Twenty"
            input <> 3, failwith "Not happening" ] @@>
```

Note that the default value must be such a value that its concrete type
can be inferred automatically, or it must be annotated with a concrete
type signature. This is because we are using untyped quotations. E.g.,
`None` has type `'a option`. The quoted version will have type `obj
option` and this will cause a runtime error.

You can see an example annotated default value in the `Tests` project.

## Etc.

Improvements and suggestions are very welcome.

