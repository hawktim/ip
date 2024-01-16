using System.IO;
using System.Drawing;
using Aspose.TeX.Plugins;


namespace MyWinFormsApp
{
    public abstract class ImageHelper
    {
        public static string GetValue(double value)
        {
            return value > 0 ? value.ToString() : "(" + value + ")";
        }

        public static Bitmap ImageToBitmap(byte[] imageBytes)
        {
            using (var mem = new MemoryStream(imageBytes))
            {
                return new Bitmap(mem);
            }
        }

        public static byte[] GetImageFormula(string formula)
        {
            var renderer = new MathRenderer();
            PngMathRendererOptions options = new PngMathRendererOptions()
            {

                //BackgroundColor = Color.White,
                //TextColor = Color.Black,
                Resolution = 1,
                Scale = 1000000,
                Margin = 10,
                Preamble = @"\usepackage{amsmath}\usepackage{amsfonts}\usepackage{amssymb}\usepackage{color}"
            };
            options.AddInputDataSource(new StringDataSource(@"\begin{equation*}" + formula + @"\end{equation*}"));

            // Создайте выходной поток для изображения формулы.
            //using (Stream stream = File.Open(@"C:\WORK\test\Latex\math-formula.png", FileMode.Create)) 
            using (Stream stream = new MemoryStream())
            {
                var s = new MathRenderer();
                //MathRenderer.Render(@"This is a sample formula $f(x) = x^2$ example.", stream, options,  out var size);
                options.AddOutputDataTarget(new StreamDataSource(stream));
                ResultContainer result = renderer.Process(options);
                byte[] bytes = new byte[stream.Length];
                stream.Position = 0;
                //return new Bitmap(stream);
                stream.Read(bytes, 0, bytes.Length);
                return bytes;
            }
        }
    }
}