using System.Numerics;
using Raylib_cs;

namespace FSCSharp;

public abstract class SpriteBase : IDisposable
{
    #region Constructors
    protected SpriteBase(float scale)
    {
        HScale = VScale = scale;
    }
    public abstract void Dispose();
    #endregion

    #region Methods
    public virtual bool IsFullyOnScreenAtLocation(Vector2 location)
    {
        // ReSharper disable once ReplaceWithSingleAssignment.True
        var result = true;

        var halfSize = Size / 2;
        
        if (location.X - halfSize.X < 0) result = false;
        if (location.X + halfSize.X > Game.CurrentInternal.WindowWidth) result = false;

        if (location.Y - halfSize.Y  < 0) result = false;
        if (location.Y + halfSize.Y > Game.CurrentInternal.WindowHeight) result = false;

        return result;
    }

    public virtual bool IsCollidingByPixelAtLocation(Vector2 myLocation, Vector2 otherLocation)
    {
        var imgA = Raylib.ImageCopy(CurrentImage);

        try
        {
            var alphaThreshold = 10;

            //return IsPixelPerfectCollision(CurrentImage, myLocation, otherSprite.CurrentImage, otherSpriteLocation, 10);
            var myHalfSize = Size / 2;

            var posA = new Vector2(myLocation.X - myHalfSize.X, myLocation.Y - myHalfSize.Y);
            Raylib.ImageResize(ref imgA, (int)(imgA.Width * HScale), (int)(imgA.Height * VScale));
            Rectangle recA = new(posA.X, posA.Y, imgA.Width, imgA.Height);

            if (!Raylib.CheckCollisionPointRec(otherLocation, recA)) return false;

            int ax = (int)(otherLocation.X - posA.X);
            int ay = (int)(otherLocation.Y - posA.Y);
            Color ca = Raylib.GetImageColor(imgA, ax, ay);

            return ca.A >= alphaThreshold;
        }
        finally
        {
            Raylib.UnloadImage(imgA);
        }
    }
    public virtual bool IsCollidingByPixelAtLocation(Vector2 myLocation, SpriteBase otherSprite, Vector2 otherSpriteLocation)
    {
        var imgA = Raylib.ImageCopy(CurrentImage);
        var imgB = Raylib.ImageCopy(otherSprite.CurrentImage);

        try
        {
            var alphaThreshold = 10;

            //return IsPixelPerfectCollision(CurrentImage, myLocation, otherSprite.CurrentImage, otherSpriteLocation, 10);
            var myHalfSize = Size / 2;
            var otherHalfSize = otherSprite.Size / 2;

            var posA = new Vector2(myLocation.X - myHalfSize.X, myLocation.Y - myHalfSize.Y);
            var posB = new Vector2(otherSpriteLocation.X - otherHalfSize.X, otherSpriteLocation.Y - otherHalfSize.Y);


            Raylib.ImageResize(ref imgA, (int)(imgA.Width * HScale), (int)(imgA.Height * VScale));
            Raylib.ImageResize(ref imgB, (int)(imgB.Width * HScale), (int)(imgB.Height * VScale));

            Rectangle recA = new(posA.X, posA.Y, imgA.Width, imgA.Height);
            Rectangle recB = new(posB.X, posB.Y, imgB.Width, imgB.Height);

            if (!Raylib.CheckCollisionRecs(recA, recB)) return false;

            int left = (int)MathF.Max(recA.X, recB.X);
            int right = (int)MathF.Min(recA.X + recA.Width, recB.X + recB.Width);
            int top = (int)MathF.Max(recA.Y, recB.Y);
            int bottom = (int)MathF.Min(recA.Y + recA.Height, recB.Y + recB.Height);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    int ax = x - (int)posA.X;
                    int ay = y - (int)posA.Y;
                    int bx = x - (int)posB.X;
                    int by = y - (int)posB.Y;

                    Color ca = Raylib.GetImageColor(imgA, ax, ay);
                    Color cb = Raylib.GetImageColor(imgB, bx, by);

                    if (ca.A >= alphaThreshold && cb.A >= alphaThreshold) return true;
                }
            }

            return false;
        }
        finally
        {
            Raylib.UnloadImage(imgA);
            Raylib.UnloadImage(imgB);
        }
    }

    public virtual bool IsCollidingAtLocation(Vector2 myLocation, Vector2 vector)
    {
        var myHalfSize = Size / 2;

        var myX0 = myLocation.X - myHalfSize.X;
        var myX1 = myLocation.X + myHalfSize.X;
        var myY0 = myLocation.Y - myHalfSize.Y;
        var myY1 = myLocation.Y + myHalfSize.Y;

        var xIntersect = false;
        if (myX0 <= vector.X && vector.X <= myX1) xIntersect = true;

        var yIntersect = false;
        if (myY0 <= vector.Y && vector.Y <= myY1) yIntersect = true;

        return xIntersect && yIntersect;
    }
    public virtual bool IsCollidingAtLocation(Vector2 myLocation, SpriteBase otherSprite, Vector2 otherSpriteLocation)
    {
        var myHalfSize = Size / 2;
        var otherHalfSize = otherSprite.Size / 2;

        var myX0 = myLocation.X - myHalfSize.X;
        var myX1 = myLocation.X + myHalfSize.X;
        var myY0 = myLocation.Y - myHalfSize.Y;
        var myY1 = myLocation.Y + myHalfSize.Y;

        var otherX0 = otherSpriteLocation.X - otherHalfSize.X;
        var otherX1 = otherSpriteLocation.X + otherHalfSize.X;
        var otherY0 = otherSpriteLocation.Y - otherHalfSize.Y;
        var otherY1 = otherSpriteLocation.Y + otherHalfSize.Y;

        var xIntersect = false;
        if (otherX0 <= myX0 && myX0 <= otherX1) xIntersect = true;
        if (otherX0 <= myX1 && myX1 <= otherX1) xIntersect = true;
        if (myX0 <= otherX0 && otherX0 <= myX1) xIntersect = true;
        if (myX0 <= otherX1 && otherX1 <= myX1) xIntersect = true;

        var yIntersect = false;
        if (otherY0 <= myY0 && myY0 <= otherY1) yIntersect = true;
        if (otherY0 <= myY1 && myY1 <= otherY1) yIntersect = true;
        if (myY0 <= otherY0 && otherY0 <= myY1) yIntersect = true;
        if (myY0 <= otherY1 && otherY1 <= myY1) yIntersect = true;

        return xIntersect && yIntersect;
    }
    private bool IsPixelPerfectCollision(Image imgA, Vector2 posA, Image imgB, Vector2 posB, byte alphaThreshold = 1)
    {
        Rectangle recA = new(posA.X, posA.Y, imgA.Width, imgA.Height);
        Rectangle recB = new(posB.X, posB.Y, imgB.Width, imgB.Height);

        if (!Raylib.CheckCollisionRecs(recA, recB)) return false;

        int left = (int)MathF.Max(recA.X, recB.X);
        int right = (int)MathF.Min(recA.X + recA.Width, recB.X + recB.Width);
        int top = (int)MathF.Max(recA.Y, recB.Y);
        int bottom = (int)MathF.Min(recA.Y + recA.Height, recB.Y + recB.Height);

        for (int y = top; y < bottom; y++)
        {
            for (int x = left; x < right; x++)
            {
                int ax = x - (int)posA.X;
                int ay = y - (int)posA.Y;
                int bx = x - (int)posB.X;
                int by = y - (int)posB.Y;

                Color ca = Raylib.GetImageColor(imgA, ax, ay);
                Color cb = Raylib.GetImageColor(imgB, bx, by);

                if (ca.A >= alphaThreshold && cb.A >= alphaThreshold) return true;
            }
        }

        return false;
    }

    public virtual void SetScalesByTargetRectangleSize(float x, float y)
    {
        HScale = x / Size.X;
        VScale = y / Size.Y;
    }
    public abstract Vector2 Size { get; }
    #endregion

    #region Properties
    public abstract ref Image CurrentImage { get; }
    
    public float HScale { get; set; }
    public float VScale { get; set; }
    public Color TintColor { get; set; } = Color.White;

    public float Rotation { get; set; }
    public bool FlipHorizontally { get; set; }
    public bool FlipVertically { get; set; }

    // ReSharper disable once InconsistentNaming
    protected Vector2? _textureCenter;
    #endregion
}