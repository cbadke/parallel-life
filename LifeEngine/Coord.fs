namespace LifeEngine

open System

type Coord(x, y) =

    member this.X = x

    member this.Y = y

    override this.Equals coord = match coord with
                                 | :? Coord -> x =  (coord :?> Coord).X && y = (coord :?> Coord).Y
                                 | _ -> false

    override this.GetHashCode() = x.GetHashCode() + y.GetHashCode()

    member this.IsNeighbour(coord : Coord) =
        match coord with
        | _ when x = coord.X && y = coord.Y -> false
        | _ when Math.Abs(x - coord.X : int64) <= 1L &&
                 Math.Abs(y - coord.Y : int64) <= 1L -> true
        | _ -> false

    member this.Neighbours = [| Coord(x-1L, y-1L);
                                Coord(x, y-1L);
                                Coord(x+1L, y-1L);
                                Coord(x-1L, y);
                                Coord(x+1L, y);
                                Coord(x-1L, y+1L);
                                Coord(x, y+1L);
                                Coord(x+1L, y+1L) |]

    new() = Coord(0L, 0L)