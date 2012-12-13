module Rules.Tests

open NUnit.Framework
open FsUnit
open LifeEngine

[<TestFixture>]
type ``RulesTests``() =

    [<Test>]
    member tests.
     ``Living Cells Die with 0 or 1 neighbours``() =
        Rules.NextState Rules.Alive 0 |> should equal Rules.Dead
        Rules.NextState Rules.Alive 1 |> should equal Rules.Dead

    [<Test>]
    member tests.
     ``Living Cells Live with 2 or 3 neighbours`` () =
        Rules.NextState Rules.Alive 2 |> should equal Rules.Alive 
        Rules.NextState Rules.Alive 3 |> should equal Rules.Alive

    [<Test>]
    member tests.
     ``Living Cells Die with >4 neighbours`` () =
        Rules.NextState Rules.Alive 4 |> should equal Rules.Dead
        Rules.NextState Rules.Alive 5 |> should equal Rules.Dead
        Rules.NextState Rules.Alive 10 |> should equal Rules.Dead

    [<Test>]
    member tests.
     ``Dead Cells come to life with 3 neighbours`` () =
        Rules.NextState Rules.Dead 3 |> should equal Rules.Alive

    [<Test>]
    member tests.
     ``Dead Cells stay dead for all other neighbours`` () =
        Rules.NextState Rules.Dead 1 |> should equal Rules.Dead
        Rules.NextState Rules.Dead 2 |> should equal Rules.Dead
        Rules.NextState Rules.Dead 4 |> should equal Rules.Dead
