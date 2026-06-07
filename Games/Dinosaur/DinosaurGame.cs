using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace Dinosaur;

public class DinosaurGame : Game<DinosaurGame>
{
    #region Constructors
    public DinosaurGame() : base("Dinosaur", 1400, 700, Color.LightGray)
    {
        // ReSharper disable VirtualMemberCallInConstructor
        PlayMusicStream("Resources/Monkeys-Spinning-Monkeys.mp3", true);
        ShowSplashScreen("Resources/splash.png", 500);
        // ReSharper restore VirtualMemberCallInConstructor

        Ground = new Ground();
        Ground.Priority = 50;
        GameObjects.Add(Ground);

        Player = new Player();
        GameObjects.Add(Player);

        GameObjects.Add(new CactusGenerator());
    }
    #endregion

    #region Properties
    public Player Player { get; }
    public Ground Ground { get; }
    #endregion
}

