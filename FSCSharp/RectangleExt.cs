using Raylib_cs;

namespace FSCSharp;

public static class RectangleExt
{
    public static Rectangle FlipHorizontally(this Rectangle me)
    {
        return new Rectangle(me.Position, -me.Width, me.Height);
    }
    public static Rectangle FlipVertically(this Rectangle me)
    {
        return new Rectangle(me.Position, me.Width, -me.Height);
    }
    public static Rectangle? FlipHorizontally(this Rectangle? me)
    {
        if (me == null) return null;
        return new Rectangle(me.Value.Position, -me.Value.Width, me.Value.Height);
    }
    public static Rectangle? FlipVertically(this Rectangle? me)
    {
        if (me == null) return null;
        return new Rectangle(me.Value.Position, me.Value.Width, -me.Value.Height);
    }
}