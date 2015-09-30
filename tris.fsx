open System.Windows.Forms
open System.Drawing
//#r @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5.2\PresentationCore.dll"
//open System.Windows.Media

let f = new Form(Text="Tetris", Width=300, Height=500, TopMost=true)
f.Show()
let bgimg = new Bitmap(@"C:\Users\Alessandro\Desktop\woodbg.jpg")
f.BackgroundImage <- bgimg
f.BackgroundImageLayout <- ImageLayout.Center
f.MinimumSize <- new Size(370, 500)
//Note: Evitare la ripetizione del bgimg

let print_grid (g:Graphics) (p: Pen) (cx:float32) (cy:float32) width height =
	let savedGraph = g.Save()
	g.TranslateTransform(cx, cy)
	let decimo = if width > height then height/10 else width/10
	for i = 1 to 4 do
		g.DrawLine(p, -3*decimo, decimo, 3*decimo, decimo)
		g.RotateTransform(float32(90))
	(*g.DrawLine(p, -3*decimo, -decimo, 3*decimo, -decimo)
	g.DrawLine(p, decimo, -3*decimo, decimo, 3*decimo)
	g.DrawLine(p, -decimo, -3*decimo, -decimo, 3*decimo)*)
	g.Restore(savedGraph)

f.Paint.Add(fun e ->
	let g = e.Graphics

	let width = f.ClientSize.Width
	let height = f.ClientSize.Height
	let cx = float32(width/2)
	let cy = float32(height/2)
	//let drGroup = new DrawingGroup()

	use whPen = new Pen(Color.White, width = float32(6))
	use whPen = new Pen(Color.White, float32(6))

	print_grid g whPen cx cy width height
)

f.Resize.Add(fun e ->
	f.Invalidate()
)

f.MouseClick.Add(fun e ->
	let x = e.X
	let y = e.Y
)