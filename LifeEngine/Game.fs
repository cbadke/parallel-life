namespace LifeEngine

type Game(collection : seq<string>) =
    let parseString idx str = str 
                              |> Seq.fold (fun (column, rowCol) ch -> 
                                            if ch <> '0' then 
                                                (column+1, (column,idx)::rowCol) 
                                            else 
                                                (column+1, rowCol)) 
                                 (0, List.Empty)
    let grid = collection
               |> Seq.mapi parseString
               |> Seq.map (fun (a,b) -> b)
               |> Seq.fold (fun gr row -> if List.length row > 0 then row::gr else gr) List.Empty

    let gridTop g = g
                    |> Seq.map (fun row -> row
                                           |> Seq.map (fun (x,y) -> y)
                                           |> Seq.min)
                    |> Seq.min
    let gridBottom g = g
                       |> Seq.map (fun row -> row
                                              |> Seq.map (fun (x,y) -> y)
                                              |> Seq.max)
                       |> Seq.max
    let gridLeft g = g
                     |>   Seq.map (fun row -> row
                                              |> Seq.map (fun (x,y) -> x)
                                              |> Seq.min)
                     |> Seq.min
    let gridRight g = g
                      |> Seq.map (fun row -> row
                                             |> Seq.map (fun (x,y) -> x)
                                             |> Seq.max)
                      |> Seq.max
    
    member x.Extents = match grid with
                       | _ when List.isEmpty grid -> (0,0)
                       | _ -> (gridRight grid - gridLeft grid + 1, gridBottom grid - gridTop grid + 1)

    new() = Game(Seq.empty)