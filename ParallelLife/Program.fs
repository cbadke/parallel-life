// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
module ParallelLife

open LifeEngine
open System.IO
open System.Windows.Forms

[<EntryPoint>]
let main argv = 
    
    match Array.length argv with
    | 1 -> if File.Exists argv.[0] then
               let seed = File.ReadAllLines argv.[0]
               do Application.Run( new LifeRenderer.RenderWindow(seed) )
    | _ -> printfn "Usage: ParallelLife path_to_seed_file"

    0 // return an integer exit code
