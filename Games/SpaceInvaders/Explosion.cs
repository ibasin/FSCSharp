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
        Body = new AnimatedSprite(frames, 1.2f);

        TimePerFrame = 0.15f;
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
        TimeElapsed += delta;
        if (TimeElapsed >= TimePerFrame * Body.Frames.Length) ToDelete = true;
    }
    public override void Draw()
    {
        if (!ToDelete)
        {
            var frameIdx = Math.Min((int)(TimeElapsed / TimePerFrame), Body.Frames.Length - 1);
            Body.Draw(Location, frameIdx);
        }
    }
    #endregion

    #region Propeties
    public Vector2 Location { get; set; }
    public AnimatedSprite Body { get; }

    public float TimePerFrame { get; set; }
    public float TimeElapsed { get; set; }
    #endregion
}