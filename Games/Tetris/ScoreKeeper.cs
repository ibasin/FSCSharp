using FSCSharp;
using Raylib_cs;

namespace Tetris;

public class ScoreKeeper : TangibleGameObject
{
    #region Overrides
    public override void Update(float delta)
    {
        //do nothing
    }
    public override void Draw(float delta)
    {
        Raylib.DrawText($"Score: {TetrisGame.Score}", 5, 5, 20, Color.White);
    }
    #endregion
}