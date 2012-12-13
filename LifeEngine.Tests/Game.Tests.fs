module Game.Tests


open NUnit.Framework
open FsUnit
open LifeEngine

[<TestFixture>]
type ``GameConstruction`` () =

    [<Test>]
    member tests.
     ``Empty Game Can be constructed`` () =
        let g = Game()
        g.Extents |> should equal (0,0)

    [<Test>]
    member tests.
     ``Game can be constructed with strings`` () =
        let g = Game( [|"0000"; "0000"; "0000"|] )
        g.Extents |> should equal (0,0)

    [<Test>]
    member tests.
     ``Extents match maximum with and height of living cells`` () =
        let g = Game ( [|"0010"; "0000"; "0100"; "0000"|] )
        g.Extents |> should equal (2,3)

