namespace LifeEngine

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
    
    member x.Extents = match cells with
                       | _ when Seq.isEmpty cells -> (0L,0L)
                       | _ -> (gridRight cells - gridLeft cells + 1L, gridBottom cells - gridTop cells + 1L)

    member x.LiveCells = cells

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
