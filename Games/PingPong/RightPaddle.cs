using Raylib_cs;
using System.Numerics;

namespace PingPong;

public class RightPaddle : Paddle
{
    #region Constructors
    public RightPaddle(Vector2 location, Color color) : base(location, color, KeyboardKey.Up, KeyboardKey.Down) { }
    #endregion
}