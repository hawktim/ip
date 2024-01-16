using System;
//using static iText.Svg.SvgConstants;
public class DArg: EventArgs
{
    public DArg(double a, double b, double c)
    {
        A = a;
        B = b;
        C = c;
    }

    public double A { get; }
    public double B { get; }
    public double C { get; }
}
