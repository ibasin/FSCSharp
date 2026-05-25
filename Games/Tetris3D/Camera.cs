using FSCSharp;
using Raylib_cs;

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
        var b = 800f + Tetris3DGame.HeightInSquares * Tetris3DGame.SquareSide / 2f;
        var c = 800f;

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
            if (x < -a+10) x = -a+10;
            
            z = -MathF.Sqrt((1f - x*x/(a*a)) * c*c);
            //Console.WriteLine($"({x},{y},{z})");
        }
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.D))
        {
            x += 1000*delta;
            if (x > a-10) x = a-10;

            z = -MathF.Sqrt((1f - x*x/(a*a)) * c*c);
            //Console.WriteLine($"({x},{y},{z})");
        }
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.W))
        {
            y += 1000 * delta;
            if (y > b-10) y = b-10;

            z = -MathF.Sqrt((1f - y*y/(b*b))*c*c);
            //Console.WriteLine($"({x},{y},{z})");
        }
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.S))
        {
            y -= 1000 * delta;
            if (y < -b+10) y = -b+10;

            z = -MathF.Sqrt((1f - y*y/(b*b))*c*c);
            //Console.WriteLine($"({x},{y},{z})");
        }

        Camera.Position.X = x;
        Camera.Position.Y = y;
        Camera.Position.Z = z;
    }
    #endregion
}