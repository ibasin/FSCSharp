using System.Numerics;
using FSCSharp;
using Raylib_cs;

namespace SpaceInvaders;

public class Explosion : TangibleGameObject
{
    #region Constructors
    public Explosion(Vector2 location)
    {
        Location = location;

        Texture2D[] frames =
        [
            Raylib.LoadTexture("Resources/explosion1.png"),
            Raylib.LoadTexture("Resources/explosion2.png"),
            Raylib.LoadTexture("Resources/explosion3.png"),
            Raylib.LoadTexture("Resources/explosion4.png")
        ];
        Body = new AnimatedArraySprite(frames, 0.15f, 1.2f);
        Body.StartAnimation();
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
        if (Body.IsAnimationStopped) ToDelete = true;
    }
    public override void Draw(float delta)
    {
        Body.DrawAnimation(Location, delta);
    }
    #endregion

    #region Propeties
    public Vector2 Location { get; set; }
    public AnimatedArraySprite Body { get; }
    #endregion
}