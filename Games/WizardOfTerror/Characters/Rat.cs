using FSCSharp;
using System.Numerics;

namespace WizardOfTerror.Characters;

public class Rat : NPCCharacterBase
{
    #region Constructors
    public Rat(Vector2 location, Go lookingDirection) : base(location, lookingDirection, "Resources/1") { }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        base.Update(delta);
        var hero = WOTGame.Current.Hero;

        if (CurrentBody.IsCollidingByPixelAtLocation(Location, hero.CurrentBody, hero.Location))
        {
            if (hero.IsAttacking && CurrentBody.IsCollidingByPixelAtLocation(Location, hero.GetAttackingSpearCoordinates()))
            {
                ToDelete = true;
                WOTGame.Current.PlaySound("Resources/rat-dying.mp3");
            }
            else
            {
                WOTGame.Current.PlaySound("Resources/hero-dying.mp3");
                WOTGame.Current.GameObjects.Add(new DyingHero(hero.Location, hero.LookingDirection));
                hero.ToDelete = true;
                throw new GameOverException("A rat ate you!", 5);
            }
        }
    }
    #endregion
}