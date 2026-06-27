using System.Numerics;
using FSCSharp;

namespace WizardOfTerror.Characters;

public abstract class NPCCharacterBase : CharacterBase
{
    #region Constructors
    protected NPCCharacterBase(Vector2 location, Go lookingDirection, string directoryName) : base(location, lookingDirection, directoryName) { }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        DirectionDuration -= delta;

        if (Location.X < 50 && LookingDirection == Go.Left) DirectionDuration = 0;
        if (Location.X > WOTGame.Current.WindowWidth - 50 && LookingDirection == Go.Right) DirectionDuration = 0;
        if (Location.Y < 50 && LookingDirection == Go.Up) DirectionDuration = 0;
        if (Location.Y > WOTGame.Current.WindowHeight - 50 && LookingDirection == Go.Down) DirectionDuration = 0;

        if (DirectionDuration <= 0)
        {
            //new direction key determined
            var rndDir = Random.Shared.Next(4);
            DirectionDuration = Random.Shared.NextSingle() * 5f;


            if (rndDir == 0)
            {
                LookingDirection = Go.Up;
                BodyUpRun.StartAnimation(true);
                IsIdle = false;
            }
            if (rndDir == 1)
            {
                LookingDirection = Go.Down;
                BodyDownRun.StartAnimation(true);
                IsIdle = false;
            }
            if (rndDir == 2)
            {
                LookingDirection = Go.Left;
                BodyLeftRun.StartAnimation(true);
                IsIdle = false;
            }
            if (rndDir == 3)
            {
                LookingDirection = Go.Right;
                BodyRightRun.StartAnimation(true);
                IsIdle = false;
            }
        }
        Location = Location.Move(LookingDirection, delta * Speed);
    }
    #endregion

    #region Properties
    public float DirectionDuration { get; set; }
    public float Speed { get; set; } = 100f;
    #endregion
}