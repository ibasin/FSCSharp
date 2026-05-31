using FSCSharp;
using Raylib_cs;

namespace SpaceInvaders;

public class ScoreKeeper : TangibleGameObject
{
    #region Overrides
    public override void Update(float delta)
    {
        //Do nothing
    }
    public override void Draw(float delta)
    {
        Raylib.DrawText($"Score: {DeadEnemiesCount}", 5, 5, 20, Color.Gray);
    }
    #endregion

    #region Properties
    public static int DeadEnemiesCount { get; set; }
    #endregion
}