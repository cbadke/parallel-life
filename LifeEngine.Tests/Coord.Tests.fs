namespace Coord.Tests

open NUnit.Framework
open FsUnit
open LifeEngine

[<TestFixture>]
type ``Coordinate Object`` ()=

    [<Test>]
    member test.
     ``Can be instantiated with ints``() =
        let c = Coord(5L, 9L)
        c.X |> should equal 5L
        c.Y |> should equal 9L

    [<Test>]
    member test.
     ``Identifies itself as non-neighbour``() =
        let c = Coord()
        c.IsNeighbour c |> should be False

    [<Test>]
    member test.
     ``Identifies horizontal neighbour as neighbour``() =
        let c = Coord(0L, 0L)
        let left = Coord(-1L, 0L)
        let right = Coord(1L, 0L)
        c.IsNeighbour left |> should be True
        c.IsNeighbour right |> should be True

    [<Test>]
    member test.
     ``Identifies vertical neighbour as neighbour``() =
        let c = Coord()
        let up = Coord(0L, 1L)
        let down = Coord(0L, -1L)
        c.IsNeighbour up |> should be True
        c.IsNeighbour down |> should be True

    [<Test>]
    member test.
     ``Identifies 'row-neighbour' as non-neighbour`` () =
        let c = Coord()
        let upRow = Coord(10L, 1L)
        c.IsNeighbour upRow |> should be False