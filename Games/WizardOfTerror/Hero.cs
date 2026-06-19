using System.Numerics;
using FSCSharp;

namespace WizardOfTerror;

public class Hero : TangibleGameObject
{
    #region Constrcutors
    public Hero(Vector2 location, Go lookingDirection)
    {
        Location = location;
        LookingDirection = lookingDirection;


    }
    #endregion
    
    #region Overrides
    public override void Update(float delta)
    {
        throw new NotImplementedException();
    }

    public override void Draw(float delta)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Properties
    public Vector2 Location { get; set; }
    public Go LookingDirection { get; set; }

    public AnimatedTilesSprite BodySide { get; set; }
    public AnimatedTilesSprite BodyUp { get; set; }
    public AnimatedTilesSprite BodyDown { get; set; }
    #endregion
}