namespace Coord.Tests

open NUnit.Framework
open FsUnit
open LifeEngine

[<TestFixture>]
type ``Coord`` ()=
    let c = LifeEngine.Coord()  

    [<Test>] member test.
     ``Identifies itself as non-neighbour``() =
        c.IsNeighbour c |> should be False