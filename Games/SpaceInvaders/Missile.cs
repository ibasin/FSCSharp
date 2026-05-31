using System.Numerics;
using FSCSharp;
using Raylib_cs;

namespace SpaceInvaders;

public class Missile : TangibleGameObject
{
    #region Constructors
    public Missile(Vector2 location, Go direction)
    {
        Location = location;
        Speed = 1200;
        Direction = direction;
        Body = new Sprite(Raylib.LoadTexture("Resources/missile.png"), 0.015f);
        if (Direction == Go.Down) Body.Rotation = 180;
        Priority = 90;
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
        var location = Location.Move(Direction, Speed*delta);

        if (!Body.IsFullyOnScreenAtLocation(location)) ToDelete = true;
        else Location = location;

        TimeSinceLaunch += delta;
        if (TimeSinceLaunch > 0.12) //is missile armed?
        {
            for(var i = 0; i < SpaceInvadersGame.Current.GameObjects.Count; i++)
            {
                var gameObject = SpaceInvadersGame.Current.GameObjects[i];
                if (gameObject.ToDelete) continue;


                if (gameObject is Missile && !ReferenceEquals(this, gameObject))
                {
                    var otherMissile = (Missile)gameObject;
                    if (Body.IsCollidingAtLocation(Location, otherMissile.Body, otherMissile.Location))
                    {
                        SpaceInvadersGame.Current.PlaySound("Resources/explosion.mp3");

                        ToDelete = true;
                        otherMissile.ToDelete = true;

                        var explosion = new Explosion(otherMissile.Location);
                        SpaceInvadersGame.Current.GameObjects.Add(explosion);
                    }
                }

                if (gameObject is EnemyShip)
                {
                    var enemy = (EnemyShip)gameObject;
                    if (Body.IsCollidingAtLocation(Location, enemy.Body, enemy.Location))
                    {
                        SpaceInvadersGame.Current.PlaySound("Resources/explosion.mp3");

                        ToDelete = true;
                        enemy.ToDelete = true;
                        EnemySpawner.EnemiesCount--;
                        ScoreKeeper.DeadEnemiesCount++;

                        var explosion = new Explosion(enemy.Location);
                        SpaceInvadersGame.Current.GameObjects.Add(explosion);
                    }
                }

                if (gameObject is PlayerShip)
                {
                    var player = (PlayerShip)gameObject;
                    if (Body.IsCollidingAtLocation(Location, player.Body, player.Location))
                    {
                        SpaceInvadersGame.Current.PlaySound("Resources/explosion.mp3");

                        var explosion = new Explosion(player.Location);
                        SpaceInvadersGame.Current.GameObjects.Add(explosion);

                        ToDelete = true;
                        player.ToDelete = true;

                        var pressEsc = "";
                        if (Raylib.IsWindowFullscreen()) pressEsc = ". Press Esc.";
                        throw new GameOverException($"You got shot by an enemy missile! Score {ScoreKeeper.DeadEnemiesCount}{pressEsc}", explosion.Body.CalcAnimationLength());
                    }
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
    public float Speed { get; }

    public Sprite Body { get; }
    public Go Direction { get; }

    protected float TimeSinceLaunch { get; set; }
    #endregion
}