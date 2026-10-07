using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace Breakout
{
    public class Ball : TangibleGameObject
    {
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
            IsServing = true;
            var randomSign = Random.Shared.Next(2) == 0 ? 1 : -1;
            Velocity= new Vector2(randomSign * Random.Shared.Next(120), -300);

            if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Left)) Velocity = Velocity with { X = Velocity.X - BreakoutGame.Current.Paddle.Velocity / 12 };
            if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Right)) Velocity = Velocity with { X = Velocity.X + BreakoutGame.Current.Paddle.Velocity / 12 };

            Location = paddle.Location with { Y = paddle.Location.Y - Body.Size.Y / 2 - paddle.Size.Y / 2 };

            return this;
        }
        #endregion

        #region Overrides
        public override void Update(float delta)
        {
            if (IsServing)
            {
                InitForServing(BreakoutGame.Current.Paddle);
                if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.Space))
                {
                    IsServing = false;
                    BreakoutGame.Current.PlaySound("Resources/ball-hit.mp3");
                }
            }
            else
            {
                Location += Velocity * delta;

                if (Location.X <= Body.Size.X / 2 || Location.X >= BreakoutGame.Current.WindowWidth - Body.Size.X / 2)
                {
                    Velocity = Velocity with { X = -Velocity.X };
                    BreakoutGame.Current.PlaySound("Resources/ball-hit.mp3");
                }

                if (Location.Y <= Body.Size.Y / 2)
                {
                    Velocity = Velocity with { Y = -Velocity.Y };
                    BreakoutGame.Current.PlaySound("Resources/ball-hit.mp3");
                }

                if (Location.Y >= BreakoutGame.Current.WindowHeight - Body.Size.Y / 2) throw new GameOverException("Game is lost!");
            }
        }
        public override void Draw(float delta)
        {
            Body.Draw(Location);
        }
        #endregion

        #region Properties
        public bool IsServing { get; set; }

        public Vector2 Location { get; set; }
        public Sprite Body { get; }

        public Vector2 Velocity { get; set; } = Vector2.Zero;
        #endregion
    }
}
