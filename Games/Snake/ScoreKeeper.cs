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
    public override void Draw()
    {
        Raylib.DrawText($"Score: {ApplesCount}", 5, 5, 20, Color.RayWhite);
    }
    #endregion

    #region Properties
    public static int ApplesCount { get; set; }
    #endregion
}