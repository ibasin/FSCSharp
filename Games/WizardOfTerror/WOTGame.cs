using System.Numerics;
using FSCSharp;
using Raylib_cs;

namespace WizardOfTerror;

public class WOTGame : Game<WOTGame>
{
    public WOTGame() : base("Wizard of Terror", 1280, 960, Color.Black)
    {
        var hero = new Hero(new Vector2(WindowWidth / 2f, WindowHeight / 2f), Go.Right);
        var field = new Field();

        GameObjects.Add(hero);
        GameObjects.Add(field);
    }
}