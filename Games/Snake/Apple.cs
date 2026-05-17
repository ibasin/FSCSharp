using System.Numerics;
using FSCSharp;

namespace Snake;

public class Apple : TangibleGameObject
{
    #region Constrcutors
    public Apple()
    {
        while (true)
        {
            Position = new Point(Random.Shared.Next(SnakeGame.Current.BoardSize.X), Random.Shared.Next(SnakeGame.Current.BoardSize.Y));
            if (!SnakeGame.Current.Snake.Body.Any(p => p.X == Position.X && p.Y == Position.Y)) break;
        }
        
        Location = new Vector2(Position.X*SnakeGame.BoardCellSize + SnakeGame.BoardCellSize/2f, Position.Y*SnakeGame.BoardCellSize + SnakeGame.BoardCellSize/2f);

        Body = new Sprite(SnakeGame.Current.AppleTexture);
        Body.SetScalesByTargetRectangleSize(SnakeGame.BoardCellSize, SnakeGame.BoardCellSize);
    }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        //do nothing
    }
    public override void Draw()
    {
        Body.Draw(Location);
    }
    #endregion

    #region Properties
    public Point Position { get; }
    public Vector2 Location { get; }
    public Sprite Body { get; }
    #endregion
}