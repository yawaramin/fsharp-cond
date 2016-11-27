namespace Cond.Tests

open Microsoft.FSharp.Quotations
open NUnit.Framework
open Cond.Core

[<TestFixture>]
module Cond_test =
  [<Test>]
  let cond () =
    let x = 11
    let actual =
      Cond.cond (None : int option)
        <@@ [ x < 5, Some -1
              x < 10, Some 0
              x < 15, Some 1
              x < 20, failwith "!" ] @@>

    Assert.AreEqual(Some 1, actual)
