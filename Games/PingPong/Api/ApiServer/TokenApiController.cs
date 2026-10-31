using Supermodel.Presentation.WebMonk.Controllers.Api;
using System.Numerics;

namespace PingPong.Api.ApiServer;

public class TokenInput
{
    public Vector2AsClass BallLocation { get; set; } = null!;
    public Vector2AsClass BallVelocity { get; set; } = null!;

    public Vector2AsClass PaddleLocation { get; set; } = null!;
}

public class TokenOutput;

public class TokenApiController : CommandApiController<TokenInput, TokenOutput>
{
    protected override Task<TokenOutput> ExecuteAsync(TokenInput input)
    {
        PingPongGame.Current.Ball.Location = input.BallLocation.ToVector2();
        PingPongGame.Current.Ball.Velocity = input.BallVelocity.ToVector2();

        if (Program.IsPlayingLeft)
        {
            PingPongGame.Current.RightPaddle.Location = input.PaddleLocation.ToVector2();
            ((RightPaddleRemoteComputer)PingPongGame.Current.RightPaddle).HasToken = true;
        }
        else
        {
            PingPongGame.Current.LeftPaddle.Location = input.PaddleLocation.ToVector2();
            ((LeftPaddleRemoteComputer)PingPongGame.Current.LeftPaddle).HasToken = true;
        }

        return Task.FromResult(new TokenOutput());
    }
}
