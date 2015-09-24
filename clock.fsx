open System.Windows.Forms
open System.Drawing

let f = new Form(Text="Clock", TopMost=true)
f.Show()

let cx, cy = 100, 100

let drawTick (g: Graphics) (p:Pen) r l a = 
    let a1 = (a/180.) * System.Math.PI
    let x1 = int(r * cos(a1))
    let y1 = int(r * sin(a1))
    let x2 = int((r+l)*cos(a1))
    let y2 = int((r+l)*sin(a1))
    g.DrawLine(p, cx+x1, cy+y1, cx+x2, cy+y2)

f.Paint.Add(fun e ->
    let g = e.Graphics
    for a in 0. .. 30. .. 360. do
        drawTick g Pens.Black 85. 15. a
)

let drawHour (g: Graphics) (p:Pen) r h m s =
    let angoloMinuti = System.Math.PI / 30. * m - System.Math.PI/2.
    let x1 = int((r-20.) * cos(angoloMinuti))
    let y1 = int((r-20.) * sin(angoloMinuti))
    g.DrawLine(p, cx, cy, cx+x1, cx+y1)

    let angoloOre = System.Math.PI/6. * h - System.Math.PI/2. + System.Math.PI/120.*m
    let x2 = int((r-5.) * cos(angoloOre))
    let y2 = int((r-5.) * sin(angoloOre))
    g.DrawLine(p, cx, cy, cx+x2, cx+y2)

    let angoloSecondi = System.Math.PI/30. * s - System.Math.PI/2.
    let x3 = int((r-22.) * cos(angoloSecondi))
    let y3 = int((r-22.) * sin(angoloSecondi))

    g.DrawLine(p, cx, cy, cx+x3, cy+y3)

f.Paint.Add(fun e ->
    let g1 = e.Graphics
    let hour = System.DateTime.Now.Hour
    let min = System.DateTime.Now.Minute
    let sec = System.DateTime.Now.Second
    drawHour g1 Pens.Red 85. (float hour) (float min) (float sec)  
)

f.Invalidate()
