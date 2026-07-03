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
    public virtual bool IsCollidingAtLocation(Vector2 myLocation, Vector2 vector)
    {
        // ReSharper disable once ReplaceWithSingleAssignment.True
        var result = true;

        var halfSize = Size / 2;

        if (myLocation.X - halfSize.X <= vector.X) result = false;
        if (myLocation.X + halfSize.X >= vector.X) result = false;

        if (myLocation.Y - halfSize.Y <= vector.Y) result = false;
        if (myLocation.Y + halfSize.Y >= vector.Y) result = false;

        return result;
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
        if (myX0 >= otherX0 && myX0 <= otherX1) xIntersect = true;
        if (otherX0 >= myX0 && otherX0 <= myX1) xIntersect = true;
        if (myX1 >= otherX0 && myX1 <= otherX1) xIntersect = true;
        if (otherX1 >= myX0 && otherX1 <= myX1) xIntersect = true;

        var yIntersect = false;
        if (myY0 >= otherY0 && myY0 <= otherY1) yIntersect = true;
        if (otherY0 >= myY0 && otherY0 <= myY1) yIntersect = true;
        if (myY1 >= otherY0 && myY1 <= otherY1) yIntersect = true;
        if (otherY1 >= myY0 && otherY1 <= myY1) yIntersect = true;

        return xIntersect && yIntersect;

        // ReSharper disable once ReplaceWithSingleAssignment.True
        //var result = true;


        //if (myLocation.X - myHalfSize.X > otherSpriteLocation.X + otherHalfSize.X) result = false;
        //if (otherSpriteLocation.X - otherHalfSize.X > myLocation.X + myHalfSize.X) result = false;

        //if (myLocation.Y - myHalfSize.Y > otherSpriteLocation.Y + otherHalfSize.Y) result = false;
        //if (otherSpriteLocation.Y - otherHalfSize.Y > myLocation.Y + myHalfSize.Y) result = false;

        //return result;
    }
    public virtual void SetScalesByTargetRectangleSize(float x, float y)
    {
        HScale = x / Size.X;
        VScale = y / Size.Y;
    }
    public abstract Vector2 Size { get; }
    #endregion

    #region Properties
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