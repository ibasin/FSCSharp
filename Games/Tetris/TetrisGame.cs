using FSCSharp;
using Raylib_cs;

namespace Tetris;

public class TetrisGame : Game<TetrisGame>
{
    #region Constrcutors
    public TetrisGame() : base("Tetris", WidthInSquares*SquareSide, HeightInSquares*SquareSide, Color.Black) {}
    #endregion

    #region Properties
    #endregion

    #region Settings
    public const int WidthInSquares = 20;
    public const int HeightInSquares = 80;

    public const int SquareSide = 10;
    #endregion
}