open System.Windows.Forms
open System.Drawing
//#r @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5.2\PresentationCore.dll"
//open System.Windows.Media

let f = new Form(Text="Tetris", Width=300, Height=500, TopMost=true)
f.Show()
let bgimg = new Bitmap(@"C:\Users\Alessandro\Desktop\wood_background.jpg")
f.BackgroundImage <- bgimg
f.BackgroundImageLayout <- ImageLayout.Tile
f.MinimumSize <- new Size(370, 500)

let graf = f.CreateGraphics()
let stato_celle = array2D [ [2; 2; 2]; [2; 2; 2]; [2; 2; 2]]
let width = ref f.ClientSize.Width
let height = ref f.ClientSize.Height
let decimo = ref (if width > height then !height/10 else  !width/10)
let mutable count = 0

let print_grid (g:Graphics) (p: Pen) (cx:float32) (cy:float32) width height = 
	let savedGraph = g.Save()
	g.TranslateTransform(cx, cy)
	decimo.Value <- if width > height then height/10 else width/10
	for i = 1 to 4 do
		g.DrawLine(p, -3*decimo.Value, decimo.Value, 3*decimo.Value, decimo.Value)
		g.RotateTransform(float32(90))
	g.Restore(savedGraph)

f.Paint.Add(fun e ->
	let g = e.Graphics

	width.Value <- f.ClientSize.Width
	height.Value <- f.ClientSize.Height
	let cx = float32(!width/2)
	let cy = float32(!height/2)
	//let drGroup = new DrawingGroup()

	use whPen = new Pen(Color.White, width = float32(6))
	use whPen = new Pen(Color.White, float32(6))

	print_grid g whPen cx cy !width !height
)

f.Resize.Add(fun e ->
	f.Invalidate()
)

let drawCross x y =
	graf.DrawLine (Pens.Beige, 0, 0, 100, 100)

let drawCircle x y =
	graf.DrawLine (Pens.Black, 0, 0, 100, 100)

let drawMatrix =
	let cx = float32(!width/2)
	let cy = float32(!height/2) 
	if (stato_celle.[0, 0] = 0) then
		drawCross (-3*decimo.Value) (-3*decimo.Value)
	else if (stato_celle.[0, 0] = 1) then
		drawCircle (-3*decimo.Value) (-3*decimo.Value)


let updateCell (x:int) (y:int) =
	decimo := if width > height then !height/10 else !width/10
	printfn "decimo: %d" !decimo
	let h_grid = 6*decimo.Value
	let x = x - ((!width - h_grid) / 2)
	let y = y - ((!height - h_grid) / 2)
	let cell_x = if x > 0 then x/(2*decimo.Value) else -1
	let cell_y = if y > 0 then y/(2*decimo.Value) else -1
	printfn "Cell_x: %d" cell_x
	printfn "Cell_y: %d" cell_y
	if (cell_x < 3 && cell_y < 3) then
		stato_celle.[cell_x, cell_y] <- if (stato_celle.[cell_x, cell_y] = 2) then count%2 else stato_celle.[cell_x, cell_y]
		count <- count+1
	drawMatrix

f.MouseClick.Add(fun e ->
	let x = e.X
	let y = e.Y
	printfn "x: %d" x
	printfn "y: %d" y
	updateCell x y
)