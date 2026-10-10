using FSCSharp;
using Raylib_cs;
using System.Numerics;
using Breakout.Helpers;

namespace Breakout;

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
            //reflect based on normal vector of collision
            MovingCircleStaticRectCollisionResolver.GetCollisionDetails(ball.Location, ball.Body.Size.X/2, Location, Size, ball.Velocity, delta, out var normal, out _);
            ball.Velocity = Vector2.Reflect(ball.Velocity, Vector2.Normalize(normal));

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
                if (BreakoutGame.Current.Blocks.Count == 0) throw new GameOverException($"You win! Score {BreakoutGame.Current.ScoreKeeper.Score++}");
            }
            // ReSharper restore UsageOfDefaultStructEquality
            BreakoutGame.Current.PlaySound("Resources/ball-hit.mp3");
            Cooldown = 0.08f;

            BreakoutGame.Current.ScoreKeeper.Score++;
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