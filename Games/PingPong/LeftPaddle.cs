using System.Numerics;
using Raylib_cs;

namespace PingPong;

public class LeftPaddle : Paddle
{
    #region Constructors
    public LeftPaddle(Vector2 location, Color color) : base(location, color) { }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        if (Raylib.IsKeyDown(KeyboardKey.A)) Location = Location with { Y = Location.Y - PaddleVelocity * delta };
        if (Raylib.IsKeyDown(KeyboardKey.Z)) Location = Location with { Y = Location.Y + PaddleVelocity * delta };

        base.Update(delta);
    }
    #endregion
}