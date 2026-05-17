using System.Numerics;
using FSCSharp;

namespace SpaceInvaders;

public class EnemySpawner : GameObject
{
    #region Overrides
    public override void Update(float delta)
    {
        TimeSinceLastEnemySpawn += delta;
        if ((EnemiesCount < SpaceInvadersGame.Current.WindowWidth / 300 && Random.Shared.Next(500) < TimeSinceLastEnemySpawn) || EnemiesCount == 0)
        {
            var enemyShip = new EnemyShip(Vector2.Zero);
            var randomX = Random.Shared.Next(SpaceInvadersGame.Current.WindowWidth - enemyShip.Body.Size.IntX) + enemyShip.Body.Size.IntX/2;
            enemyShip.Location = new Vector2(randomX, 10 + enemyShip.Body.Size.Y / 2);
            SpaceInvadersGame.Current.GameObjects.Add(enemyShip);
            EnemiesCount++;
            TimeSinceLastEnemySpawn = 0f;
        }
    }
    #endregion

    #region Properties
    public static int EnemiesCount { get; set; }
    public float TimeSinceLastEnemySpawn { get; set; }
    #endregion
}