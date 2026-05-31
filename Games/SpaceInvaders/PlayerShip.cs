using System.Numerics;
using FSCSharp;
using Raylib_cs;

namespace SpaceInvaders;

public class PlayerShip : TangibleGameObject
{
    #region Constructors
    public PlayerShip(Vector2 location)
    {
        Location = location;
        Speed = 500;
        Body = new Sprite(Raylib.LoadTexture("Resources/player.png"), 0.28f);
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

        while (true)
        {
            var key = Game.KeyboardManager.ReadKey();
            if (key == KeyboardKey.Null) break;

            var location = Location;

            if (key == KeyboardKey.Right) location = Location.Move(Go.Right, Speed*delta);
            if (key == KeyboardKey.Left) location = Location.Move(Go.Left, Speed * delta);
            if (key== KeyboardKey.Space && MissileCooldown <= 0)
            {
                var missile1 = new Missile(Vector2.Zero, Go.Up);
                missile1.Location = Location.Move(Go.Left, Body.Size.X/4);
                SpaceInvadersGame.Current.GameObjects.Add(missile1);

                var missile2 = new Missile(Vector2.Zero, Go.Up);
                missile2.Location = Location.Move(Go.Right, Body.Size.X / 4);
                SpaceInvadersGame.Current.GameObjects.Add(missile2);

                MissileCooldown = 0.7f;

                SpaceInvadersGame.Current.MissileLaunch ??= Raylib.LoadSound("Resources/missile-launch.mp3");
                SpaceInvadersGame.Current.PlaySound(SpaceInvadersGame.Current.MissileLaunch.Value);
            }

            if (Body.IsFullyOnScreenAtLocation(location)) Location = location;
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
    public float Speed { get; }

    protected float MissileCooldown { get; set; }
    #endregion
}