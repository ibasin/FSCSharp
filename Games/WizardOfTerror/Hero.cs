using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace WizardOfTerror;

public class Hero : TangibleGameObject
{
    #region Constrcutors
    public Hero(Vector2 location, Go lookingDirection)
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
        var tilesLeftIdleTexture = Raylib.LoadTexture("Resources/2/L_Special.png");
        BodyLeftIdle = new AnimatedTilesSprite(tilesLeftIdleTexture, frames.ToArray(), size, 0.1f, 2f);
        
        var tilesRightIdleTexture = Raylib.LoadTexture("Resources/2/R_Special.png");
        BodyRightIdle = new AnimatedTilesSprite(tilesRightIdleTexture, frames.ToArray(), size, 0.1f, 2f);

        var tilesUpIdleTexture = Raylib.LoadTexture("Resources/2/U_Special.png");
        BodyUpIdle = new AnimatedTilesSprite(tilesUpIdleTexture, frames.ToArray(), size, 0.1f, 2f);

        var tilesDownIdleTexture = Raylib.LoadTexture("Resources/2/D_Special.png");
        BodyDownIdle = new AnimatedTilesSprite(tilesDownIdleTexture, frames.ToArray(), size, 0.1f, 2f);

        //Run
        var tilesLeftRunTexture = Raylib.LoadTexture("Resources/2/L_Run.png");
        BodyLeftRun = new AnimatedTilesSprite(tilesLeftRunTexture, frames.ToArray(), size, 0.1f, 2f);

        var tilesRightRunTexture = Raylib.LoadTexture("Resources/2/R_Run.png");
        BodyRightRun = new AnimatedTilesSprite(tilesRightRunTexture, frames.ToArray(), size, 0.1f, 2f);

        var tilesUpRunTexture = Raylib.LoadTexture("Resources/2/U_Run.png");
        BodyUpRun = new AnimatedTilesSprite(tilesUpRunTexture, frames.ToArray(), size, 0.1f, 2f);

        var tilesDownRunTexture = Raylib.LoadTexture("Resources/2/D_Run.png");
        BodyDownRun = new AnimatedTilesSprite(tilesDownRunTexture, frames.ToArray(), size, 0.1f, 2f);
    }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.Up) && IsIdle)
        {
            LookingDirection = Go.Up;
            BodyUpRun.StartAnimation(true);
            IsIdle = false;
        }
        if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.Down) && IsIdle)
        {
            LookingDirection = Go.Down;
            BodyDownRun.StartAnimation(true);
            IsIdle = false;
        }
        if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.Left) && IsIdle)
        {
            LookingDirection = Go.Left;
            BodyLeftRun.StartAnimation(true);
            IsIdle = false;
        }
        if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.Right) && IsIdle)
        {
            LookingDirection = Go.Right;
            BodyRightRun.StartAnimation(true);
            IsIdle = false;
        }

        if (Game.KeyboardManager.IsKeyReleased(KeyboardKey.Up))
        {
            LookingDirection = Go.Up;
            BodyUpIdle.StartAnimation(true);
            IsIdle = true;
        }
        if (Game.KeyboardManager.IsKeyReleased(KeyboardKey.Down))
        {
            LookingDirection = Go.Down;
            BodyDownIdle.StartAnimation(true);
            IsIdle = true;
        }
        if (Game.KeyboardManager.IsKeyReleased(KeyboardKey.Left))
        {
            LookingDirection = Go.Left;
            BodyLeftIdle.StartAnimation(true);
            IsIdle = true;
        }
        if (Game.KeyboardManager.IsKeyReleased(KeyboardKey.Right))
        {
            LookingDirection = Go.Right;
            BodyRightIdle.StartAnimation(true);
            IsIdle = true;
        }

    }

    public override void Draw(float delta)
    {
        switch (LookingDirection)
        {
            case Go.Up:
            {
                if (IsIdle) BodyUpIdle.DrawAnimation(Location, delta);
                else BodyUpRun.DrawAnimation(Location, delta);

                break;
            }
            case Go.Down:
            {
                if (IsIdle) BodyDownIdle.DrawAnimation(Location, delta);
                else BodyDownRun.DrawAnimation(Location, delta);

                break;
            }
            case Go.Left:
            {
                if (IsIdle) BodyLeftIdle.DrawAnimation(Location, delta);
                else BodyLeftRun.DrawAnimation(Location, delta);

                break;
            }
            case Go.Right:
            {
                if (IsIdle) BodyRightIdle.DrawAnimation(Location, delta);
                else BodyRightRun.DrawAnimation(Location, delta);

                break;
            }
        }
    }
    #endregion

    #region Properties
    public Vector2 Location { get; set; }
    public Go LookingDirection { get; set; }
    public bool IsIdle { get; set; }

    public AnimatedTilesSprite BodyLeftIdle { get; set; }
    public AnimatedTilesSprite BodyRightIdle { get; set; }
    public AnimatedTilesSprite BodyUpIdle { get; set; }
    public AnimatedTilesSprite BodyDownIdle { get; set; }

    public AnimatedTilesSprite BodyLeftRun { get; set; }
    public AnimatedTilesSprite BodyRightRun { get; set; }
    public AnimatedTilesSprite BodyUpRun { get; set; }
    public AnimatedTilesSprite BodyDownRun { get; set; }

    #endregion
}