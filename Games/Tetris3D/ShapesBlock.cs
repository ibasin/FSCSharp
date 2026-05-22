using System.Diagnostics;
using System.Numerics;
using FSCSharp;
using Raylib_cs;

namespace Tetris3D;

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

        if (linesRemoved == 1) Tetris3DGame.Current.PlaySound("Resources/line_clear.mp3");
        if (linesRemoved == 2) Tetris3DGame.Current.PlaySound("Resources/2line_clear.mp3");
        if (linesRemoved == 3) Tetris3DGame.Current.PlaySound("Resources/3line_clear.mp3");
        if (linesRemoved == 4) Tetris3DGame.Current.PlaySound("Resources/4line_clear.mp3");

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
        int shapeBlockX = (int)location.X / Tetris3DGame.SquareSide;
        int shapeBlockY = (int)MathF.Ceiling(location.Y / Tetris3DGame.SquareSide);

        for (var x = 0; x < Data.Shapes.GetLength(2); x++)
        {
            for (var y = 0; y < Data.Shapes.GetLength(3); y++)
            {
                var shapeColor = Data.Shapes[shape, orientation, x, y];
                if (shapeColor.HasValue)
                {
                    Debug.Assert(shapeBlockX + x < Tetris3DGame.WidthInSquares);
                    if (shapeBlockY + y >= Tetris3DGame.HeightInSquares) return true;
                    
                    var blockColor = Blocks[shapeBlockX + x, shapeBlockY + y];
                    if (blockColor.HasValue) return true;
                }
            }
        }
        return false;
    }
    public void MergeShapeAtLocation(int shape, int orientation, Vector2 location)
    {
        int shapeBlockX = (int)location.X / Tetris3DGame.SquareSide;
        int shapeBlockY = (int)MathF.Floor(location.Y / Tetris3DGame.SquareSide);

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
    public override void Draw()
    {
        for (var x = 0; x < Blocks.GetLength(0); x++)
        {
            for (var y = 0; y < Blocks.GetLength(1); y++)
            {
                if (Blocks[x, y].HasValue)
                {
                    var xx = x * Tetris3DGame.SquareSide;
                    var yy = y * Tetris3DGame.SquareSide;
                    
                    
                    //Raylib.DrawRectangle(xx, yy, Tetris3DGame.SquareSide, Tetris3DGame.SquareSide, Blocks[x, y]!.Value);
                }
            }
        }
    }
    #endregion

    #region Properties
    public readonly Color?[,] Blocks = new Color?[Tetris3DGame.WidthInSquares, Tetris3DGame.HeightInSquares];
    #endregion
}