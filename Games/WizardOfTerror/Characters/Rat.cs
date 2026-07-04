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
        if (ToDelete) return;
        
        var hero = WOTGame.Current.Hero;
        if (hero.ToDelete) return;

        if (CurrentBody.IsCollidingByPixelAtLocation(Location, hero.CurrentBody, hero.Location))
        {
            if (hero.IsAttacking && CurrentBody.IsCollidingByPixelAtLocation(Location, hero.GetAttackingSpearCoordinates()))
            {
                WOTGame.Current.PlaySound("Resources/rat-dying.mp3");
                WOTGame.Current.GameObjects.Add(new DyingRat(Location, LookingDirection));
                ToDelete = true;
                if (WOTGame.Current.GameObjects.Count(x => x is Rat) <= 1) throw new GameOverException("You killed all rats!", 4);
            }
            else
            {
                Go lookingDirection;
                var locationDelta = hero.Location - Location;
                if (Math.Abs(locationDelta.X) > Math.Abs(locationDelta.Y)) lookingDirection = locationDelta.X < 0 ? Go.Left : Go.Right;
                else lookingDirection = locationDelta.Y < 0 ? Go.Up : Go.Down;

                WOTGame.Current.PlaySound("Resources/hero-dying.mp3");
                WOTGame.Current.GameObjects.Add(new AttackingRat(Location, lookingDirection));
                WOTGame.Current.GameObjects.Add(new DyingHero(hero.Location, hero.LookingDirection));

                ToDelete = true;
                hero.ToDelete = true;
                throw new GameOverException("A rat ate you!", 4);
            }
        }
    }
    #endregion
}