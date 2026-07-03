using System.Numerics;
using FSCSharp;

namespace WizardOfTerror.Characters;

public class DyingRat : DyingCharacterBase
{
    public DyingRat(Vector2 location, Go lookingDirection) : base(location, lookingDirection, "Resources/1") { }
}