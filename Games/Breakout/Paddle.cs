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
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Left))
        {
            if (Velocity > 0) Velocity = 0;
            Velocity += -Acceleration * delta;
        }
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Right))
        {
            if (Velocity < 0) Velocity = 0;
            Velocity += Acceleration * delta;
        }

        Location = Location with { X = Location.X + Velocity*delta };

        var ball = BreakoutGame.Current.Ball;
        if (ball.Body.IsCollidingByPixelAtLocation(ball.Location, Location, Size))
        {
            ball.Velocity = ball.Velocity with { Y = -ball.Velocity.Y };

            ball.Velocity = ball.Velocity with { X = ball.Velocity.X + Velocity/12 };

            //var locationDelta = ball.Location.X - Location.X;
            //ball.Velocity = ball.Velocity with { X = ball.Velocity.X + locationDelta * 2.5f };

            BreakoutGame.Current.PlaySound("Resources/ball-hit.mp3");
        }

        var clampedLocation = Location with { X = Location.X.Clamp(Size.X / 2, BreakoutGame.Current.WindowWidth - Size.X / 2) };
        if (clampedLocation != Location)
        {
            Location = clampedLocation;
            Velocity = 0;
        }
    }

    public override void Draw(float delta)
    {
        Raylib.DrawRectangle((Location.X - Size.X/2).RoundToInt(), (Location.Y - Size.Y/2).RoundToInt(), Size.X.RoundToInt(), Size.Y.RoundToInt(), Color.RayWhite);
    }
    #endregion

    #region Properties
    public Vector2 Location { get; set; }
    
    public Vector2 Size { get; } = new(200, 10);
    public float Velocity { get; set; }
    public float Acceleration => 500;

    #endregion
}
