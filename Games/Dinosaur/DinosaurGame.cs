using FSCSharp;
using Raylib_cs;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Dinosaur;

public class DinosaurGame : Game<DinosaurGame>
{
    #region Constructors
    public DinosaurGame() : base("Dinosaur", 1400, 700, Color.LightGray)
    {
        // ReSharper disable VirtualMemberCallInConstructor
        PlaySound("Resources/Monkeys-Spinning-Monkeys.mp3");
        ShowSplashScreen("Resources/splash.png", 1000);
        // ReSharper restore VirtualMemberCallInConstructor

        Ground = new Ground();
        Ground.Priority = 50;
        GameObjects.Add(Ground);

        Player = new Player(new Vector2(WindowWidth / 2, WindowHeight / 2));
        GameObjects.Add(Player);
    }
    #endregion

    #region Properties
    public Player Player { get; }
    public Ground Ground { get; }
    #endregion
}

