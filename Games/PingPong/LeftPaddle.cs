using Raylib_cs;
using System.Numerics;

namespace PingPong;

public class LeftPaddle : Paddle
{
    #region Constructors
    public LeftPaddle(Vector2 location, Color color) : base(location, color, KeyboardKey.A, KeyboardKey.Z) { }
    #endregion
}