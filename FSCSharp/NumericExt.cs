namespace FSCSharp;

public static class NumericExt
{
    #region Extension Methods
    public static int RoundToInt(this double me)
    {
        return (int)Math.Round(me);
    }
    public static int RoundToInt(this float me)
    {
        return (int)Math.Round(me);
    }

    public static double Clamp(this double me, double min, double max)
    {
        return Math.Clamp(me, min, max);
    }
    public static float Clamp(this float me, float min, float max)
    {
        return Math.Clamp(me, min, max);
    }
    public static int Clamp(this int me, int min, int max)
    {
        return Math.Clamp(me, min, max);
    }
    #endregion
}
