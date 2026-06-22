using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace WizardOfTerror.Characters;

public class Hero : CharacterBase
{
    #region Constrcutors
    public Hero(Vector2 location, Go lookingDirection) : base(location, lookingDirection, "Resources/2") { }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        //Direction key pressed
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Up) && IsIdle)
        {
            LookingDirection = Go.Up;
            BodyUpRun.StartAnimation(true);
            IsIdle = false;
        }
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Down) && IsIdle)
        {
            LookingDirection = Go.Down;
            BodyDownRun.StartAnimation(true);
            IsIdle = false;
        }
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Left) && IsIdle)
        {
            LookingDirection = Go.Left;
            BodyLeftRun.StartAnimation(true);
            IsIdle = false;
        }
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Right) && IsIdle)
        {
            LookingDirection = Go.Right;
            BodyRightRun.StartAnimation(true);
            IsIdle = false;
        }

        //Direction key released
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

        //Attack key pressed
        IsAttacking = Game.KeyboardManager.IsKeyDown(KeyboardKey.Space) && !IsIdle;
        if (!IsIdle) Location = Location.Move(LookingDirection, delta * 150f);
    }
    #endregion
}