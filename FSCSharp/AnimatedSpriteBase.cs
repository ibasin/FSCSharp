using System.Numerics;
using Raylib_cs;

namespace FSCSharp;

public abstract class AnimatedSpriteBase : SpriteBase
{
    #region Constructors
    protected AnimatedSpriteBase(float scale, float timePerFrame) : base(scale)
    {
        TimePerFrame = timePerFrame;
    }
    #endregion

    #region Methods
    public virtual void StartAnimation(bool looping = false)
    {
        TimeElapsed = 0;
        IsAnimationStopped = false;
        Looping = looping;
    }
    public virtual bool DrawAnimation(Vector2 location, float delta)
    {
        TimeElapsed += delta;

        var animationLength = CalcAnimationLength();
        if (!Looping && TimeElapsed > animationLength)
        {
            StopAnimation();
            return true;
        }
        
        var timeElapsedAdjusted = TimeElapsed % animationLength;
        var frameIdx = (int)(timeElapsedAdjusted / TimePerFrame);

        return Draw(location, frameIdx);
    }
    public virtual void StopAnimation()
    {
        IsAnimationStopped = true;
        TimeElapsed = 0;
    }

    public abstract bool Draw(Vector2 location, int frameIdx);
    public virtual bool Erase(Vector2 location, Color bgColor)
    {
        var result = IsFullyOnScreenAtLocation(location);
        var rect = new Rectangle(location, Size);
        Raylib.DrawRectanglePro(rect, Size / 2, Rotation, bgColor);
        return result;
    }

    public virtual float CalcAnimationLength()
    {
        return TimePerFrame * FramesCount;
    }
    public abstract int FramesCount { get; }

    public void ResetSize()
    {
        _size = null;
    }
    // ReSharper disable once InconsistentNaming
    protected Vector2? _size;
    #endregion

    #region Properties
    public bool IsAnimationStopped { get; protected set; } = true;
    public float TimePerFrame { get; set; }
    public float TimeElapsed { get; set; }
    public bool Looping { get; set; }
    #endregion
}