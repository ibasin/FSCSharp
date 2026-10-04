using System.Numerics;

namespace FSCSharp;

public enum Go { Up, Right, Down, Left }

public static class GoExt
{
    public static Vector2 ToVector2(this Go me)
    {
        return me switch
        {
            Go.Up => new Vector2(0, -1),
            Go.Right => new Vector2(1, 0),
            Go.Down => new Vector2(0, 1),
            Go.Left => new Vector2(-1, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(me), me, null)
        };
    }
}