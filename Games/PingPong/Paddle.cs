using FSCSharp;
using PingPong.Helpers;
using Raylib_cs;
using System.Numerics;

namespace PingPong;

public abstract class Paddle : TangibleGameObject
{
    #region Constructors
    protected Paddle(Vector2 location, Color color, KeyboardKey upKey, KeyboardKey downKey)
    {
        Location = location;
        Color = color;

        UpKey = upKey;
        DownKey = downKey;
    }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        var isUp = Game.KeyboardManager.IsKeyDown(UpKey);
        var isDown = Game.KeyboardManager.IsKeyDown(DownKey);
        UpdateInternal(delta, isUp, isDown);
    }
    public override void Draw(float delta)
    {
        Raylib.DrawRectangle((Location.X - Size.X / 2).RoundToInt(), (Location.Y - Size.Y / 2).RoundToInt(), Size.X.RoundToInt(), Size.Y.RoundToInt(), Color);
    }
    #endregion

    #region Methods
    protected void UpdateInternal(float delta, bool isUp, bool isDown)
    {
        if (Cooldown > 0) Cooldown -= delta;

        if (isUp) Location = Location with { Y = Location.Y - Velocity * delta };
        if (isDown) Location = Location with { Y = Location.Y + Velocity * delta };

        var ball = PingPongGame.Current.Ball;
        if (Cooldown <= 0 && ball.Body.IsCollidingByPixelAtLocation(ball.Location, Location, Size))
        {
            MovingCircleStaticRectCollisionResolver.GetCollisionDetails(ball.Location, ball.Body.Size.X / 2, Location, Size, ball.Velocity, delta, out var normal, out _);
            ball.Velocity = Vector2.Reflect(ball.Velocity, Vector2.Normalize(normal));

            if (isUp) ball.Velocity = ball.Velocity with { X = ball.Velocity.X - Velocity / 5 };
            if (isDown) ball.Velocity = ball.Velocity with { X = ball.Velocity.X + Velocity / 5 };

            var locationDelta = ball.Location.X - Location.X;
            ball.Velocity = ball.Velocity with { X = ball.Velocity.X + 2.5f * locationDelta };

            if (ball.Velocity.X is >= 0 and < 250) ball.Velocity = ball.Velocity with { X = 250 };
            if (ball.Velocity.X is < 0 and > -250) ball.Velocity = ball.Velocity with { X = -250 };

            PingPongGame.Current.PlaySound("Resources/ball-hit.mp3");
            Cooldown = 0.1f;
        }

        Location = Location with { Y = Math.Clamp(Location.Y, Size.Y / 2, PingPongGame.Current.WindowHeight - Size.Y / 2) };

    }
    #endregion

    #region Properties
    public KeyboardKey UpKey { get; }
    public KeyboardKey DownKey { get; }

    public Vector2 Location { get; set; }
    public Color Color { get; }

    protected float Cooldown { get; set; }

    public static Vector2 Size { get; } = new(15, 120);
    public static float Velocity => 850;
    #endregion
}
