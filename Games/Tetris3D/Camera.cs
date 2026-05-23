using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace Tetris3D;

public class Camera : Camera3DGameObject
{
    #region Constructors
    public Camera(Camera3D camera) : base(camera) { }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        var a = 800f + Tetris3DGame.WidthInSquares * Tetris3DGame.SquareSide / 2f;
        var b = 800f;

        var x = Camera.Position.X;
        var y = Camera.Position.Y;
        var z = Camera.Position.Z;

        if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.Q))
        {
            x = Tetris3DGame.Current.Camera3DDefaultPosition.X;
            y = Tetris3DGame.Current.Camera3DDefaultPosition.Y;
            z = Tetris3DGame.Current.Camera3DDefaultPosition.Z;
        }
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.A))
        {
            x -= 1000*delta;
            if (x < -a) x = -a;
            
            z = -MathF.Sqrt((1f - x*x/(a*a)) * b*b);
            //Console.WriteLine($"({x},{y},{z})");
        }
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.D))
        {
            x += 1000*delta;
            if (x > a) x = a;

            z = -MathF.Sqrt((1f - x*x/(a*a)) * b*b);
            //Console.WriteLine($"({x},{y},{z})");
        }
        //if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.W)) Camera.Position.X--;
        //if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.S)) Camera.Position.X++;

        Camera.Position.X = x;
        Camera.Position.Y = y;
        Camera.Position.Z = z;

        //Camera.Target = Vector3.Zero;
    }
    #endregion
}