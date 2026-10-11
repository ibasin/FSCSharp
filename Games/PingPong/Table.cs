using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace PingPong;

public class Table : TangibleGameObject
{
    #region Overrides
    public override void Update(float delta)
    {
        //do nothing, this is a static object
    }
    public override void Draw(float delta)
    {
        var lineStart = new Vector2(PingPongGame.Current.WindowWidth / 2f, 50);
        var lineEnd = new Vector2(PingPongGame.Current.WindowWidth / 2f, PingPongGame.Current.WindowHeight - 20);

        GLib.DrawDashedThickLine(lineStart, lineEnd, 30, 30, 6, Color.White);
    }
    #endregion
}
