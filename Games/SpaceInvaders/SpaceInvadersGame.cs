using System.Numerics;
using FSCSharp;
using Raylib_cs;

namespace SpaceInvaders;

public class SpaceInvadersGame : Game<SpaceInvadersGame>
{
    #region Constructors
    public SpaceInvadersGame() : base("Space Invaders3", /*1800, 1200,*/ Color.Black)
    {
        Current.PlaySound("Resources/launch-sequence.mp3");
        Current.ShowSplashScreen("Resources/splash.png", 3000);

        Player = new PlayerShip(Vector2.Zero);
        Player.Location = Vector2.WindowCenter with { Y = Current.WindowHeight - Player.Body.Size.Y/2 - 40 };
        GameObjects.Add(Player);

        var enemySpawner = new EnemySpawner();
        GameObjects.Add(enemySpawner);

        var scoreKeeper = new ScoreKeeper();
        GameObjects.Add(scoreKeeper);
    }
    #endregion

    #region Properties
    public PlayerShip Player { get; }

    public Sound? MissileLaunch { get; set; }
    public Sound? Explosion { get; set; }
    #endregion
}