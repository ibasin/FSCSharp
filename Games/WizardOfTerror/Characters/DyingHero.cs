using System.Numerics;
using FSCSharp;

namespace WizardOfTerror.Characters;

public class DyingHero : DyingCharacterBase
{
    public DyingHero(Vector2 location, Go lookingDirection) : base(location, lookingDirection, "Resources/2") { }
}