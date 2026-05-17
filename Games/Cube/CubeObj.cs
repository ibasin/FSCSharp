using Raylib_cs;
using System.Numerics;
using FSCSharp;

namespace Cube;

public class CubeObj : TangibleGameObject
{
    #region Cnstrcutors
    public CubeObj(Vector3 location)
    {
        Location = location;
    }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        var key = Game.KeyboardManager.ReadKey();
        if (key == KeyboardKey.Left) Location -= Vector3.UnitX;
        if (key == KeyboardKey.Right) Location += Vector3.UnitX;
        if (key == KeyboardKey.Up) Location += Vector3.UnitZ;
        if (key == KeyboardKey.Down) Location -= Vector3.UnitZ;
        if (key == KeyboardKey.Equal) Location -= Vector3.UnitY;
        if (key == KeyboardKey.Minus) Location += Vector3.UnitY;

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