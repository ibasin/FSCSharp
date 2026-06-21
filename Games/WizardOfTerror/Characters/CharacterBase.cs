using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace WizardOfTerror.Characters;

public abstract class CharacterBase : TangibleGameObject
{
    #region Constrcutors
    protected CharacterBase(Vector2 location, Go lookingDirection, string directoryName)
    {
        Location = location;
        LookingDirection = lookingDirection;
        IsIdle = true;

        var frames = new List<Vector2>();
        for (var x = 48; x <= 576; x += 96)
        {
            frames.Add(new Vector2(x, 48));
        }

        var size = new Vector2(96, 96);

        //Idle
        var tilesLeftIdleTexture = Raylib.LoadTexture($"{directoryName}/S_Special.png");
        BodyLeftIdle = new AnimatedTilesSprite(tilesLeftIdleTexture, frames.ToArray(), size, 0.1f, 2f);

        var tilesRightIdleTexture = Raylib.LoadTexture($"{directoryName}/S_Special.png");
        BodyRightIdle = new AnimatedTilesSprite(tilesRightIdleTexture, frames.ToArray(), size, 0.1f, 2f);
        BodyRightIdle.FlipHorizontally = true;

        var tilesUpIdleTexture = Raylib.LoadTexture($"{ directoryName}/U_Special.png");
        BodyUpIdle = new AnimatedTilesSprite(tilesUpIdleTexture, frames.ToArray(), size, 0.1f, 2f);

        var tilesDownIdleTexture = Raylib.LoadTexture($"{directoryName}/D_Special.png");
        BodyDownIdle = new AnimatedTilesSprite(tilesDownIdleTexture, frames.ToArray(), size, 0.1f, 2f);

        //Run
        var tilesLeftRunTexture = Raylib.LoadTexture($"{directoryName}/S_Run.png");
        BodyLeftRun = new AnimatedTilesSprite(tilesLeftRunTexture, frames.ToArray(), size, 0.1f, 2f);

        var tilesRightRunTexture = Raylib.LoadTexture($"{directoryName}/S_Run.png");
        BodyRightRun = new AnimatedTilesSprite(tilesRightRunTexture, frames.ToArray(), size, 0.1f, 2f);
        BodyRightRun.FlipHorizontally = true;

        var tilesUpRunTexture = Raylib.LoadTexture($"{directoryName}/U_Run.png");
        BodyUpRun = new AnimatedTilesSprite(tilesUpRunTexture, frames.ToArray(), size, 0.1f, 2f);

        var tilesDownRunTexture = Raylib.LoadTexture($"{directoryName}/D_Run.png");
        BodyDownRun = new AnimatedTilesSprite(tilesDownRunTexture, frames.ToArray(), size, 0.1f, 2f);

        //Attack
        var tilesLeftAttackTexture = Raylib.LoadTexture($"{directoryName}/S_Attack.png");
        BodyLeftAttack = new AnimatedTilesSprite(tilesLeftAttackTexture, frames.ToArray(), size, 0.1f, 2f);

        var tilesRightAttackTexture = Raylib.LoadTexture($"{directoryName}/S_Attack.png");
        BodyRightAttack = new AnimatedTilesSprite(tilesRightAttackTexture, frames.ToArray(), size, 0.1f, 2f);
        BodyRightAttack.FlipHorizontally = true;

        var tilesUpAttackTexture = Raylib.LoadTexture($"{directoryName}/U_Attack.png");
        BodyUpAttack = new AnimatedTilesSprite(tilesUpAttackTexture, frames.ToArray(), size, 0.1f, 2f);

        var tilesDownAttackTexture = Raylib.LoadTexture($"{directoryName}/D_Attack.png");
        BodyDownAttack = new AnimatedTilesSprite(tilesDownAttackTexture, frames.ToArray(), size, 0.1f, 2f);
    }
    #endregion

    #region Overrides
    public override void Draw(float delta)
    {
        switch (LookingDirection)
        {
            case Go.Up:
            {
                if (IsAttacking)
                {
                    if (BodyUpAttack.IsAnimationStopped) BodyUpAttack.StartAnimation();
                    BodyUpAttack.DrawAnimation(Location, delta);
                }
                else
                {
                    if (IsIdle) BodyUpIdle.DrawAnimation(Location, delta);
                    else BodyUpRun.DrawAnimation(Location, delta);
                }
                break;
            }
            case Go.Down:
            {
                if (IsAttacking)
                {
                    if (BodyUpAttack.IsAnimationStopped) BodyDownAttack.StartAnimation();
                    BodyDownAttack.DrawAnimation(Location, delta);
                }
                else
                {
                    if (IsIdle) BodyDownIdle.DrawAnimation(Location, delta);
                    else BodyDownRun.DrawAnimation(Location, delta);
                }
                break;
            }
            case Go.Left:
            {
                if (IsAttacking)
                {
                    if (BodyLeftAttack.IsAnimationStopped) BodyLeftAttack.StartAnimation();
                    BodyLeftAttack.DrawAnimation(Location, delta);
                }
                else
                {
                    if (IsIdle) BodyLeftIdle.DrawAnimation(Location, delta);
                    else BodyLeftRun.DrawAnimation(Location, delta);
                }
                break;
            }
            case Go.Right:
            {
                if (IsAttacking)
                {
                    if (BodyRightAttack.IsAnimationStopped) BodyRightAttack.StartAnimation();
                    BodyRightAttack.DrawAnimation(Location, delta);
                }
                else
                {
                    if (IsIdle) BodyRightIdle.DrawAnimation(Location, delta);
                    else BodyRightRun.DrawAnimation(Location, delta);
                }
                break;
            }
        }
    }
    #endregion

    #region Properties
    public Vector2 Location { get; set; }
    public Go LookingDirection { get; set; }
    public bool IsIdle { get; set; }
    public bool IsAttacking { get; set; }

    public AnimatedTilesSprite BodyLeftIdle { get; set; }
    public AnimatedTilesSprite BodyRightIdle { get; set; }
    public AnimatedTilesSprite BodyUpIdle { get; set; }
    public AnimatedTilesSprite BodyDownIdle { get; set; }

    public AnimatedTilesSprite BodyLeftRun { get; set; }
    public AnimatedTilesSprite BodyRightRun { get; set; }
    public AnimatedTilesSprite BodyUpRun { get; set; }
    public AnimatedTilesSprite BodyDownRun { get; set; }

    public AnimatedTilesSprite BodyLeftAttack { get; set; }
    public AnimatedTilesSprite BodyRightAttack { get; set; }
    public AnimatedTilesSprite BodyUpAttack { get; set; }
    public AnimatedTilesSprite BodyDownAttack { get; set; }
    #endregion
}