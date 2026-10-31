using Raylib_cs;
using System.Numerics;
using PingPong.Api;
using PingPong.Api.ApiClient;

namespace PingPong;

public class LeftPaddleRemoteComputer : LeftPaddle, IComputerPaddle
{
    #region Constrcutors
    public LeftPaddleRemoteComputer(Vector2 location, Color color) : base(location, color) { }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        //Do nothing, this paddle is updated by the web server
    }
    public override void PostUpdate(float delta)
    {
        var webApiDataContext = new PingPongWebApiDataContext();
        _ = webApiDataContext.TokenAsync(new TokenInput
        {
            BallLocation = PingPongGame.Current.Ball.Location.ToVector2AsClass(),
            BallVelocity = PingPongGame.Current.Ball.Velocity.ToVector2AsClass(),,
            PaddleLocation = PingPongGame.Current.RightPaddle.Location.ToVector2AsClass(),
        });
    }
    #endregion

    #region Properties
    public bool HasToken { get; set; }
    #endregion
}

public class RightPaddleRemoteComputer : LeftPaddle, IComputerPaddle
{
    #region Constrcutors
    public RightPaddleRemoteComputer(Vector2 location, Color color) : base(location, color) { }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        //Do nothing, this paddle is updated by the web server
    }
    public override void PostUpdate(float delta)
    {
        var webApiDataContext = new PingPongWebApiDataContext();
        _ = webApiDataContext.TokenAsync(new TokenInput
        {
            BallLocation = PingPongGame.Current.Ball.Location,
            BallVelocity = PingPongGame.Current.Ball.Velocity,
            PaddleLocation = PingPongGame.Current.LeftPaddle.Location
        });
    }
    #endregion

    #region Properties
    public bool HasToken { get; set; }
    #endregion
}


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
    // ReSharper disable InconsistentNaming
    public float YTarget;
    public float DecisionCooldown;
    // ReSharper restore InconsistentNaming
    #endregion
}