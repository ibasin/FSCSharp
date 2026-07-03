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
            IsIdle = false;
        }
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Down) && IsIdle)
        {
            LookingDirection = Go.Down;
            IsIdle = false;
        }
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Left) && IsIdle)
        {
            LookingDirection = Go.Left;
            IsIdle = false;
        }
        if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Right) && IsIdle)
        {
            LookingDirection = Go.Right;
            IsIdle = false;
        }

        //Direction key released
        if (Game.KeyboardManager.IsKeyReleased(KeyboardKey.Up))
        {
            LookingDirection = Go.Up;
            IsIdle = true;
        }
        if (Game.KeyboardManager.IsKeyReleased(KeyboardKey.Down))
        {
            LookingDirection = Go.Down;
            IsIdle = true;
        }
        if (Game.KeyboardManager.IsKeyReleased(KeyboardKey.Left))
        {
            LookingDirection = Go.Left;
            IsIdle = true;
        }
        if (Game.KeyboardManager.IsKeyReleased(KeyboardKey.Right))
        {
            LookingDirection = Go.Right;
            IsIdle = true;
        }

        //Attack key pressed
        IsAttacking = Game.KeyboardManager.IsKeyDown(KeyboardKey.Space) && !IsIdle;
        if (!IsIdle)
        {
            var location = IsAttacking ? Location.Move(LookingDirection, delta * 150f) : Location.Move(LookingDirection, delta * 200f);

            if (location.X < 80 && LookingDirection == Go.Left) location = Location;
            if (location.X > WOTGame.Current.WindowWidth - 80 && LookingDirection == Go.Right) location = Location;
            if (location.Y < 80 && LookingDirection == Go.Up) location = Location;
            if (location.Y > WOTGame.Current.WindowHeight - 80 && LookingDirection == Go.Down) location = Location;

            Location = location;
        }
    }

    public override AnimatedTilesSprite CurrentBody
    {
        get
        {
            if (IsIdle)
            {
                switch (LookingDirection)
                {
                    case Go.Up: return BodyUpIdle;
                    case Go.Down: return BodyDownIdle;
                    case Go.Left: return BodyLeftIdle;
                    case Go.Right: return BodyRightIdle;
                    default: throw new Exception("Invalid LookingDirection");
                }

            }
            else if (IsAttacking)
            {
                switch (LookingDirection)
                {
                    case Go.Up: return BodyUpAttack;
                    case Go.Down: return BodyDownAttack;
                    case Go.Left: return BodyLeftAttack;
                    case Go.Right: return BodyRightAttack;
                    default: throw new Exception("Invalid LookingDirection");
                }
            }
            else
            {
                switch (LookingDirection)
                {
                    case Go.Up: return BodyUpRun;
                    case Go.Down: return BodyDownRun;
                    case Go.Left: return BodyLeftRun;
                    case Go.Right: return BodyRightRun;
                    default: throw new Exception("Invalid LookingDirection");
                }
            }
        }
    }
    #endregion

    #region Methods
    public Vector2 GetAttackingSpearCoordinates()
    {
        if (!IsAttacking) throw new Exception("Can't get attacking Spear coordinates when not attacking!");

        switch (LookingDirection)
        {
            case Go.Up: return Location.Move(Go.Up, 100);
            case Go.Right: return Location.Move(Go.Right,100);
            case Go.Down: return Location.Move(Go.Down, 100);
            case Go.Left: return Location.Move(Go.Left, 100);
            default: throw new Exception("Invalid LookingDirection");
        }
    }
    #endregion

    #region Properties
    public bool IsIdle { get; set; } = true;
    public bool IsAttacking { get; set; }
    #endregion
}