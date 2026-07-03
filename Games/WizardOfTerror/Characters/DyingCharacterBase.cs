using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace WizardOfTerror.Characters;

public abstract class DyingCharacterBase : TangibleGameObject
{
    #region Constrcutors
    protected DyingCharacterBase(Vector2 location, Go lookingDirection, string directoryName)
    {
        Location = location;
        LookingDirection = lookingDirection;

        var frames = new List<Vector2>();
        for (var x = 48; x <= 576; x += 96)
        {
            frames.Add(new Vector2(x, 48));
        }

        var size = new Vector2(96, 96);

        //Idle
        var tilesLeftIdleTexture = Raylib.LoadTexture($"{directoryName}/S_Death.png");
        BodyLeft = new AnimatedTilesSprite(tilesLeftIdleTexture, frames.ToArray(), size, 0.2f, 2f);

        var tilesRightIdleTexture = Raylib.LoadTexture($"{directoryName}/S_Death.png");
        BodyRight = new AnimatedTilesSprite(tilesRightIdleTexture, frames.ToArray(), size, 0.2f, 2f);
        BodyRight.FlipHorizontally = true;

        var tilesUpIdleTexture = Raylib.LoadTexture($"{directoryName}/U_Death.png");
        BodyUp = new AnimatedTilesSprite(tilesUpIdleTexture, frames.ToArray(), size, 0.2f, 2f);

        var tilesDownIdleTexture = Raylib.LoadTexture($"{directoryName}/D_Death.png");
        BodyDown = new AnimatedTilesSprite(tilesDownIdleTexture, frames.ToArray(), size, 0.2f, 2f);

        CurrentBody.StartAnimation();
    }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        if (CurrentBody.IsAnimationStopped) ToDelete = true;
    }
    public override void Draw(float delta)
    {
        CurrentBody.DrawAnimation(Location, delta);
    }
    public AnimatedTilesSprite CurrentBody
    {
        get
        {
            switch (LookingDirection)
            {
                case Go.Up: return BodyUp;
                case Go.Down: return BodyDown;
                case Go.Left: return BodyLeft;
                case Go.Right: return BodyRight;
                default: throw new Exception("Invalid LookingDirection");
            }
        }
    }
    #endregion

    #region Properties
    public Vector2 Location { get; set; }
    public Go LookingDirection { get; set; }

    public AnimatedTilesSprite BodyLeft { get; set; }
    public AnimatedTilesSprite BodyRight { get; set; }
    public AnimatedTilesSprite BodyUp { get; set; }
    public AnimatedTilesSprite BodyDown { get; set; }
    #endregion

}