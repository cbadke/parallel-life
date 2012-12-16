// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
module ParallelLife

open LifeEngine
open System.Windows.Forms

[<EntryPoint>]
let main argv = 
    let propeller = ["000";"111";"000"];

    do Application.Run(new LifeRenderer.RenderWindow(propeller))
    0 // return an integer exit code
