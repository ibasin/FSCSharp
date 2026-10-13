using Raylib_cs;
using System.Numerics;

namespace PingPong;

public class LeftPaddleComputer : LeftPaddle, IComputerPaddle
{
    #region Constrcutors
    public LeftPaddleComputer(Vector2 location, Color color) : base(location, color) { }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        IComputerPaddle.PredictBallYTargetImperfectly(delta, true, ref YTarget, ref DecisionCooldown);
        var isUp = Location.Y - YTarget > 10;
        var isDown = Location.Y - YTarget < -10;
        UpdateInternal(delta, isUp, isDown);
    }
    #endregion

    #region Properties
    public float YTarget;
    public float DecisionCooldown;
    #endregion
}