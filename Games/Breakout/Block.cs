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
        //public void OnAreaEntered(Area2D other)
        //{
        //    Main.Current.Ball.Speed = new Vector2(Main.Current.Ball.Speed.X, -Main.Current.Ball.Speed.Y);
        //    if (Input.IsActionPressed("Left")) Main.Current.Ball.Speed = new Vector2(Main.Current.Ball.Speed.X - PaddleVelocity / 12, Main.Current.Ball.Speed.Y);
        //    if (Input.IsActionPressed("Right")) Main.Current.Ball.Speed = new Vector2(Main.Current.Ball.Speed.X + PaddleVelocity / 12, Main.Current.Ball.Speed.Y);

        //    var positionDelta = Main.Current.Ball.Position.X - Main.Current.Paddle.Position.X;
        //    Main.Current.Ball.Speed = new Vector2(Main.Current.Ball.Speed.X + positionDelta / 2.5f, Main.Current.Ball.Speed.Y);

        //    Main.Current.PlayBallHitSound();
        //}

        public override void Update(float delta)
        {
            //do nothing, blocks are static
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
        #endregion
    }
}
