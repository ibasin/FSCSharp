using System.Numerics;
using FSCSharp;
using Raylib_cs;

namespace PingPong;

public class PingPongGame : Game<PingPongGame>
{
    #region Constructors
    public PingPongGame() : base("Ping Pong", 1500, 1000, Color.Black)
    {
        Task.Run(() =>
        {
            var delay = 320f;
            for (var i = 0; i < 9; i++)
            {
                Current.PlaySound("Resources/ball-hit.mp3");
                Thread.Sleep((int)MathF.Round(delay));
                delay *= 0.9f;
            }
        });
        
        Current.ShowSplashScreen("Resources/splash.png", 2500);

        GameObjects.Add(new Table());
        
        ScoreKeeper = new ScoreKeeper();
        GameObjects.Add(ScoreKeeper);

        LeftPaddle = new LeftPaddle(new Vector2(50, Current.WindowHeight / 2f), Color.Green);
        GameObjects.Add(LeftPaddle);

        RightPaddle = new RightPaddle(new Vector2(Current.WindowWidth - 50, Current.WindowHeight / 2f), Color.Red);
        GameObjects.Add(RightPaddle);

        Ball = new Ball(Vector2.Zero).InitForServing(Random.Shared.Next(2) == 0 ? LeftPaddle : RightPaddle);
        GameObjects.Add(Ball);
    }
    #endregion

    #region Properties
    public Paddle LeftPaddle { get; }
    public Paddle RightPaddle { get; }

    public Ball Ball { get; }

    public ScoreKeeper ScoreKeeper { get; }
    #endregion
}
