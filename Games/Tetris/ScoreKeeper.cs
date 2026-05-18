using FSCSharp;
using Raylib_cs;

namespace Tetris;

public class ScoreKeeper : GameObject
{
    #region Overrides
    public override void Update(float delta)
    {
        Raylib.DrawText($"Score: {TetrisGame.Score}", 5, 5, 20, Color.DarkGray);
    }
    #endregion
}