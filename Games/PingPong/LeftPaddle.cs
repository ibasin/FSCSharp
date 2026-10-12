using FSCSharp;
using PingPong.Helpers;
using Raylib_cs;
using System.Numerics;

namespace PingPong;

public class LeftPaddle : Paddle
{
    #region Constructors
    public LeftPaddle(Vector2 location, Color color) : base(location, color) { }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        if (Cooldown > 0) Cooldown -= delta;

        if (Raylib.IsKeyDown(KeyboardKey.A)) Location = Location with { Y = Location.Y - Velocity * delta };
        if (Raylib.IsKeyDown(KeyboardKey.Z)) Location = Location with { Y = Location.Y + Velocity * delta };

        var ball = PingPongGame.Current.Ball;
        if (Cooldown <= 0 && ball.Body.IsCollidingByPixelAtLocation(ball.Location, Location, Size))
        {
            MovingCircleStaticRectCollisionResolver.GetCollisionDetails(ball.Location, ball.Body.Size.X / 2, Location, Size, ball.Velocity, delta, out var normal, out _);
            ball.Velocity = Vector2.Reflect(ball.Velocity, Vector2.Normalize(normal));

            if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Left)) ball.Velocity = ball.Velocity with { X = ball.Velocity.X - Velocity / 12 };
            if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Right)) ball.Velocity = ball.Velocity with { X = ball.Velocity.X + Velocity / 12 };

            var locationDelta = ball.Location.X - Location.X;
            ball.Velocity = ball.Velocity with { X = ball.Velocity.X + 1.5f * locationDelta };

            PingPongGame.Current.PlaySound("Resources/ball-hit.mp3");
            Cooldown = 0.1f;
        }

        base.Update(delta);
    }
    #endregion
}