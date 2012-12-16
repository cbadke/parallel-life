namespace LifeRenderer

open System.Windows.Forms
open System.Drawing
open LifeEngine

type UpdateDelegate = delegate of unit -> unit

type RenderWindow(seed : seq<string>) as form =
    inherit Form()

    let mutable squareSize = 10.0
    let mutable game = Game(seed)

    let drawGame (gp : Graphics) =
        let renderCell (cell : Coord) = 
            let x = int squareSize * int cell.X
            let y = int squareSize * int cell.Y
            let width = int squareSize - 1

            let myBrush = new System.Drawing.SolidBrush(Color.White)
            let rectangle = Rectangle(x, y, width, width)
            gp.FillRectangle(myBrush, rectangle)

            myBrush.Dispose()
        Seq.toArray game.LiveCells
        |> Array.iter renderCell

    let UpdateWorld e =
        game <- game.Next
        form.Refresh()

    do form.InitializeForm()

    member this.InitializeForm() =
        this.MinimumSize <- Size(400,400) 
        this.Text <- "Conway's Game of Life"
        this.BackColor <- Color.Black

        let t = new Timer()
        t.Interval <- 500;
        t.Tick.Add UpdateWorld
        t.Start()

    override this.OnPaint e =
        let gp = form.CreateGraphics()
        drawGame(gp)
        gp.Dispose()

    override this.OnKeyPress e =
        base.OnKeyPress(e);

        match e.KeyChar with
        | '+' -> squareSize <- squareSize * 0.5
                 this.Refresh()
        | '-' -> squareSize <- squareSize * 2.0
                 this.Refresh()
        | ' ' -> squareSize <- 100.0
                 UpdateWorld()
                 this.Refresh()
        | 'Q'
        | 'q' -> Application.Exit()
        | _ -> this.Refresh()