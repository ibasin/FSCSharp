using FSCSharp;
using System.Numerics;

namespace WizardOfTerror.Characters;

public class Rat : NPCCharacterBase
{
    #region Constructors
    public Rat(Vector2 location, Go lookingDirection) : base(location, lookingDirection, "Resources/1") { }
    #endregion

    //#region Overrides
    //public override void Update(float delta)
    //{
    //    base.Update(delta);
        
    //}
    //#endregion
}