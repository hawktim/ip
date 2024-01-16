using iText.IO.Image;
using iText.Layout;
using iText.Layout.Element;
using MyWinFormsApp;
using System;
using System.Drawing;
public class VietaMethod : DiscriminantMethod
{
    protected override string NameMethod { get; } = "Теорема Виета";
    protected override bool NotDescr { get; } = true;
    /// <summary>
    /// больше нуля
    /// </summary>
    public override void GreaterThanZero(Document document, DArg arg)
    {
        var d = GetDiscriminant(arg);
        var x1 = (-arg.B - Math.Sqrt(d)) / (2 * arg.A);
        var x2 = (-arg.B + Math.Sqrt(d)) / (2 * arg.A);

        var formula = @"\begin{cases}x_1 + x_2 = \frac{" + -arg.B + "}{" + arg.A + @"}\\x_1 \cdot x_2 = \frac{" + arg.C + @"}{" + arg.A + @"}\end{cases}\begin{array}{|c} x_1="+x1.ToString("#0.0####") + @" \\  x_2="+x2.ToString("#0.0####") + @"\end{array}";
        var img1 = new iText.Layout.Element.Image(
                       ImageDataFactory.Create(
                           ImageHelper.ImageToBitmap(ImageHelper.GetImageFormula(formula))
                           , Color.White, false)
                       );
        img1.SetHeight(70);
        var formula2 = @"x_1=" + x1.ToString("#0.0####") + "; x_2 = " + x2.ToString("#0.0####");
        var img3 = new iText.Layout.Element.Image(
                       ImageDataFactory.Create(
                           ImageHelper.ImageToBitmap(ImageHelper.GetImageFormula(formula2))
                           , Color.White, false)
                       );
        img3.SetHeight(20);
        document.Add(new Paragraph().Add(img1));
        document.Add(new Paragraph("Ответ:").SetFontSize(12));
        document.Add(new Paragraph().Add(img3));
    }

    /// <summary>
    /// Равно нулю
    /// </summary>
    public override void EqualToZero(Document document, DArg arg)
    {
        var x1 = (-arg.B) / (2 * arg.A);
        var formula = @"\begin{cases}x_1 + x_2 = \frac{" + -arg.B + "}{" + arg.A + @"}\\x_1 \cdot x_2 = \frac{" + arg.C + @"}{" + arg.A + @"}\end{cases}\begin{array}{|} \\ \\ \end{array}\begin{array} & x_1=x_2=" + x1.ToString("#0.0####") + @"\end{array}";
        var img1 = new iText.Layout.Element.Image(
                       ImageDataFactory.Create(
                           ImageHelper.ImageToBitmap(ImageHelper.GetImageFormula(formula))
                           , Color.White, false)
                       );
        img1.SetHeight(70);
        var formula2 = @"x_1=x_2=" + x1.ToString("#0.0####");
        var img3 = new iText.Layout.Element.Image(
                       ImageDataFactory.Create(
                           ImageHelper.ImageToBitmap(ImageHelper.GetImageFormula(formula2))
                           , Color.White, false)
                       );
        img3.SetHeight(20);
        document.Add(new Paragraph().Add(img1));
        document.Add(new Paragraph("Ответ:").SetFontSize(12));
        document.Add(new Paragraph().Add(img3));
    }

    /// <summary>
    /// меньше нуля
    /// </summary>
    public override void LessThanZero(Document document, DArg arg)
    {
        var formula = @"\begin{cases}x_1 + x_2 = \frac{" + -arg.B + "}{" + arg.A + @"}\\x_1 \cdot x_2 = \frac{" + arg.C + @"}{" + arg.A + @"}\end{cases}\begin{array}{|} \\ \\ \end{array}\begin{array} & \varnothing \end{array}";
        var img1 = new iText.Layout.Element.Image(
                       ImageDataFactory.Create(
                           ImageHelper.ImageToBitmap(ImageHelper.GetImageFormula(formula))
                           , Color.White, false)
                       );
        img1.SetHeight(70);
        document.Add(new Paragraph().Add(img1));
        document.Add(new Paragraph("Ответ: уравнение не имеет корней в действительных числах").SetFontSize(12));
    }
}
