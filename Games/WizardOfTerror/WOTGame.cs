using System.Numerics;
using FSCSharp;
using Raylib_cs;
using WizardOfTerror.AnimatedObjects;
using WizardOfTerror.Characters;
using WizardOfTerror.StaticObjects;

namespace WizardOfTerror;

public class WOTGame : Game<WOTGame>
{
    public WOTGame() : base("Wizard of Terror", Field.WidthInTiles*64, Field.HeightInTiles*64, Color.Black)
    {
        var hero = new Hero(new Vector2(WindowWidth / 2f, WindowHeight / 2f), Go.Right) { Priority = 300 };
        GameObjects.Add(hero);

        var mage = new Mage(new Vector2(WindowWidth / 2f, WindowHeight / 2f), Go.Right) { Priority = 275 };
        GameObjects.Add(mage);

        var rat1 = new Rat(new Vector2(WindowWidth / 2f, WindowHeight / 2f), Go.Right) { Priority = 250 };
        GameObjects.Add(rat1);

        var rat2 = new Rat(new Vector2(WindowWidth / 2f, WindowHeight / 2f), Go.Right) { Priority = 250 };
        GameObjects.Add(rat2);

        var rat3 = new Rat(new Vector2(WindowWidth / 2f, WindowHeight / 2f), Go.Right) { Priority = 250 };
        GameObjects.Add(rat3);

        var rat4 = new Rat(new Vector2(WindowWidth / 2f, WindowHeight / 2f), Go.Right) { Priority = 250 };
        GameObjects.Add(rat4);

        var rat5 = new Rat(new Vector2(WindowWidth / 2f, WindowHeight / 2f), Go.Right) { Priority = 250 };
        GameObjects.Add(rat5);

        var rat6 = new Rat(new Vector2(WindowWidth / 2f, WindowHeight / 2f), Go.Right) { Priority = 250 };
        GameObjects.Add(rat6);

        var rat7 = new Rat(new Vector2(WindowWidth / 2f, WindowHeight / 2f), Go.Right) { Priority = 250 };
        GameObjects.Add(rat7);

        var rat8 = new Rat(new Vector2(WindowWidth / 2f, WindowHeight / 2f), Go.Right) { Priority = 250 };
        GameObjects.Add(rat8);

        var field = new Field { Priority = 100 };
        GameObjects.Add(field);

        var campfire1 = new Campfire1(new Vector2(32 + 64 * 8, 16 + 64 * 5)) { Priority = 200 };
        GameObjects.Add(campfire1);

        var campfire2 = new Campfire2(new Vector2(32 + 64 * 10, 16 + 64 * 5)) { Priority = 200 };
        GameObjects.Add(campfire2);

        var flag1 = new Flag1(new Vector2(32 + 64 * 20, 16 + 64 * 10)) { Priority = 200 };
        GameObjects.Add(flag1);

        var flag2 = new Flag2(new Vector2(32 + 64 * 22, 16 + 64 * 10)) { Priority = 200 };
        GameObjects.Add(flag2);

        var flag3 = new Flag3(new Vector2(32 + 64 * 24, 16 + 64 * 10)) { Priority = 200 };
        GameObjects.Add(flag3);

        var flag4 = new Flag4(new Vector2(32 + 64 * 26, 16 + 64 * 10)) { Priority = 200 };
        GameObjects.Add(flag4);

        var flag5 = new Flag5(new Vector2(32 + 64 * 28, 16 + 64 * 10)) { Priority = 200 };
        GameObjects.Add(flag5);

        Current.PlaySound("Resources/lightning-strike.mp3");
        Current.ShowSplashScreen("Resources/splash.png", 3000);
        
        Current.PlayMusicStream("Resources/background-music.mp3", true);
    }
}