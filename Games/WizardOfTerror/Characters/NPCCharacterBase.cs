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
            LookingDirection = (Go)Random.Shared.Next(4);
            DirectionDuration = Random.Shared.NextSingle() * 5f;
        }
        CurrentBody.StartAnimation();
        Location = Location.Move(LookingDirection, delta * Speed);
    }

    public override AnimatedTilesSprite CurrentBody 
    {
        get
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

    #endregion

    #region Properties
    public float DirectionDuration { get; set; }
    public float Speed { get; set; } = 100f;
    #endregion
}