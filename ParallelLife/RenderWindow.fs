﻿namespace LifeRenderer

open System.Windows.Forms
open System.Drawing
open LifeEngine

type UpdateDelegate = delegate of unit -> unit

type RenderWindow(seed : seq<string>) as form =
    inherit Form()

    let defaultSquareSize = 10.0
    let mutable anchorX, anchorY = (0, 0)
    let mutable squareSize = 10.0
    let mutable game = Game(seed)

    let moveLeft() = anchorX <- anchorX - 1
    let moveRight() = anchorX <- anchorX + 1
    let moveUp() = anchorY <- anchorY - 1
    let moveDown() = anchorY <- anchorY + 1
    let centerView() = let extents = game.Extents
                       let gameCenterX = (fst extents).X + (((snd extents).X - (fst extents).X) / int64 2)
                       let gameCenterY = (fst extents).Y + (((snd extents).Y - (fst extents).Y) / int64 2)
                       let screenWidth = form.ClientSize.Width / int squareSize
                       let screenHeight = form.ClientSize.Height / int squareSize
                       anchorX <- int gameCenterX - (screenWidth / 2)
                       anchorY <- int gameCenterY - (screenHeight / 2)
                       

    let drawGame (gp : Graphics) =
        let renderCell (cell : Coord) = 
            let x = int squareSize * (int cell.X - anchorX)
            let y = int squareSize * (int cell.Y - anchorY)
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
        this.ClientSize <- Size(800,800)
        this.Text <- "Conway's Game of Life"
        this.BackColor <- Color.Black

        centerView()

        let t = new Timer()
        t.Interval <- 100;
        t.Tick.Add UpdateWorld
        t.Start()

    override this.OnPaint e =
        let gp = form.CreateGraphics()
        drawGame(gp)
        gp.Dispose()

    override this.OnKeyUp e =
        base.OnKeyUp(e);

        match e.KeyCode with
        | Keys.Add -> squareSize <- squareSize * 0.5
                      this.Refresh()
        | Keys.Subtract -> squareSize <- squareSize * 2.0
                           this.Refresh()
        | Keys.Space -> squareSize <- defaultSquareSize
                        centerView()
                        this.Refresh()
        | Keys.Left -> moveLeft()
                       this.Refresh()
        | Keys.Right -> moveRight()
                        this.Refresh()
        | Keys.Up -> moveUp()
                     this.Refresh()
        | Keys.Down -> moveDown()
                       this.Refresh()
        | Keys.Q -> Application.Exit()
        | _ -> this.Refresh()