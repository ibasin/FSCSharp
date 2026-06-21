using System.Numerics;
using FSCSharp;

namespace WizardOfTerror.Characters;

public class Mage : NPCCharacterBase
{
    #region Constructors
    public Mage(Vector2 location, Go lookingDirection) : base(location, lookingDirection, "Resources/3") { }
    #endregion
}