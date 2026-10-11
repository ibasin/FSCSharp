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
            for (var i = 0; i < 8; i++)
            {
                Current.PlaySound("Resources/ball-hit.mp3");
                Thread.Sleep((int)MathF.Round(delay));
                delay *= 0.9f;
            }
        });
        
        Current.ShowSplashScreen("Resources/splash.png", 2500);
    }
    #endregion

    #region Properties
    //public Paddle LeftPaddle { get; protected set; }
    //public Paddle RightPaddle { get; protected set; }

    public int LeftScore { get; set; }
    public int RightScore { get; set; }
    #endregion
}
