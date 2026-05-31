using System.Diagnostics;
using System.Numerics;
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

        if (linesRemoved == 1) TetrisGame.Current.PlaySound("Resources/line_clear.mp3");
        if (linesRemoved == 2) TetrisGame.Current.PlaySound("Resources/2line_clear.mp3");
        if (linesRemoved == 3) TetrisGame.Current.PlaySound("Resources/3line_clear.mp3");
        if (linesRemoved == 4) TetrisGame.Current.PlaySound("Resources/4line_clear.mp3");

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

    public bool IsShapeAtLocationColliding(int shape, int orientation, Vector2 location)
    {
        int shapeBlockX = (int)location.X / TetrisGame.SquareSide;
        int shapeBlockY = (int)MathF.Ceiling(location.Y / TetrisGame.SquareSide);

        for (var x = 0; x < Data.Shapes.GetLength(2); x++)
        {
            for (var y = 0; y < Data.Shapes.GetLength(3); y++)
            {
                var shapeColor = Data.Shapes[shape, orientation, x, y];
                if (shapeColor.HasValue)
                {
                    Debug.Assert(shapeBlockX + x < TetrisGame.WidthInSquares);
                    if (shapeBlockY + y >= TetrisGame.HeightInSquares) return true;
                    
                    var blockColor = Blocks[shapeBlockX + x, shapeBlockY + y];
                    if (blockColor.HasValue) return true;
                }
            }
        }
        return false;
    }
    public void MergeShapeAtLocation(int shape, int orientation, Vector2 location)
    {
        int shapeBlockX = (int)location.X / TetrisGame.SquareSide;
        int shapeBlockY = (int)MathF.Floor(location.Y / TetrisGame.SquareSide);

        for (var x = 0; x < Data.Shapes.GetLength(2); x++)
        {
            for (var y = 0; y < Data.Shapes.GetLength(3); y++)
            {
                var shapeColor = Data.Shapes[shape, orientation, x, y];
                if (shapeColor.HasValue)
                {
                    Debug.Assert(!Blocks[shapeBlockX + x, shapeBlockY + y].HasValue);
                    Blocks[shapeBlockX + x, shapeBlockY + y] = shapeColor;
                }
            }
        }
    }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        //Do nothing
    }
    public override void Draw(float delta)
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