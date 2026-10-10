using FSCSharp;
using Raylib_cs;

namespace Snake;

public class ScoreKeeper : TangibleGameObject
{
    #region Overrides
    public override void Update(float delta)
    {
        //do nothing
    }
    public override void Draw(float delta)
    {
        Raylib.DrawText($"Score: {Score}", 5, 5, 20, Color.RayWhite);
    }
    #endregion

    #region Properties
    public int Score { get; set; }
    #endregion
}