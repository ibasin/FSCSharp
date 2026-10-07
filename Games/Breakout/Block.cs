using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace Breakout
{
    public class Block : TangibleGameObject
    {
        #region Constructors
        public Block(Vector2 location, Color color)
        {
            Location = location;
            Color = color;
        }
        #endregion

        #region Overrides
        public override void Update(float delta)
        {
            if (Cooldown > 0) Cooldown -= delta;

            var ball = BreakoutGame.Current.Ball;
            if (Cooldown <= 0 && Sprite.AreCircleAndRectangleColliding(ball.Location, ball.Body.Size.X/2, Location, Size))
            {
                if (Sprite.IfCollidingAreCircleAndRectangleCollidingVertically(ball.Location, ball.Body.Size.X / 2, Location, Size)) ball.Velocity = ball.Velocity with { X = -ball.Velocity.X };
                else if (Sprite.IfCollidingAreCircleAndRectangleCollidingHorizontally(ball.Location, ball.Body.Size.X / 2, Location, Size)) ball.Velocity = ball.Velocity with { Y = -ball.Velocity.Y };
                else throw new Exception();

                // ReSharper disable UsageOfDefaultStructEquality
                if (Color.Equals(Color.Red))
                {
                    Color = Color.Yellow;
                }
                else if (Color.Equals(Color.Yellow))
                {
                    Color = Color.Green;
                }
                else
                {
                    ToDelete = true;
                    BreakoutGame.Current.Blocks.Remove(this);
                    if (BreakoutGame.Current.Blocks.Count == 0) throw new GameOverException("You win!");
                }
                // ReSharper restore UsageOfDefaultStructEquality
                BreakoutGame.Current.PlaySound("Resources/ball-hit.mp3");
                Cooldown = 0.2f;
            }
        }
        public override void Draw(float delta)
        {
            Raylib.DrawRectangle((int)(Location.X - Size.X/2), (int)(Location.Y - Size.Y/2), (int)Size.X, (int)Size.Y, Color);
        }
        #endregion

        #region Properties
        public static readonly Vector2 Size = new Vector2(95, 25);
        public Vector2 Location { get; set; }
        public Color Color { get; protected set; }
        
        protected float Cooldown { get; set; }
        #endregion
    }
}
