using FSCSharp;
using Raylib_cs;
using System.Numerics;
using PingPong.Helpers;

namespace PingPong;

public class RightPaddle : Paddle
{
    #region Constructors
    public RightPaddle(Vector2 location, Color color) : base(location, color) { }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        if (Cooldown > 0) Cooldown -= delta;

        if (Raylib.IsKeyDown(KeyboardKey.Up)) Location = Location with { Y = Location.Y - Velocity * delta };
        if (Raylib.IsKeyDown(KeyboardKey.Down)) Location = Location with { Y = Location.Y + Velocity * delta };

        var ball = PingPongGame.Current.Ball;
        if (Cooldown <= 0 && ball.Body.IsCollidingByPixelAtLocation(ball.Location, Location, Size))
        {
            MovingCircleStaticRectCollisionResolver.GetCollisionDetails(ball.Location, ball.Body.Size.X / 2, Location, Size, ball.Velocity, delta, out var normal, out _);
            ball.Velocity = Vector2.Reflect(ball.Velocity, Vector2.Normalize(normal));

            if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Up)) ball.Velocity = ball.Velocity with { X = ball.Velocity.X - Velocity / 10 };
            if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Down)) ball.Velocity = ball.Velocity with { X = ball.Velocity.X + Velocity / 10 };

            var locationDelta = ball.Location.X - Location.X;
            ball.Velocity = ball.Velocity with { X = ball.Velocity.X + 2.5f * locationDelta };

            if (ball.Velocity.X is >= 0 and < 250) ball.Velocity = ball.Velocity with { X = 250 };
            if (ball.Velocity.X is < 0 and > -250) ball.Velocity = ball.Velocity with { X = -250 };

            PingPongGame.Current.PlaySound("Resources/ball-hit.mp3");
            Cooldown = 0.1f;
        }

        base.Update(delta);
    }
    #endregion
}