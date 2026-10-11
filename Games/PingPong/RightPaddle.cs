using System.Numerics;
using Raylib_cs;

namespace PingPong;

public class RightPaddle : Paddle
{
    #region Constructors
    public RightPaddle(Vector2 location, Color color) : base(location, color) { }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        if (Raylib.IsKeyDown(KeyboardKey.Up)) Location = Location with { Y = Location.Y - PaddleVelocity * delta };
        if (Raylib.IsKeyDown(KeyboardKey.Down)) Location = Location with { Y = Location.Y + PaddleVelocity * delta };
        
        base.Update(delta);
    }
    #endregion
}