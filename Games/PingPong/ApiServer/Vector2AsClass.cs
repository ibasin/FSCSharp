using System.Numerics;

namespace PingPong.ApiServer;

public static class Vector2Ex
{
    #region Methods
    public static Vector2AsClass ToVector2AsClass(this Vector2 me)
    {
        return new Vector2AsClass { X = me.X, Y = me.Y };
    }
    #endregion
}

public class Vector2AsClass
{
    #region Methods
    public Vector2 ToVector2()
    {
        return new Vector2(X, Y);
    }
    #endregion

    #region Properties
    public float X { get; set; }
    public float Y { get; set; }
    #endregion
}
