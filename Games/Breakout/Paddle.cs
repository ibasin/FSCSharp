using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace Breakout;

public class Paddle : TangibleGameObject
{
    #region Constructotrs
    public Paddle(Vector2 location)
    {
        Location = location;
    }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        Vector2 velocity = Vector2.Zero;
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Left)) velocity = Go.Left.ToVector2() * PaddleVelocity;
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Right)) velocity = Go.Right.ToVector2() * PaddleVelocity;

        Location += velocity * delta;
        
        Location = Location with { X = Location.X.Clamp(Size.X / 2, BreakoutGame.Current.WindowWidth - Size.X / 2) };
    }

    public override void Draw(float delta)
    {
        Raylib.DrawRectangle((Location.X - Size.X/2).RoundToInt(), (Location.Y - Size.Y).RoundToInt(), Size.X.RoundToInt(), Size.Y.RoundToInt(), Color.RayWhite);
    }
    #endregion

    #region Properties
    public Vector2 Location { get; set; }
    
    public static Vector2 Size { get; } = new(120, 15);
    public static float PaddleVelocity => 850;
    #endregion
}
