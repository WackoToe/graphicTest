(* TRASFORMAZIONI AFFINI NEL PIANO *)

open System.Windows.Forms
open System.Drawing
//open System.Threading

let f = new Form(Text="Clock", TopMost=true)
f.Show()

// Valori iniziali
let cx = 100.f
let cy = 100.f

f.Paint.Add(fun e ->
	let g = e.Graphics
	g.SmoothingMode <- Drawing2D.SmoothingMode.HighQuality
	g.TranslateTransform(cx, cy)
	g.RotateTransform(-90.f)
	let s = g.Save() //Salvo il contenuto del contesto grafico
	for i = 1 to 12 do
		g.DrawLine(Pens.Black, 90, 0, 100, 0)
		g.RotateTransform(30.f)
	g.Restore(s) //Lo ripristino per evitare l'accumularsi di errori grafici dovuto ad approssimazione di cos e sin
	let t = System.DateTime.Now
	g.RotateTransform(single((t.Hour % 12) * 30))
	g.DrawLine(Pens.Black, -5, 0, 60, 0)
	g.Restore(s)
	g.RotateTransform(single(t.Minute * 6))
	g.DrawLine(Pens.Black, -5, 0, 60, 0)
	g.Restore(s)
	g.RotateTransform(single(t.Second * 6 ))
	g.DrawLine(Pens.Red, -5, 0, 60, 0)
)

let timer = new Timer(Interval=1000) //Creo timer, ogni 1000 secondi
timer.Tick.Add(fun _ -> 
	f.Invalidate()
)
timer.Start()


