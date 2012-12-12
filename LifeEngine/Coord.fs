namespace LifeEngine

open System

type Coord(x, y) =
    member this.IsNeighbour(coord : Coord) =
        match coord with
        | _ when x = coord.X && y = coord.Y -> false
        | _ when Math.Abs(x - coord.X : int64) <= 1L &&
                 Math.Abs(y - coord.Y : int64) <= 1L -> true
        | _ -> false

    member this.X = x
    member this.Y = y

    new() = Coord(0L, 0L)