using System.Numerics;
using Raylib_cs;

namespace FSCSharp;

public class Sprite : SpriteBase
{
    #region Constructors
    public Sprite(Texture2D texture, float scale = 1.0f) : base(scale)
    {
        Texture = texture;
    }
    public override void Dispose()
    {
        Raylib.UnloadTexture(Texture);
    }
    #endregion

    #region Methods
    public virtual bool Draw(Vector2 location)
    {
        var result = IsFullyOnScreenAtLocation(location);

        if (_sourceRect == null)
        {
            _sourceRect = new Rectangle(0f, 0f, Texture.Width, Texture.Height);
            if (FlipHorizontally) _sourceRect = _sourceRect.FlipHorizontally();
            if (FlipVertically) _sourceRect = _sourceRect.FlipVertically();
        }

        var destRect = new Rectangle(location, Size);
        _textureCenter ??= Size / 2;
        Raylib.DrawTexturePro(Texture, _sourceRect!.Value, destRect, _textureCenter.Value, Rotation, TintColor);

        return result;
    }
    public virtual bool Erase(Vector2 location, Color bgColor)
    {
        var result = IsFullyOnScreenAtLocation(location);
        var rect = new Rectangle(location, Size);

        Raylib.DrawRectanglePro(rect, Size/2, Rotation, bgColor);
        return result;
    }

    public override Vector2 Size => new(Texture.Width * HScale, Texture.Height * VScale);
    #endregion

    #region Properties
    public Texture2D Texture;
    private Image _image;
    private bool _imageLoaded;

    private Rectangle? _sourceRect;

    public override ref Image CurrentImage
    {
        get
        {
            if (!_imageLoaded)
            {
                _image = Raylib.LoadImageFromTexture(Texture);
                _imageLoaded = true;
            }
            return ref _image;
        }
    }
    #endregion
}