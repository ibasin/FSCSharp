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
    }
    public AnimatedTilesSprite(Texture2D tilesTexture, Rectangle[] frameRects, float timePerFrame, float scale = 1.0f) : base(scale, timePerFrame)
    {
        TilesTexture = tilesTexture;
        FrameRects = frameRects;
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
    public Texture2D TilesTexture { get; set; }
    public Rectangle[] FrameRects { get; set; }
    #endregion
}