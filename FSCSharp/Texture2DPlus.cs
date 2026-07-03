using Raylib_cs;

namespace FSCSharp;

public class Texture2DPlus : IDisposable
{
    #region Constructors
    public Texture2DPlus(Texture2D texture)
    {
        Texture = texture;
    }
    #endregion

    public void Dispose()
    {
        Raylib.UnloadTexture(Texture);
        if (_image.HasValue) Raylib.UnloadImage(_image.Value);
    }

    #region Properties
    //this is intentionally not a property b/c this is a struct and we want to avoid copying it around
    public Texture2D Texture; 

    public Image Image 
    {
        get
        {
            if (!_image.HasValue) _image = Raylib.LoadImageFromTexture(Texture);
            return _image.Value;
        }
    }
    private Image? _image;
    #endregion
}