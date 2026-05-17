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
    public virtual bool IsCollidingAtLocation(Vector2 myLocation, Sprite otherSprite, Vector2 otherSpriteLocation)
    {
        // ReSharper disable once ReplaceWithSingleAssignment.True
        var result = true;

        var myHalfSize = Size / 2;
        var otherHalfSize = otherSprite.Size / 2;

        if (myLocation.X - myHalfSize.X > otherSpriteLocation.X + otherHalfSize.X) result = false;
        if (otherSpriteLocation.X - otherHalfSize.X > myLocation.X + myHalfSize.X) result = false;

        if (myLocation.Y - myHalfSize.Y > otherSpriteLocation.Y + otherHalfSize.Y) result = false;
        if (otherSpriteLocation.Y - otherHalfSize.Y > myLocation.Y + myHalfSize.Y) result = false;

        return result;
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
    
    // ReSharper disable once InconsistentNaming
    protected Vector2? _textureCenter;
    #endregion
}