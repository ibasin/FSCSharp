using System.Numerics;
using Raylib_cs;

namespace FSCSharp;

public class Sprite : SpriteBase
{
    #region Constructors
    public Sprite(Texture2D texture, float scale = 1.0f) : base(scale)
    {
        TexturePlus = new Texture2DPlus(texture);
    }
    public override void Dispose()
    {
        TexturePlus.Dispose();
    }
    #endregion

    #region Methods
    public virtual bool Draw(Vector2 location)
    {
        var result = IsFullyOnScreenAtLocation(location);

        if (_sourceRect == null)
        {
            _sourceRect = new Rectangle(0f, 0f, TexturePlus.Texture.Width, TexturePlus.Texture.Height);
            if (FlipHorizontally) _sourceRect = _sourceRect.FlipHorizontally();
            if (FlipVertically) _sourceRect = _sourceRect.FlipVertically();
        }

        var destRect = new Rectangle(location, Size);
        _textureCenter ??= Size / 2;
        Raylib.DrawTexturePro(TexturePlus.Texture, _sourceRect!.Value, destRect, _textureCenter.Value, Rotation, TintColor);

        return result;
    }
    public virtual bool Erase(Vector2 location, Color bgColor)
    {
        var result = IsFullyOnScreenAtLocation(location);
        var rect = new Rectangle(location, Size);

        Raylib.DrawRectanglePro(rect, Size/2, Rotation, bgColor);
        return result;
    }

    public override Vector2 Size => new(TexturePlus.Texture.Width * HScale, TexturePlus.Texture.Height * VScale);
    #endregion

    #region Properties
    public Texture2DPlus TexturePlus { get; set; }
    private Rectangle? _sourceRect;
    public override Image CurrentImage => TexturePlus.Image;
    #endregion
}