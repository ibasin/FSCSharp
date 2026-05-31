using System.Numerics;
using FSCSharp;
using Raylib_cs;

namespace SpaceInvaders;

public class EnemyShip : TangibleGameObject
{
    #region Constructors
    public EnemyShip(Vector2 location)
    {
        Location = location;

        Body = new Sprite(Raylib.LoadTexture("Resources/enemy.png"), 0.12f);
    }
    public override void Dispose()
    {
        base.Dispose();
        Body.Dispose();
    }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        MissileCooldown -= delta;
        if (MissileCooldown < 0) MissileCooldown = 0;

        if (MissileCooldown <= 0 && Random.Shared.Next(250) <= 5)
        {
            var missile = new Missile(Vector2.Zero, Go.Down);
            missile.Location = Location;
            SpaceInvadersGame.Current.GameObjects.Add(missile);

            MissileCooldown = 3f;

            SpaceInvadersGame.Current.PlaySound("Resources/missile-launch.mp3");
        }

        ActionCooldown -= delta;
        if (ActionCooldown <= 0)
        {
            var directionAngle = Random.Shared.NextSingle() * MathF.PI + 1.5f * MathF.PI;
            Velocity = new Vector2(MathF.Sin(directionAngle), MathF.Cos(directionAngle)) * (Random.Shared.NextSingle() * 50 + 75);

            ActionCooldown = Random.Shared.NextSingle() * 1f + 1f;
        }

        var location = Location + Velocity*delta;

        if (location.Move(Go.Down, Body.Size.Y/2).Violations.YPlusViolation)
        {
            SpaceInvadersGame.Current.PlaySound("Resources/explosion.mp3");

            var explosion = new Explosion(SpaceInvadersGame.Current.Player.Location);
            SpaceInvadersGame.Current.GameObjects.Add(explosion);

            if (SpaceInvadersGame.Current.Player.ToDelete != true)
            {
                SpaceInvadersGame.Current.Player.ToDelete = true;

                var pressEsc = "";
                if (Raylib.IsWindowFullscreen()) pressEsc = ". Press Esc.";
                throw new GameOverException($"An enemy got through! Score {ScoreKeeper.DeadEnemiesCount}{pressEsc}", explosion.Body.CalcAnimationLength());
            }
        }

        if (Body.IsFullyOnScreenAtLocation(location)) Location = location;
        else ActionCooldown = 0;

        for (var i = 0; i < SpaceInvadersGame.Current.GameObjects.Count; i++)
        {
            var gameObject = SpaceInvadersGame.Current.GameObjects[i];
            if (gameObject.ToDelete) continue;

            if (gameObject is EnemyShip && !ReferenceEquals(this, gameObject))
            {
                var otherEnemy = (EnemyShip)gameObject;
                if (otherEnemy.Body.IsCollidingAtLocation(otherEnemy.Location, Body, Location))
                {
                    SpaceInvadersGame.Current.PlaySound("Resources/explosion.mp3");

                    ToDelete = true;
                    otherEnemy.ToDelete = true;
                    EnemySpawner.EnemiesCount -= 2;
                    ScoreKeeper.DeadEnemiesCount += 2;

                    var explosion = new Explosion(otherEnemy.Location);
                    SpaceInvadersGame.Current.GameObjects.Add(explosion);
                }
            }

            if (gameObject is PlayerShip)
            {
                var player = (PlayerShip)gameObject;
                if (player.Body.IsCollidingAtLocation(player.Location, Body, Location))
                {
                    SpaceInvadersGame.Current.PlaySound("Resources/explosion.mp3");

                    var explosion = new Explosion(player.Location);
                    SpaceInvadersGame.Current.GameObjects.Add(explosion);

                    ScoreKeeper.DeadEnemiesCount++;

                    SpaceInvadersGame.Current.Player.ToDelete = true;

                    var pressEsc = "";
                    if (Raylib.IsWindowFullscreen()) pressEsc = ". Press Esc.";
                    throw new GameOverException($"You collided with an enemy! Score {ScoreKeeper.DeadEnemiesCount}{pressEsc}", explosion.Body.CalcAnimationLength());
                }
            }
        }
    }
    public override void Draw(float delta)
    {
        Body.Draw(Location);
    }
    #endregion

    #region Properties
    public Vector2 Location { get; set; }
    public Sprite Body { get; }

    public float ActionCooldown { get; protected set; }
    public Vector2 Velocity { get; protected set; }

    protected float MissileCooldown { get; set; }
    #endregion
}