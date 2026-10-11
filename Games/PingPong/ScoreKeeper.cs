using FSCSharp;
using Raylib_cs;

namespace PingPong;

public class ScoreKeeper : TangibleGameObject
{
    #region Overrides
    public override void Update(float delta)
    {
        //do nothing
    }
    public override void Draw(float delta)
    {
        GLib.DrawTextRightAligned($"{LeftScore}", PingPongGame.Current.WindowWidth / 2 - 20, 8, 40, Color.RayWhite);
        Raylib.DrawText($": {RightScore}", PingPongGame.Current.WindowWidth / 2 - 2, 8, 40, Color.RayWhite);
    }
    #endregion

    #region Properties
    public int LeftScore { get; set; }
    public int RightScore { get; set; }
    #endregion
}