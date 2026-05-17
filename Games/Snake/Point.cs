namespace Snake;

public struct Point
{
    #region Constrctors
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
    #endregion

    #region Properties
    public int X { get; set; }
    public int Y { get; set; }
    #endregion
}