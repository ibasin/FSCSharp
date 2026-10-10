using Breakout.Helpers;
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
        if (Cooldown > 0) Cooldown -= delta;

        Vector2 velocity = Vector2.Zero;
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Left)) velocity = Go.Left.ToVector2() * Velocity;
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Right)) velocity = Go.Right.ToVector2() * Velocity;

        Location += velocity * delta;

        var ball = BreakoutGame.Current.Ball;
        if (Cooldown <= 0 && ball.Body.IsCollidingByPixelAtLocation(ball.Location, Location, Size))
        {
            MovingCircleStaticRectCollisionResolver.GetCollisionDetails(ball.Location, ball.Body.Size.X / 2, Location, Size, ball.Velocity, delta, out var normal, out _);
            ball.Velocity = Vector2.Reflect(ball.Velocity, Vector2.Normalize(normal));

            if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Left)) ball.Velocity = ball.Velocity with { X = ball.Velocity.X - Velocity.X / 12 };
            if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Right)) ball.Velocity = ball.Velocity with { X = ball.Velocity.X + Velocity.X / 12 };

            var locationDelta = ball.Location.X - Location.X;
            ball.Velocity = ball.Velocity with { X = ball.Velocity.X + 1.5f*locationDelta };

            BreakoutGame.Current.PlaySound("Resources/ball-hit.mp3");
            Cooldown = 0.1f;
        }

        Location = Location with { X = Location.X.Clamp(Size.X / 2, BreakoutGame.Current.WindowWidth - Size.X / 2) };
    }

    public override void Draw(float delta)
    {
        Raylib.DrawRectangle((Location.X - Size.X / 2).RoundToInt(), (Location.Y - Size.Y / 2).RoundToInt(), Size.X.RoundToInt(), Size.Y.RoundToInt(), Color.RayWhite);
    }
    #endregion

    #region Properties
    public Vector2 Location { get; set; }

    public Vector2 Size { get; } = new(150, 15);
    public Vector2 Velocity { get; } = new(850, 0);

    protected float Cooldown { get; set; }
    #endregion
}