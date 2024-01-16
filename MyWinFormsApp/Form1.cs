using System;
using System.Windows.Forms;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Pdfa;
using System.IO;
using System.Drawing;
using System.Security.Cryptography;

namespace MyWinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Document GetPdfDocument(string fileName)
        {
            string filePath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "PDFResources", "sRGB_CS_profile.icm");
            string fontFile = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "PDFResources", "Roboto-Regular.ttf");

            PdfADocument pdf = new PdfADocument(
                new PdfWriter(fileName),
                PdfAConformanceLevel.PDF_A_2U,
                new PdfOutputIntent("Custom", "", "", "sRGB IEC61966-2.1",
                new FileStream(filePath, FileMode.Open, FileAccess.Read)));

            Document document = new Document(pdf);

            var font = PdfFontFactory.CreateFont(fontFile, "Identity-H");
            document.SetFont(font);
            return document;
        }
        private void CloseDocument(Document document, string fileName)
        {
            document.Close();
            System.Diagnostics.Process.Start(fileName);
        }
        private void Execute<T>(Document document, DArg arg) where T:DiscriminantMethod, new()
        {
            var x = new T();
            x.Execute(document, arg);
        }

        private void btnCreatePDF_Click(object sender, EventArgs e)
        {
            numericValueChanged(sender, e);

            string fileName = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "PDFResources", "sample.pdf");
            var document = GetPdfDocument(fileName);
            try
            {
                btnCreatePDF.Enabled = false;
                try
                {
                    Paragraph header = new Paragraph("Решение квадратных уравнений")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(20);
                    document.Add(header);

                    var d = new DArg((double)numericUpDown1.Value, (double)numericUpDown2.Value, (double)numericUpDown3.Value);

                    if (checkBox1.Checked) Execute<DiscriminantMethod>(document, d);
                    if (checkBox2.Checked) Execute<VietaMethod>(document, d);
                    if (checkBox3.Checked) Execute<GraphMethod>(document, d);


                }
                catch (Exception ex)
                {

                }
            }
            finally
            {
                CloseDocument(document, fileName);
                btnCreatePDF.Enabled = true;
                numericValueChanged(sender, e);
            }
        }

        private void numericValueChanged(object sender, EventArgs e)
        {
            var d = new DArg((double)numericUpDown1.Value, (double)numericUpDown2.Value, (double)numericUpDown3.Value);
            btnCreatePDF.Enabled = d.A != 0;
            var discriminant = d.B * d.B - 4 * d.A * d.C;

            if (d.A == 0)
            {
                label5.Text = "уравнение не является квадратным";
                return;
            }

            if (discriminant < 0)
            {
                UpdateResult(null);
                return;
            }

            if (discriminant > 0)
            {
                var x1 = (-d.B - Math.Sqrt(discriminant)) / (2 * d.A);
                var x2 = (-d.B + Math.Sqrt(discriminant)) / (2 * d.A);
                UpdateResult(new string[] { "x1=" + x1.ToString(), "x2=" + x2.ToString() });
                return;
            }

            var x = -d.B / (2 * d.A);
            UpdateResult(new string[] {"x1=x2="+ x.ToString() });

        }

        private void UpdateResult(string[] value)
        {
            if (value == null || value.Length==0)
            {
                label5.Text = "Уравнение не имеет действительных решений";
                return;
            }

            label5.Text = string.Join(", ", value);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            numericValueChanged(sender, e);
            pictureBox2.Paint += pictureBox2_Paint;

        }
        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            var d = new DArg((double)numericUpDown1.Value, (double)numericUpDown2.Value, (double)numericUpDown3.Value);
            DrawGraph(e, d);
        }

        public void DrawGraph(PaintEventArgs e, DArg arg)
        {
            var a = arg.A;
            var b = arg.B;
            var c = arg.C;
            Graphics g = e.Graphics;
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

            PointF[] points = new PointF[j+1];

            points[cent] = new PointF(kx+0, ky+0);
            var setk = 0F;
            for (int i = 1; i <= cent; i++)
            {
                var xx = (float)Math.Sqrt(dop / a)* koof;
                points[cent - i] = new PointF(kx + -xx, ky + dop*-1* koof);
                points[cent + i] = new PointF(kx + xx, ky + dop*-1* koof);
                dop += fl;

                setk = i * koof;
                //points[cent] = new PointF(kx + 0, ky + 0);
                PointF[] pointslinex = { new PointF((float) 0, ky + setk), new PointF((float)kx*2, ky + setk) };
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
                    var xx1 = i*100 * (i > 1 ? -1 : 1);
                    var yy1 = -c;
                    pointsline[i - 1] = new PointF((float)(kx + xx1 * koof), (float)((ky) - (yy1 * koof)));
                }
            }
            g.DrawLines(Pens.Blue, pointsline);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.Refresh();
        }
    }
}