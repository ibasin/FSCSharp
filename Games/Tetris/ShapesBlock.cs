using FSCSharp;
using Raylib_cs;

namespace Tetris;

public class ShapesBlock : TangibleGameObject
{
    #region Methods
    public int RemoveFullLines()
    {
        int linesRemoved = 0;
        for (var y = 0; y < Blocks.GetLength(1); y++)
        {
            var gapFound = false;
            for (var x = 0; x < Blocks.GetLength(0); x++)
            {
                if (!Blocks[x, y].HasValue)
                {
                    gapFound = true;
                    break;
                }
            }
            if (!gapFound)
            {
                RemoveLine(y);
                y--;
                linesRemoved++;
            }
        }
        return linesRemoved;
    }
    public void RemoveLine(int yLine)
    {
        for (var y = yLine; y >= 1; y--)
        {
            for (var x = 0; x < Blocks.GetLength(0); x++)
            {
                Blocks[x, y] = Blocks[x, y - 1];
            }
        }

        for (var x = 0; x < Blocks.GetLength(0); x++)
        {
            Blocks[x, 0] = null;
        }
    }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        //Do nothing
    }
    public override void Draw()
    {
        for (var x = 0; x < Blocks.GetLength(0); x++)
        {
            for (var y = 0; y < Blocks.GetLength(1); y++)
            {
                if (Blocks[x, y].HasValue)
                {
                    var xx = x * TetrisGame.SquareSide;
                    var yy = y * TetrisGame.SquareSide;
                    Raylib.DrawRectangle(xx, yy, TetrisGame.SquareSide, TetrisGame.SquareSide, Blocks[x, y]!.Value);
                }
            }
        }
    }
    #endregion

    #region Properties
    public readonly Color?[,] Blocks = new Color?[TetrisGame.WidthInSquares, TetrisGame.HeightInSquares];
    #endregion
}