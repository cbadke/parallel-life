namespace LifeEngine

type Game(collection : seq<string>) =
    let parseString (idx : int) str = str 
                                      |> Seq.fold (fun (column, rowCol) ch -> 
                                            if ch <> '0' then 
                                                (column+1L, Coord(column,int64 idx)::rowCol) 
                                            else 
                                                (column+1L, rowCol)) 
                                            (0L, List.Empty)
    let grid = collection
               |> Seq.mapi parseString
               |> Seq.map (fun (a,b) -> b)
               |> Seq.fold (fun gr row -> if List.length row > 0 then row::gr else gr) List.Empty

    let gridTop g = g
                    |> Seq.map (fun row -> row
                                           |> Seq.map (fun (c : Coord) -> c.Y)
                                           |> Seq.min)
                    |> Seq.min
    let gridBottom g = g
                       |> Seq.map (fun row -> row
                                              |> Seq.map (fun (c : Coord) -> c.Y)
                                              |> Seq.max)
                       |> Seq.max
    let gridLeft g = g
                     |>   Seq.map (fun row -> row
                                              |> Seq.map (fun (c : Coord) -> c.X)
                                              |> Seq.min)
                     |> Seq.min
    let gridRight g = g
                      |> Seq.map (fun row -> row
                                             |> Seq.map (fun (c : Coord) -> c.X)
                                             |> Seq.max)
                      |> Seq.max
    
    member x.Extents = match grid with
                       | _ when List.isEmpty grid -> (0L,0L)
                       | _ -> (gridRight grid - gridLeft grid + 1L, gridBottom grid - gridTop grid + 1L)


    member x.LiveCells = Seq.concat grid
    new() = Game(Seq.empty)