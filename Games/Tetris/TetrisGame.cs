using FSCSharp;
using Raylib_cs;

namespace Tetris;

public class TetrisGame : Game<TetrisGame>
{
    #region Constrcutors
    public TetrisGame() : base("Tetris", WidthInSquares * SquareSide, HeightInSquares * SquareSide, Color.Black)
    {
        GameObjects.Add(new ScoreKeeper());
        GameObjects.Add(new FallingShape());
    }
    #endregion

    #region Properties
    public static int Score { get; set; }
    #endregion

    #region Settings
    public const int WidthInSquares = 20;
    public const int HeightInSquares = 40;

    public const int SquareSide = 30;
    #endregion
}