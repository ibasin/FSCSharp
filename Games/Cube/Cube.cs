using Raylib_cs;
using System.Numerics;
using FSCSharp;

namespace Cube;

public class Cube : Tangible3DGameObject
{
    #region Cnstrcutors
    public Cube(Vector3 location)
    {
        Location = location;
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

    }
    public override void Draw()
    {
        Raylib.DrawCube(Location, 30, 30, 30, Color.DarkBrown);
        Raylib.DrawCubeWires(Location, 30, 30, 30, Color.White);
    }
    #endregion

    #region Properties
    public Vector3 Location { get; set; }
    #endregion

}