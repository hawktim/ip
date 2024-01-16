using iText.Layout;
using iText.Layout.Element;
public class GraphMethod : DiscriminantMethod
{
    protected override string NameMethod { get; } = "Графический способ";
    protected override bool NotDescr { get; } = true;
    /// <summary>
    /// больше нуля
    /// </summary>
    public override void GreaterThanZero(Document document, DArg arg)
    {

    }

    /// <summary>
    /// Равно нулю
    /// </summary>
    public override void EqualToZero(Document document, DArg arg)
    {

    }

    /// <summary>
    /// меньше нуля
    /// </summary>
    public override void LessThanZero(Document document, DArg arg)
    {
        document.Add(new Paragraph("Ответ: уравнение не имеет корней в действительных числах"));
    }
}
