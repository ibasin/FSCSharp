using System.Numerics;
using FSCSharp;
using Raylib_cs;
using WizardOfTerror.Characters;
using WizardOfTerror.StaticObjects;

namespace WizardOfTerror;

public class WOTGame : Game<WOTGame>
{
    #region Constructors
    public WOTGame() : base("Wizard of Terror", Field.WidthInTiles*64, Field.HeightInTiles*64, Color.Black)
    {
        Image icon = Raylib.LoadImage("Resources/icon.png");
        Raylib.SetWindowIcon(icon);
        Raylib.UnloadImage(icon);

        Hero = new Hero(new Vector2(WindowWidth / 2f, WindowHeight / 2f), Go.Right) { Priority = 300 };
        GameObjects.Add(Hero);

        //var mage = new Mage(new Vector2(WindowWidth / 2f, WindowHeight / 2f), Go.Right) { Priority = 275 };
        //GameObjects.Add(mage);

        var windowWidthCenter = WindowWidth / 2;
        var windowHeightCenter = WindowHeight / 2;

        for (var i = 0; i < 4; i++)
        {
            var direction = (Go)Random.Shared.Next(4);
            var rat = new Rat(Vector2.Zero, direction) { Priority = 250 };

            while (true)
            {
                var x = Random.Shared.NextSingle() * (WindowWidth - 100) + 50;
                var y = Random.Shared.NextSingle() * (WindowHeight - 100) + 50;
                
                if (Math.Abs(x - windowWidthCenter) < 200 && Math.Abs(y - windowHeightCenter) < 200) continue;

                rat.Location = new Vector2(x, y);

                GameObjects.Add(rat);
                break;
            }
        }

        var field = new Field { Priority = 100 };
        GameObjects.Add(field);

        //var campfire1 = new Campfire1(new Vector2(32 + 64 * 8, 16 + 64 * 5)) { Priority = 200 };
        //GameObjects.Add(campfire1);

        //var campfire2 = new Campfire2(new Vector2(32 + 64 * 10, 16 + 64 * 5)) { Priority = 200 };
        //GameObjects.Add(campfire2);

        //var flag1 = new Flag1(new Vector2(32 + 64 * 20, 16 + 64 * 10)) { Priority = 200 };
        //GameObjects.Add(flag1);

        //var flag2 = new Flag2(new Vector2(32 + 64 * 22, 16 + 64 * 10)) { Priority = 200 };
        //GameObjects.Add(flag2);

        //var flag3 = new Flag3(new Vector2(32 + 64 * 24, 16 + 64 * 10)) { Priority = 200 };
        //GameObjects.Add(flag3);

        //var flag4 = new Flag4(new Vector2(32 + 64 * 26, 16 + 64 * 10)) { Priority = 200 };
        //GameObjects.Add(flag4);

        //var flag5 = new Flag5(new Vector2(32 + 64 * 28, 16 + 64 * 10)) { Priority = 200 };
        //GameObjects.Add(flag5);

        //var tent = new Tent(new Vector2(32 + 64 * 20, 16 + 64 * 12)) { Priority = 200 };
        //GameObjects.Add(tent);

        Current.PlaySound("Resources/lightning-strike.mp3");
        Current.ShowSplashScreen("Resources/splash.png", 3000);
        
        Current.PlayMusicStream("Resources/background-music.mp3", true);
    }
    #endregion

    #region Properties
    public Hero Hero { get; }
    #endregion
}