using System.Numerics;
using Raylib_cs;

namespace FSCSharp;

public class Model3D : IDisposable
{
    #region Constructors
    public Model3D(Model model, Texture2D texture, float scale = 1.0f)
    {
        Model = model;
        Texture = texture;
        Scale = scale;
        
        Raylib.SetMaterialTexture(ref model, 0, MaterialMapIndex.Albedo, ref texture);
    }
    public Model3D(Model model, float scale = 1.0f)
    {
        Model = model;
        Texture = null;
        Scale = scale;
    }
    #endregion

    #region IDisposable implemetation
    public void Dispose()
    {
        if (Texture.HasValue) Raylib.UnloadTexture(Texture.Value);
        Raylib.UnloadModel(Model);
    }
    #endregion

    #region Methods
    public void Draw(Vector3 location, Vector3? rotationAxis = null, float rotationAngle = 0)
    {
        if (rotationAxis == null) Raylib.DrawModel(Model, location, Scale, TintColor); 
        else Raylib.DrawModelEx(Model, location, rotationAxis.Value, rotationAngle, new Vector3(Scale, Scale, Scale), TintColor);
    }
    #endregion

    #region Properties
    public Model Model { get; }
    public Texture2D? Texture { get; } 
    public float Scale { get; set; }
    public Color TintColor { get; set; } = Color.White;
    #endregion
}