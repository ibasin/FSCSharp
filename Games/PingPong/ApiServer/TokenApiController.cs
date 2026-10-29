using Supermodel.Presentation.WebMonk.Controllers.Api;
using System.Numerics;

namespace PingPong.ApiServer;

public class TokenInput
{
    public Vector2 BallLocation { get; set; }
    public Vector2 BallVelocity { get; set; }

    public Vector2 PaddleLocation { get; set; }
}

public class TokenOutput;

public class TokenApiController : CommandApiController<TokenInput, TokenOutput>
{
    protected override Task<TokenOutput> ExecuteAsync(TokenInput input)
    {
        PingPongGame.Current.Ball.Location = input.BallLocation;
        PingPongGame.Current.Ball.Velocity = input.BallVelocity;

        if (Program.IsPlayingLeft)
        {
            PingPongGame.Current.RightPaddle.Location = input.PaddleLocation;
            ((RightPaddleRemoteComputer)PingPongGame.Current.RightPaddle).HasToken = true;
        }
        else
        {
            PingPongGame.Current.LeftPaddle.Location = input.PaddleLocation;
            ((LeftPaddleRemoteComputer)PingPongGame.Current.LeftPaddle).HasToken = true;
        }

        return Task.FromResult(new TokenOutput());
    }
}
