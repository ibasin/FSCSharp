using FSCSharp;
using Raylib_cs;

namespace PingPong;

public class PingPongGame : Game<PingPongGame>
{
    public PingPongGame() : base("Ping Pong", 1500, 500, Color.Black)
    {
        #region Properties
        //public Paddle LeftPaddle { get; protected set; }
        //public Paddle RightPaddle { get; protected set; }

        public int LeftScore { get; set; }
        public int RightScore { get; set; }
        #endregion
    }
}
