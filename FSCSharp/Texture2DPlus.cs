//using Raylib_cs;

//namespace FSCSharp;

//public class Texture2DPlus : IDisposable
//{
//    #region Constructors
//    public Texture2DPlus(Texture2D texture)
//    {
//        Texture = texture;
//    }
//    #endregion

//    public void Dispose()
//    {
//        Raylib.UnloadTexture(Texture);
//        if (_imageExists) Raylib.UnloadImage(_image);
//    }

//    #region Properties
//    //this is intentionally not a property b/c this is a struct and we want to avoid copying it around
//    public Texture2D Texture; 

//    public ref Image Image
//    {
//        get
//        {
//            if (_imageExists)
//            {
//                _image = Raylib.LoadImageFromTexture(Texture);
//                _imageExists = true;
//            }
//            return ref _image;
//        }
//    }
//    private Image _image;
//    private bool _imageExists;
//    #endregion
//}