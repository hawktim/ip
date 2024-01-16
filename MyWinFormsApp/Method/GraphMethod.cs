using iText.Layout;
using iText.Layout.Element;
using System.Drawing;
using System;
using System.Drawing.Imaging;
using System.Windows.Forms;
using iText.IO.Image;
using MyWinFormsApp;
public class GraphMethod : DiscriminantMethod
{
    protected override string NameMethod { get; } = "Графический способ";
    protected override bool NotDescr { get; } = true;
    private void AddImage(Document document, DArg arg)
    {
        document.Add(new Paragraph("Графически построим:"));
        document.Add(new Paragraph("параболу:"));
        var formula2 = @"y=" + arg.A + "x^2";
        var imgp = new iText.Layout.Element.Image(
                       ImageDataFactory.Create(
                           ImageHelper.ImageToBitmap(ImageHelper.GetImageFormula(formula2))
                           , Color.White, false)
                       );
        imgp.SetWidth(200);
        document.Add(new Paragraph().Add(imgp));

        document.Add(new Paragraph("прямую:"));
        var formula3 = @"y=" + arg.B + "x+c";
        var imgl = new iText.Layout.Element.Image(
                       ImageDataFactory.Create(
                           ImageHelper.ImageToBitmap(ImageHelper.GetImageFormula(formula3))
                           , Color.White, false)
                       );
        imgl.SetWidth(200);
        document.Add(new Paragraph().Add(imgl));


        var im = GetImageGrp(arg);
        var img1 = new iText.Layout.Element.Image(ImageDataFactory.Create(im, null, false));
        img1.SetAutoScaleWidth(true);
        document.Add(new AreaBreak());
        document.Add(new Paragraph().Add(img1));

    }
    /// <summary>
    /// больше нуля
    /// </summary>
    public override void GreaterThanZero(Document document, DArg arg)
    {
        AddImage(document, arg);

        var d = GetDiscriminant(arg);
        var x1 = (-arg.B - Math.Sqrt(d)) / (2 * arg.A);
        var x2 = (-arg.B + Math.Sqrt(d)) / (2 * arg.A);

        document.Add(new Paragraph("Точки пересечения прямой и параболы будут точками А и В с абсциссами х1 = " + x1 + " и х2 = " + x2 + " соответственно."));
        var formula4 = @"x_1=" + x1 + "; x_2 = " + x2;
        var img3 = new iText.Layout.Element.Image(
                       ImageDataFactory.Create(
                           ImageHelper.ImageToBitmap(ImageHelper.GetImageFormula(formula4))
                           , Color.White, false)
                       );
        img3.SetWidth(200);
        document.Add(new Paragraph("Ответ:"));
        document.Add(new Paragraph().Add(img3));
    }

    /// <summary>
    /// Равно нулю
    /// </summary>
    public override void EqualToZero(Document document, DArg arg)
    {
        AddImage(document, arg);
        var d = GetDiscriminant(arg);
        var x1 = (-arg.B) / (2 * arg.A);

        document.Add(new Paragraph("Пересечением прямой и параболы будет точка А с абсциссой х = "+ x1 ));
        var formula4 = @"x_1=x_2=" + x1;
        var img3 = new iText.Layout.Element.Image(
                       ImageDataFactory.Create(
                           ImageHelper.ImageToBitmap(ImageHelper.GetImageFormula(formula4))
                           , Color.White, false)
                       );
        img3.SetWidth(200);
        document.Add(new Paragraph("Ответ:"));
        document.Add(new Paragraph().Add(img3));
    }

    /// <summary>
    /// меньше нуля
    /// </summary>
    public override void LessThanZero(Document document, DArg arg)
    {
        AddImage(document, arg);
        document.Add(new Paragraph("Ответ: уравнение не имеет корней в действительных числах"));
    }
    private void DrawGraph(Graphics g, DArg arg)
    {
        var a = arg.A;
        var b = arg.B;
        var c = arg.C;
        
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        var kx = 250;
        var ky = 150;

        var koof = 10;

        int j = 950;
        int cent = j / 2;
        float fl = 0.2F;
        float dop = fl;
        if (a < 0)
        {
            dop *= -1;
            fl *= -1;
        }

        PointF[] points = new PointF[j + 1];

        points[cent] = new PointF(kx + 0, ky + 0);
        var setk = 0F;
        for (int i = 1; i <= cent; i++)
        {
            var xx = (float)Math.Sqrt(dop / a) * koof;
            points[cent - i] = new PointF(kx + -xx, ky + dop * -1 * koof);
            points[cent + i] = new PointF(kx + xx, ky + dop * -1 * koof);
            dop += fl;

            setk = i * koof;
            //points[cent] = new PointF(kx + 0, ky + 0);
            PointF[] pointslinex = { new PointF((float)0, ky + setk), new PointF((float)kx * 2, ky + setk) };
            g.DrawLines(Pens.LightGray, pointslinex);
            PointF[] pointslinex1 = { new PointF((float)0, ky - setk), new PointF((float)kx * 2, ky - setk) };
            g.DrawLines(Pens.LightGray, pointslinex1);
            PointF[] pointsliney = { new PointF(kx + setk, 0), new PointF(kx + setk, (float)ky * 2) };
            g.DrawLines(Pens.LightGray, pointsliney);
            PointF[] pointsliney1 = { new PointF(kx - setk, 0), new PointF(kx - setk, (float)ky * 2) };
            g.DrawLines(Pens.LightGray, pointsliney1);
        }
        g.DrawLines(Pens.Red, points);

        PointF[] pointsx = { new PointF(0, ky), new PointF(kx * 2, ky) };
        g.DrawLines(Pens.Black, pointsx);

        PointF[] pointsy = { new PointF(kx, 0), new PointF(kx, ky * 2) };
        g.DrawLines(Pens.Black, pointsy);

        PointF[] pointsline = new PointF[2];

        if (b != 0)
        {
            for (var i = 1; i < 3; i++)
            {
                var yy1 = i * 100 * (i > 1 ? -1 : 1);
                var xx1 = (-yy1 - c) / b;
                pointsline[i - 1] = new PointF((float)(kx + xx1 * koof), (float)((ky) - (yy1 * koof)));
            }
        }
        else
        {
            for (var i = 1; i < 3; i++)
            {
                var xx1 = i * 100 * (i > 1 ? -1 : 1);
                var yy1 = -c;
                pointsline[i - 1] = new PointF((float)(kx + xx1 * koof), (float)((ky) - (yy1 * koof)));
            }
        }
        g.DrawLines(Pens.Blue, pointsline);
    }
    private System.Drawing.Image GetImageGrp(DArg arg)
    {
        using (var pictureBox2 = new System.Windows.Forms.PictureBox())
        {
            ((System.ComponentModel.ISupportInitialize)(pictureBox2)).BeginInit();
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new System.Drawing.Size(500, 300);
            pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            ((System.ComponentModel.ISupportInitialize)(pictureBox2)).EndInit();
            pictureBox2.CreateGraphics();
            pictureBox2.Refresh();
            pictureBox2.Image = new Bitmap(pictureBox2.Width, pictureBox2.Height);

            System.Drawing.Image bmp = pictureBox2.Image;
            Graphics g = Graphics.FromImage(bmp);
            DrawGraph(g, arg);
            //pictureBox2.Image.Save(@"C:\WORK\test\image.jpg", System.Drawing.Imaging.ImageFormat.Png);
            //Bitmap bitmap = new Bitmap(pictureBox2.Image);
            //bitmap.Save(@"C:\WORK\test\image.jpg", ImageFormat.Jpeg);
            return pictureBox2.Image;
        }
    }
}
