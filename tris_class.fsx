open System.Windows.Forms
open System.Drawing

let f = new Form(Text="Tetris", Width=300, Height=500, TopMost=true)
f.Show()
let bgimg = new Bitmap(@"C:\Users\Alessandro\Desktop\wood_background.jpg")
f.BackgroundImage <- bgimg
f.BackgroundImageLayout <- ImageLayout.Tile

f.MinimumSize <- new Size(370, 50)

let set_grid_size (e:UserControl) =
	let width = f.ClientSize.Width
	let height = f.ClientSize.Height
	let cx = width / 2
	let cy = height / 2
	(*Divido ora l'area utile in 10 parti, la griglia occuperà 6/10 di area*)
	let decimo = if width > height then height/10 else width/10
	e.Width <- decimo * 6
	e.Height <- decimo * 6
	e.Location <- Point(cx-(3*decimo), cy-(3*decimo))


type Grid () as this =
	inherit UserControl()
	do this.DoubleBuffered <- true
	//Stato Interno
	let stato_celle = array2D [ [2; 2; 2]; [2; 2; 2]; [2; 2; 2]]
	let mutable count = 0

	override this.OnPaint e =
		let g = e.Graphics
		let savedGraph = g.Save()

		let p = new Pen(Color.White, width=6.f)
		let lenght = this.Width // = this.Height
		for i = 1 to 4 do
			g.DrawLine(p, 0, lenght/3, lenght, lenght/3)
			g.RotateTransform(float32(90))
		g.Restore(savedGraph)

	override this.OnMouseClick e =
		printfn "%d %d" e.X e.Y

	override this.OnResize e =
		this.Invalidate()

	override this.OnPaintBackground e =
		((*Codice vuoto: stampa background trasparente*))

let e = new Grid()
set_grid_size e
f.Controls.Add(e)

f.Resize.Add(fun _ ->
	set_grid_size e
	f.Invalidate()
)
