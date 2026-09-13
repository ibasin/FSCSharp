using FSCSharp;
using Raylib_cs;

namespace Breakout
{
    public class BreakoutGame : Game<BreakoutGame>
    {
        public BreakoutGame() : base("Breakout", 540*3, 315*3, Color.Black)
        {
            //Current.PlaySound("Resources/kalinka.mp3");
            Current.ShowSplashScreen("Resources/splash.png", 7750);
        }
    }
}
