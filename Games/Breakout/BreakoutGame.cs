using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace Breakout
{
    public class BreakoutGame : Game<BreakoutGame>
    {
        public BreakoutGame() : base("Breakout", 540*3, 315*3, Color.Black)
        {
            //Current.PlaySound("Resources/kalinka.mp3");
            Current.ShowSplashScreen("Resources/splash.png", 2000);

            //GameFinished = false;

            //var paddlePosition = new Vector2(ScreenSize.X / 2, ScreenSize.Y - Paddle.Size.Y / 2 - 50);
            //var ballPosition = new Vector2(paddlePosition.X, paddlePosition.Y - Ball.Size.Y);

            //Paddle.Start(paddlePosition, Colors.AntiqueWhite);
            //var randomSign = Random.Shared.Next(2) == 0 ? 1 : -1;
            //var ballVelocity = new Vector2(randomSign * (Random.Shared.Next(50) + 50), -300);
            //Ball.Start(ballPosition, ballVelocity);

            const int margin = 5;

            var rowCount1 = 0;
            for (float y = 100; y < 170; y += Block.Size.Y + margin)
            {
                rowCount1++;
                for (float x = margin + Block.Size.X; x < Current.WindowWidth - margin - Block.Size.X; x += Block.Size.X + margin)
                {
                    Color color;

                    if (rowCount1 <= 1) color = Color.Green;
                    else if (rowCount1 <= 2) color = Color.Yellow;
                    else if (rowCount1 <= 3) color = Color.Red;
                    else if (rowCount1 <= 4) color = Color.Yellow;
                    else color = Color.Green;

                    var block = new Block(new Vector2(x, y), color);
                    Current.GameObjects.Add(block);
                }
            }

            var rowCount2 = 0;
            for (float y = 300; y < 430; y += Block.Size.Y + margin)
            {
                rowCount2++;
                for (float x = margin + Block.Size.X; x < Current.WindowWidth - margin - Block.Size.X; x += Block.Size.X + margin)
                {
                    Color color;

                    if (rowCount2 <= 1) color = Color.Green;
                    else if (rowCount2 <= 2) color = Color.Yellow;
                    else if (rowCount2 <= 3) color = Color.Red;
                    else if (rowCount2 <= 4) color = Color.Yellow;
                    else color = Color.Green;

                    var block = new Block(new Vector2(x, y), color);
                    Current.GameObjects.Add(block);
                }
            }
        }
    }
}
