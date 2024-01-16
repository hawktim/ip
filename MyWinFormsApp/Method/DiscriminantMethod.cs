using iText.IO.Image;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using MyWinFormsApp;
using System;
using System.Drawing;
public class DiscriminantMethod
{
    public void Execute(Document document, DArg arg)
    {
        //Вычисление дискриминанта и выбор вывода 
        AddHeader(document, arg);
        var d = GetDiscriminant(arg);
        if (d > 0)
        {
            GreaterThanZero(document, arg);
            return;
        }

        if (d == 0)
        {
            EqualToZero(document, arg);
            return;
        }

        LessThanZero(document, arg);
    }

    protected double GetDiscriminant(DArg arg)
    {
        return arg.B * arg.B - 4 * arg.A * arg.C;
    }

    protected virtual string NameMethod { get; } = "По формуле дискриминанта";
    protected virtual bool NotDescr { get; } = false;

    public virtual void AddHeader(Document document, DArg arg)
    {
        LineSeparator ls = new LineSeparator(new SolidLine());
        document.Add(ls);
        Paragraph subheader = new Paragraph(NameMethod)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(12)
                   .SetBold();
        document.Add(subheader);
        document.Add(ls);

        var img1 = new iText.Layout.Element.Image(
                       ImageDataFactory.Create(
                           ImageHelper.ImageToBitmap(ImageHelper.GetImageFormula(arg.A + @"x^2"+(arg.B<0?"":"+") + arg.B + "x"+ (arg.C < 0 ? "" : "+") + arg.C + "=0"))
                           , Color.White, false)
                       );
        img1.SetHeight(20);

        var d = GetDiscriminant(arg);
        var znak = (d == 0) ? "=" : (d > 0) ? ">" : "<";
        var img2 = new iText.Layout.Element.Image(
                       ImageDataFactory.Create(
                           ImageHelper.ImageToBitmap(ImageHelper.GetImageFormula("D=" + ImageHelper.GetValue(arg.B) + @"^2-4\cdot" + ImageHelper.GetValue(arg.A) + @"\cdot" + ImageHelper.GetValue(arg.C) + "=" + GetDiscriminant(arg) + znak + "0"))
                           , Color.White, false)
                       );
        img2.SetHeight(20);

        document.Add(new Paragraph().Add(img1));
        if (!NotDescr)
        {
            document.Add(new Paragraph("Найдем дискриминант квадратного уравнения:"));
            document.Add(new Paragraph().Add(img2));
        }
    }
    /// <summary>
    /// больше нуля
    /// </summary>
    public virtual void GreaterThanZero(Document document, DArg arg)
    {
        var d = GetDiscriminant(arg);
        var x1 = (-arg.B - Math.Sqrt(d)) / (2 * arg.A);
        var x2 = (-arg.B + Math.Sqrt(d)) / (2 * arg.A);

        var formula = @"x_1=\frac{"+(-arg.B)+@"-\sqrt{" +d+ @"}}{2\cdot" + ImageHelper.GetValue(arg.A)+"}=" + x1.ToString("#0.0####");
        var img1 = new iText.Layout.Element.Image(
                       ImageDataFactory.Create(
                           ImageHelper.ImageToBitmap(ImageHelper.GetImageFormula(formula))
                           , Color.White, false)
                       );
        img1.SetHeight(50);
        var formula1 = @"x_2=\frac{" + (-arg.B) + @"+\sqrt{" + d + @"}}{2\cdot" + ImageHelper.GetValue(arg.A) + "}="+x2.ToString("#0.0####");
        var img2 = new iText.Layout.Element.Image(
                       ImageDataFactory.Create(
                           ImageHelper.ImageToBitmap(ImageHelper.GetImageFormula(formula1))
                           , Color.White, false)
                       );
        img2.SetHeight(50);

        var formula2 = @"x_1=" + x1.ToString("#0.0####") + "; x_2 = " + x2.ToString("#0.0####");
        var img3 = new iText.Layout.Element.Image(
                       ImageDataFactory.Create(
                           ImageHelper.ImageToBitmap(ImageHelper.GetImageFormula(formula2))
                           , Color.White, false)
                       );
        img3.SetHeight(20);


        document.Add(new Paragraph().Add(img1));
        document.Add(new Paragraph().Add(img2));
        document.Add(new Paragraph("Ответ:").SetFontSize(12));
        document.Add(new Paragraph().Add(img3));
    }

    /// <summary>
    /// Равно нулю
    /// </summary>
    public virtual void EqualToZero(Document document, DArg arg)
    {
        var x1 = (-arg.B) / (2 * arg.A);

        var formula = @"x_1=x_2=\frac{" + (-arg.B) + @"}{2\cdot" + ImageHelper.GetValue(arg.A) + "}=" + x1.ToString("#0.0####");
        var img1 = new iText.Layout.Element.Image(
                       ImageDataFactory.Create(
                           ImageHelper.ImageToBitmap(ImageHelper.GetImageFormula(formula))
                           , Color.White, false)
                       );
        img1.SetHeight(50);
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
    public virtual void LessThanZero(Document document, DArg arg)
    {
        document.Add(new Paragraph("Ответ: уравнение не имеет корней в действительных числах").SetFontSize(12));
    }
}
