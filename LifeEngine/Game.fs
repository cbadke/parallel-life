namespace LifeEngine

open LifeEngine

type Game(cells : seq<Coord>) =

    let gridTop g = g
                    |> Seq.map (fun (c : Coord) -> c.Y)
                    |> Seq.min
    let gridBottom g = g
                       |> Seq.map (fun (c : Coord) -> c.Y)
                       |> Seq.max
    let gridLeft g = g
                     |> Seq.map (fun (c : Coord) -> c.X)
                     |> Seq.min
    let gridRight g = g
                      |> Seq.map (fun (c : Coord) -> c.X)
                      |> Seq.max

    let countLivingNeighbours (coord : Coord) = cells
                                                |> Seq.filter(coord.IsNeighbour)
                                                |> Seq.length

    let currentState (coord : Coord) = match coord with
                                        | _ when Seq.exists (fun b -> b = coord) cells -> Rules.Alive
                                        | _ -> Rules.Dead
    
    member x.Extents = match cells with
                       | _ when Seq.isEmpty cells -> (Coord(0L,0L), Coord(0L,0L))
                       | _ -> (Coord(gridLeft cells, gridTop cells), Coord(gridRight cells, gridBottom cells))

    member x.LiveCells = cells

    member x.Next = let newCells = cells
                                    |> Seq.map (fun c -> c :: c.Neighbours)
                                    |> Seq.concat
                                    |> Seq.distinct
                                    |> Seq.filter (fun c -> Rules.NextState (currentState c) (countLivingNeighbours c) = Rules.Alive)
                    Game(newCells)

    new(collection : seq<string>) =
        let parseString (idx : int) str = str 
                                          |> Seq.fold (fun (column, rowCol) ch -> 
                                                if ch <> '0' then 
                                                    (column+1L, Coord(column,int64 idx)::rowCol) 
                                                else 
                                                    (column+1L, rowCol)) 
                                                (0L, List.Empty)
        let coords = collection
                    |> Seq.mapi parseString
                    |> Seq.map (fun (a,b) -> b)
                    |> Seq.fold (fun gr row -> if List.length row > 0 then row::gr else gr) List.Empty
                    |> Seq.concat
        Game(coords)
    new() = Game(Seq.empty<Coord>)
