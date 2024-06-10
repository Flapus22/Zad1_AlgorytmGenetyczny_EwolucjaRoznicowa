namespace AlgorytmGenetyczny;

internal static class Helper
{
    public static double InverseLerp(double a, double b, double value)
    {
        if ((b - a) == 0)
        {
            return 1;
        }
        return (value - a) / (b - a);
    }

}
