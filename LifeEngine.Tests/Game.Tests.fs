﻿module Game.Tests


open NUnit.Framework
open FsUnit
open LifeEngine

[<TestFixture>]
type ``GameConstruction`` () =

    [<Test>]
    member tests.
     ``Empty Game Can be constructed`` () =
        let g = Game()
        fst g.Extents |> should equal (Coord(0L,0L))
        snd g.Extents |> should equal (Coord(0L,0L))

    [<Test>]
    member tests.
     ``Game can be constructed with strings`` () =
        let g = Game( [|"0000"; "0000"; "0000"|] )
        fst g.Extents |> should equal (Coord(0L,0L))
        snd g.Extents |> should equal (Coord(0L,0L))

    [<Test>]
    member tests.
     ``Extents return top left and bottom right corners of covering square`` () =
        let g = Game ( [|"0010"; "0000"; "0100"; "0000"|] )
        fst g.Extents |> should equal (Coord(1L,0L))
        snd g.Extents |> should equal (Coord(2L,2L))

    [<Test>]
    member tests.
     ``Live Cells are Coords`` () =
        let g = Game ( [|"100"; "010"; "001"|] )
        Seq.length g.LiveCells |> should equal 3
        Seq.exists (fun (coord : Coord) -> coord.X = 0L && coord.Y = 0L) g.LiveCells |> should be True
        Seq.exists (fun (coord : Coord) -> coord.X = 1L && coord.Y = 1L) g.LiveCells |> should be True
        Seq.exists (fun (coord : Coord) -> coord.X = 2L && coord.Y = 2L) g.LiveCells |> should be True

[<TestFixture>]
type ``Game Progression``() =

    [<Test>]
    member tests.
     ``Dead Board spawns dead board``() =
       let g = Game()
       Seq.length g.Next.LiveCells |> should equal 0

    [<Test>]
    member tests.
     ``Propellor init works``() =
       let g = Game( [|"111"|] )
       Seq.exists (fun (coord : Coord) -> coord.X = 0L && coord.Y = 0L) g.LiveCells |> should be True
       Seq.exists (fun (coord : Coord) -> coord.X = 1L && coord.Y = 0L) g.LiveCells |> should be True
       Seq.exists (fun (coord : Coord) -> coord.X = 2L && coord.Y = 0L) g.LiveCells |> should be True

    [<Test>]
    member tests.
     ``Propellor state 1 leads to state 2``() =
        let g = Game( [|"111"|] ).Next
        Seq.length g.LiveCells |> should equal 3
        Seq.exists (fun (coord : Coord) -> coord.X = 1L && coord.Y = -1L) g.LiveCells |> should be True
        Seq.exists (fun (coord : Coord) -> coord.X = 1L && coord.Y = 0L) g.LiveCells |> should be True
        Seq.exists (fun (coord : Coord) -> coord.X = 1L && coord.Y = 1L) g.LiveCells |> should be True

    [<Test>]
    member tests.
     ``Propellor state 2 leads to state 1``() =
       let g = Game( [|"111"|] ).Next.Next
       Seq.exists (fun (coord : Coord) -> coord.X = 0L && coord.Y = 0L) g.LiveCells |> should be True
       Seq.exists (fun (coord : Coord) -> coord.X = 1L && coord.Y = 0L) g.LiveCells |> should be True
       Seq.exists (fun (coord : Coord) -> coord.X = 2L && coord.Y = 0L) g.LiveCells |> should be True
            
