using System.Numerics;
using FSCSharp;
using Raylib_cs;

namespace WizardOfTerror;

public class WOTGame : Game<WOTGame>
{
    public WOTGame() : base("Wizard of Terror", 1200, 800, Color.Black)
    {
        var hero = new Hero(new Vector2(WindowWidth / 2f, WindowHeight / 2f), Go.Right);

        GameObjects.Add(hero);
    }
}