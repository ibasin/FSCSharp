using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace Car;

public class Car : Tangible3DGameObject
{
    #region Constructors
    public Car(Vector3 location)
    {
        Location = location;

        //model downloaded here
        var model = Raylib.LoadModel("Resources/pony_cartoon_small.glb");
        Body = new Model3D(model, 50f);
    }
    public override void Dispose()
    {
        base.Dispose();
        Body.Dispose();
    }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Left)) Location += Vector3.UnitX;
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Right)) Location -= Vector3.UnitX;
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Up)) Location += Vector3.UnitY;
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Down)) Location -= Vector3.UnitY;
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Equal)) Location -= Vector3.UnitZ;
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Minus)) Location += Vector3.UnitZ;
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.A)) Rotaton -= 1f;
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.D)) Rotaton += 1f;
    }

    public override void Draw()
    {
        Body.Draw(Location, Vector3.UnitY, Rotaton);
    }
    #endregion

    #region Properties
    public Model3D Body { get; }
    public Vector3 Location { get; set; }
    public float Rotaton { get; set; }
    #endregion
}