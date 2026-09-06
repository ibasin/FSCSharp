using System.Numerics;
using Raylib_cs;

namespace FSCSharp;

public class AnimatedArraySprite : AnimatedSpriteBase
{
    #region Constructors
    public AnimatedArraySprite(Texture2D[] frames, float timePerFrame, float scale = 1.0f) : base(scale, timePerFrame)
    {
        Frames = frames;
        _images = new Image[frames.Length];
        _imageLoaded = new bool[frames.Length];
    }
    public override void Dispose()
    {
        for (var i = 0; i < Frames.Length; i++)
        {
            Raylib.UnloadTexture(Frames[i]);
            if (_imageLoaded[i]) Raylib.UnloadImage(_images[i]);
        }
    }
    #endregion

    #region Methods
    public override bool Draw(Vector2 location, int frameIdx)
    {
        var result = IsFullyOnScreenAtLocation(location);
        
        var sourceRect = new Rectangle(0f, 0f, Frames[frameIdx].Width, Frames[frameIdx].Height);
        if (FlipHorizontally) sourceRect = sourceRect.FlipHorizontally();
        if (FlipVertically) sourceRect = sourceRect.FlipVertically();

        var destRect = new Rectangle(location, Size);
        _textureCenter ??= Size / 2;
        Raylib.DrawTexturePro(Frames[frameIdx], sourceRect, destRect, _textureCenter.Value, Rotation, TintColor);

        return result;
    }

    public override int FramesCount => Frames.Length;
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
    #endregion

    #region Properties
    // ReSharper disable once InconsistentNaming
    public readonly Texture2D[] Frames;

    public override ref Image CurrentImage // => ref Frames[CalculateAnimationFrameIdx()];
    {
        get
        {
            var frameIdx = CalculateAnimationFrameIdx();
            if (!_imageLoaded[frameIdx])
            {
                _images[frameIdx] = Raylib.LoadImageFromTexture(Frames[frameIdx]);
                _imageLoaded[frameIdx] = true;
            }
            return ref _images[frameIdx];
        }
    }

    private readonly Image[] _images;
    private readonly bool[] _imageLoaded;
    #endregion
}