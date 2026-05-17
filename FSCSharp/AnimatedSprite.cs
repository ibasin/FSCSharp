using System.Numerics;
using Raylib_cs;

namespace FSCSharp;

public class AnimatedSprite : SpriteBase
{
    #region Constructors
    public AnimatedSprite(Texture2D[] frames, float scale = 1.0f) : base(scale)
    {
        Frames = frames;
    }
    public override void Dispose()
    {
        foreach(var texture in Frames)
        {
            Raylib.UnloadTexture(texture);
        }
    }
    #endregion

    #region Methods
    public virtual bool Draw(Vector2 location, int frameIdx)
    {
        var result = IsFullyOnScreenAtLocation(location);
        
        var sourceRect = new Rectangle(0f, 0f, Frames[frameIdx].Width, Frames[frameIdx].Height);
        var destRect = new Rectangle(location, Size);
        _textureCenter ??= Size / 2;
        Raylib.DrawTexturePro(Frames[frameIdx], sourceRect, destRect, _textureCenter.Value, Rotation, TintColor);

        return result;
    }
    public virtual bool Erase(Vector2 location, Color bgColor)
    {
        var result = IsFullyOnScreenAtLocation(location);
        var rect = new Rectangle(location, Size);
        Raylib.DrawRectanglePro(rect, Size / 2, Rotation, bgColor);
        return result;
    }

    public void ResetSize()
    {
        _size = null;
    }
    public override Vector2 Size
    {
        get
        {
            if (_size == null)
            {
                var maxX = 0f;
                var maxY = 0f;

                foreach (var frame in Frames)
                {
                    var frameX = (int)(frame.Width * HScale);
                    var frameY = (int)(frame.Height * VScale);
                    
                    if (frameX > maxX) maxX = frameX;
                    if (frameY > maxY) maxY = frameY;
                }

                _size = new Vector2(maxX, maxY);
            }
            return _size.Value;
        }
    }
    private Vector2? _size;
    #endregion

    #region Properties
    public Texture2D[] Frames { get; set; }
    #endregion
}