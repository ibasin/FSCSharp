using FSCSharp;
using Raylib_cs;

namespace Tetris;

public class Grid : TangibleGameObject
{
    #region Overrides
    public override void Update(float delta)
    {
        //do nothing
    }
    public override void Draw()
    {
        //horizontal lines
        for (var i = 1; i < TetrisGame.HeightInSquares; i++)
        {
            var y = i * TetrisGame.SquareSide;
            Raylib.DrawLine(0, y, TetrisGame.WidthInSquares * TetrisGame.SquareSide, y, Color.DarkGray);
        }

        //vertical lines
        for (var i = 1; i < TetrisGame.WidthInSquares; i++)
        {
            var  x = i * TetrisGame.SquareSide;
            Raylib.DrawLine(x, 0, x, TetrisGame.HeightInSquares * TetrisGame.SquareSide, Color.DarkGray);
        }
    }
    #endregion
}