using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace Tetris;

public class FallingShape : TangibleGameObject
{
    #region Constructors
    public FallingShape()
    {
        // ReSharper disable once PossibleLossOfFraction
        var locationInGrid = new Vector2((TetrisGame.WidthInSquares - 4) / 2, 0);
        Location = locationInGrid * TetrisGame.SquareSide;
        Shape = Random.Shared.Next(Data.NumOfShapes);
        Orientation = Random.Shared.Next(4);
    }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        Location = Location.Move(Go.Down, delta * 200);
    }

    public override void Draw()
    {
        for (var x = 0; x < Data.Shapes.GetLength(2); x++)
        {
            for (var y = 0; y < Data.Shapes.GetLength(3); y++)
            {
                var color = Data.Shapes[Shape, Orientation, x, y];
                if (color.HasValue)
                {
                    var xx = (int)Math.Round(Location.X + x * TetrisGame.SquareSide);
                    var yy = (int)Math.Round(Location.Y + y * TetrisGame.SquareSide);
                    Raylib.DrawRectangle(xx, yy, TetrisGame.SquareSide, TetrisGame.SquareSide, color.Value);

                }
            }
        }
    }
    #endregion

    #region Properties
    public Vector2 Location { get; set; }
    public int Shape { get; }
    public int Orientation { get; protected set; }
    #endregion
}