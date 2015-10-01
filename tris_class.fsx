open System.Windows.Forms
open System.Drawing

let f = new Form(Text="Tetris", Width=300, Height=500, TopMost=true)
f.Show()
let bgimg = new Bitmap(@"C:\Users\Alessandro\Desktop\wood_background.jpg")
f.BackgroundImage <- bgimg
f.BackgroundImageLayout <- ImageLayout.Tile

f.MinimumSize <- new Size(370, 50)

type Grid () as this =
	inherit UserControl()
	do this.DoubleBuffered <- true
	do this.BackColor <- Color.Transparent 

	//Stato Interno
	let stato_celle = array2D [ [2; 2; 2]; [2; 2; 2]; [2; 2; 2]]
	let mutable count = 0
	let mutable cell_size = 0

	member this.CellSize
		with get () = cell_size
		and set(v) = cell_size <- v

	member this.DrawCross (g:Graphics) x y =
		let savedGraph = g.Save()
		g.TranslateTransform(float32(x*cell_size + cell_size/2), float32(y*cell_size+cell_size/2))
		for i = 0 to 4 do
			g.RotateTransform(45.f)
			g.DrawLine(Pens.Black, 0, 0, cell_size/2, 0)
		g.Restore(savedGraph)

	member this.DrawCircle (g:Graphics) x y =
		let savedGraph = g.Save()
		g.TranslateTransform(float32(x*cell_size + cell_size/2), float32(y*cell_size+cell_size/2))
		g.DrawEllipse(Pens.Black, -cell_size/2, -cell_size/2, cell_size/2, cell_size/2)
		g.Restore(savedGraph)

	member this.CrossCircle (g:Graphics) =
		for i = 0 to 2 do
			for j = 0 to 2 do
				if stato_celle.[i, j]=0 then
					this.DrawCross g i j
					printfn "RAMO THEN"
				else if stato_celle.[i, j]=1 then
					this.DrawCircle g i j
					printfn "RAMO ELSE"

	override this.OnPaint e =
		let g = e.Graphics
		let savedGraph = g.Save()
		let p = new Pen(Color.White, width=6.f)
		let lenght = this.Width // = this.Height
		g.TranslateTransform(float32(lenght/2), float32(lenght/2) )
		for i = 1 to 4 do
			g.DrawLine(p, -lenght/2, lenght/6, lenght/2, lenght/6)
			g.RotateTransform(90.f)
		g.Restore(savedGraph)
		this.CrossCircle(g)


	override this.OnMouseClick e =
		let mutable column = 0
		let mutable row = 0
		match e.X with
		| x when x<cell_size -> column <- 0
		| x when x<2*cell_size -> column <- 1
		| _ -> column <- 2
		match e.Y with
		| y when y<cell_size -> row <-  0
		| y when y<2*cell_size -> row <- 1
		| _ -> row <- 2

		this.Invalidate()

	override this.OnResize e =
		this.Invalidate()


let set_grid_size (e:Grid) =
	let width = f.ClientSize.Width
	let height = f.ClientSize.Height
	let cx = width / 2
	let cy = height / 2
	(*Divido ora l'area utile in 10 parti, la griglia occuperà 6/10 di area*)
	let decimo = if width > height then height/10 else width/10
	e.Width <- decimo * 6
	e.Height <- decimo * 6
	e.Location <- Point(cx-(3*decimo), cy-(3*decimo))
	e.CellSize <- decimo*2

let e = new Grid()
set_grid_size e
f.Controls.Add(e)

f.Resize.Add(fun _ ->
	set_grid_size e
	f.Invalidate()
)
