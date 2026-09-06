using Raylib_cs;
using System.Numerics;

namespace FSCSharp;

public class AnimatedTilesSprite : AnimatedSpriteBase
{
    #region Constructors
    public AnimatedTilesSprite(Texture2D tilesTexture, Vector2[] frameRectCenters, Vector2 frameRectSize, float timePerFrame, float scale = 1.0f) : base(scale, timePerFrame)
    {
        TilesTexture = tilesTexture;
        
        var frameRects = new Rectangle[frameRectCenters.Length];
        for (var i = 0; i < frameRects.Length; i++)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            frameRects[i] = new Rectangle(frameRectCenters[i] - frameRectSize/2, frameRectSize);
        }
        FrameRects = frameRects;

        _images = new Image[frameRects.Length];
        _imageLoaded = new bool[frameRects.Length];

    }
    public AnimatedTilesSprite(Texture2D tilesTexture, Rectangle[] frameRects, float timePerFrame, float scale = 1.0f) : base(scale, timePerFrame)
    {
        TilesTexture = tilesTexture;
        FrameRects = frameRects;

        _images = new Image[frameRects.Length];
        _imageLoaded = new bool[frameRects.Length];
    }
    public override void Dispose()
    {
        Raylib.UnloadTexture(TilesTexture);
    }
    #endregion

    #region Overrides
    public override Vector2 Size
    {
        get
        {
            if (_size == null)
            {
                var maxX = 0f;
                var maxY = 0f;

                foreach (var frameRect in FrameRects)
                {
                    var frameX = (int)frameRect.Size.X * HScale;
                    var frameY = (int)(frameRect.Size.Y * VScale);

                    if (frameX > maxX) maxX = frameX;
                    if (frameY > maxY) maxY = frameY;
                }

                _size = new Vector2(maxX, maxY);
            }
            return _size.Value;
        }
    }
    public override int FramesCount => FrameRects.Length;
    #endregion

    #region Methods
    public void DrawAllTiles()
    {
        DrawAllTiles(Color.Lime);
    }
    public void DrawAllTiles(Color borderColor)
    {
        var location = Size / 2;

        for (var i = 0; i < FramesCount; i++)
        {
            Draw(location, i);
            var rect = new Rectangle((int)(location.X - Size.X / 2), (int)(location.Y - Size.Y / 2), (int)Size.X, (int)Size.Y);
            Raylib.DrawRectangleLinesEx(rect, 1, borderColor);

            var proposedLocation = location.Move(Go.Right, Size.X);

            if (proposedLocation.X + Size.X <= Game.CurrentInternal.WindowWidth) location = proposedLocation;
            else location = new Vector2(Size.X / 2, location.Y + Size.Y);
        }
    }
    public override bool Draw(Vector2 location, int frameIdx)
    {
        var result = IsFullyOnScreenAtLocation(location);

        var sourceRect = FrameRects[frameIdx];
        if (FlipHorizontally) sourceRect = sourceRect.FlipHorizontally();
        if (FlipVertically) sourceRect = sourceRect.FlipVertically();

        var destRect = new Rectangle(location, Size);
        _textureCenter ??= Size / 2;
        Raylib.DrawTexturePro(TilesTexture, sourceRect, destRect, _textureCenter.Value, Rotation, TintColor);

        return result;
    }
    #endregion

    #region Properties
    // ReSharper disable once InconsistentNaming
    public Texture2D TilesTexture;
    
    private Image _tilesImage;
    private bool _tilesImageLoaded;

    // ReSharper disable once InconsistentNaming
    public readonly Rectangle[] FrameRects;
    
    public override ref Image CurrentImage
    {
        get
        {
            var frameIdx = CalculateAnimationFrameIdx();
            
            if (!_imageLoaded[frameIdx])
            {
                if (!_tilesImageLoaded)
                {
                    _tilesImage = Raylib.LoadImageFromTexture(TilesTexture);
                    _tilesImageLoaded = true;
                }
                
                // Make a copy so the original isn't modified
                Image tileImage = Raylib.ImageCopy(_tilesImage);

                // Crop to just the tile
                Raylib.ImageCrop(ref tileImage, FrameRects[frameIdx]);

                _images[frameIdx] = tileImage;

                //Raylib.UnloadImage(tileImage);
            }

            return ref _images[frameIdx];
        }
    }

    private readonly Image[] _images;
    private readonly bool[] _imageLoaded;
    #endregion
}