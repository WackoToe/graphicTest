open System.Windows.Forms
open System.Drawing

let f = new Form(Text="Curve", TopMost=true)
f.Show()

type Editor() =
	inherit UserControl()

	let pts = [| Point (); Point(20,20); Point(50,50); Point(50,100)|]

	let handleSize = 5
	let mutable selected = None
	let mutable tension = 1.f

	let handleHitTest (p:Point) (h:Point) =
		let x = p.X - h.X
		let y = p.Y - h.Y
		x*x + y*y < handleSize * handleSize

	member this.Tension
		with get() = tension
		and set(v) = tension <- v; this.Invalidate()

	override this.OnMouseDown e =
		//printfn "%d, %d" e.Location.X e.Location.Y
		let ht = handleHitTest e.Location
		selected <- pts |> Array.tryFindIndex ht

	override this.OnMouseUp e =
		selected <- None

	override this.OnMouseMove e =
		match selected with
			| Some index -> 
				pts.[index] <- e.Location
				this.Invalidate()
			| None -> ()

	override this.OnPaint e =
		let g = e.Graphics

		let drawHandle (p:Point) =
			let w = 5
			g.DrawEllipse(Pens.Black, p.X - w, p.Y-w, 2*w, 2*w)

		//Disegno la curva
		//disegno la curva 
		g.DrawBezier(Pens.Black, pts.[0], pts.[1], pts.[2], pts.[3])
		g.DrawLine(Pens.Red, pts.[0], pts.[1])
		g.DrawLine(Pens.Red, pts.[2], pts.[3])
		g.DrawCurve(Pens.Blue, pts, tension)

		//disegno le maniglie
		//Array.iter drawHandle p
		//versione con forward pipe operator:
		//let (|>) x f = f x
		//iter: per ogni elem, do
		pts |> Array.iter drawHandle


let e = new Editor(Dock=DockStyle.Fill)
f.Controls.Add(e)
