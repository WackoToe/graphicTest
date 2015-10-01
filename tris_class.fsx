open System.Windows.Forms
open System.Drawing

let f = new Form(Text="Tetris", Width=300, Height=500, TopMost=true)
f.Show()
let bgimg = new Bitmap(@"C:\Users\Alessandro\Desktop\wood_background.jpg")
f.BackgroundImage <- bgimg
f.BackgroundImageLayout <- ImageLayout.Tile

f.MinimumSize <- new Size(370, 50)


type Grid () =
	inherit UserControl()

	//Stato Interno
	let stato_celle = array2D [ [2; 2; 2]; [2; 2; 2]; [2; 2; 2]]
	let mutable count = 0

	override this.OnPaint e =
		let g = e.Graphics
		let savedGraph = g.Save()

		let width = f.ClientSize.Width
		let height = f.ClientSize.Height
		let cx = float32(width / 2)
		let cy = float32(height / 2)
		g.TranslateTransform(cx, cy)

		let decimo = if width > height then height/10 else width/10
		let p = new Pen(Color.White, width=6.f)
		for i = 1 to 4 do
			g.DrawLine(p, -3*decimo, decimo, 3*decimo, decimo)
			g.RotateTransform(float32(90))
		g.Restore(savedGraph)

	override this.OnMouseClick e =
		printfn "%d %d" e.X e.Y

	override this.OnResize e =
		this.Invalidate()

	override this.OnPaintBackground e =
		()

let e = new Grid(Dock=DockStyle.Fill)
f.Controls.Add(e)
f.Resize.Add(fun e ->
	f.Invalidate()
)
