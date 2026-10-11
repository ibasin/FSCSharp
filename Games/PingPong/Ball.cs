using System.Numerics;
using FSCSharp;
using Raylib_cs;

namespace PingPong;

public class Ball : TangibleGameObject
{
    #region Embedded Types
    public enum Serving
    {
        None, Left, Right
    }
    #endregion
    
    #region Constrcutors
    public Ball(Vector2 location)
    {
        Location = location;
        Body = new Sprite(Raylib.LoadTexture("Resources/ball.png"), 0.055f);
    }
    #endregion

    #region Methods
    public Ball InitForServing(Paddle paddle)
    {
        var isLeft = paddle is LeftPaddle;
        ServingState = isLeft ? Serving.Left : Serving.Right;

        var randomSign = Random.Shared.Next(2) == 0 ? 1 : -1;
        if (isLeft)
        {
            Velocity = new Vector2(300, randomSign * (Random.Shared.Next(120) + 100));
            if (Game.KeyboardManager.IsKeyDown(KeyboardKey.A)) Velocity = Velocity with { X = Velocity.Y - Paddle.Velocity / 12 };
            if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Z)) Velocity = Velocity with { X = Velocity.X + Paddle.Velocity / 12 };
            Location = paddle.Location with { X = paddle.Location.X + Body.Size.X / 2 - Paddle.Size.X / 2 };
        }
        else
        {
            Velocity = new Vector2(-300, randomSign * (Random.Shared.Next(120) + 100));
            if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Up)) Velocity = Velocity with { X = Velocity.Y - Paddle.Velocity / 12 };
            if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Down)) Velocity = Velocity with { X = Velocity.X + Paddle.Velocity / 12 };
            Location = paddle.Location with { X = paddle.Location.X - Body.Size.X / 2 - Paddle.Size.X / 2 };
        }

        return this;
    }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        if (ServingState != Serving.None)
        {
            if (ServingState == Serving.Left) InitForServing(PingPongGame.Current.LeftPaddle);
            else InitForServing(PingPongGame.Current.RightPaddle);

            if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.Space))
            {
                ServingState = Serving.None;
                PingPongGame.Current.PlaySound("Resources/ball-hit.mp3");
            }
        }
        else
        {
            Velocity = Vector2.Normalize(Velocity) * 450;

            Location += Velocity * delta;

            if (Location.X <= Body.Size.X / 2)
            {
                Velocity = Velocity with { X = MathF.Abs(Velocity.X) };
                PingPongGame.Current.PlaySound("Resources/ball-hit.mp3");
            }

            if (Location.X >= PingPongGame.Current.WindowWidth - Body.Size.X / 2)
            {
                Velocity = Velocity with { X = -MathF.Abs(Velocity.X) };
                PingPongGame.Current.PlaySound("Resources/ball-hit.mp3");
            }

            if (Location.Y <= Body.Size.Y / 2)
            {
                Velocity = Velocity with { Y = MathF.Abs(Velocity.Y) };
                PingPongGame.Current.PlaySound("Resources/ball-hit.mp3");
            }

            if (Location.Y >= PingPongGame.Current.WindowHeight - Body.Size.Y / 2)
            {
                Velocity = Velocity with { Y = -MathF.Abs(Velocity.Y) };
                PingPongGame.Current.PlaySound("Resources/ball-hit.mp3");
            }
        }
    }
    public override void Draw(float delta)
    {
        Body.Draw(Location);
    }
    #endregion

    #region Properties
    public Serving ServingState { get; set; }

    public Vector2 Location { get; set; }
    public Sprite Body { get; }

    public Vector2 Velocity { get; set; } = Vector2.Zero;
    #endregion
}