using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace WizardOfTerror.Characters;

public class AttackingRat : TangibleGameObject
{
    #region Constrcutors
    public AttackingRat(Vector2 location, Go lookingDirection)
    {
        var directoryName = "Resources/1";
        
        Location = location;
        LookingDirection = lookingDirection;

        var frames = new List<Vector2>();
        for (var x = 48; x <= 576; x += 96)
        {
            frames.Add(new Vector2(x, 48));
        }

        var size = new Vector2(96, 96);

        //Attack
        var tilesLeftAttackTexture = Raylib.LoadTexture($"{directoryName}/S_Attack.png");
        BodyLeftAttack = new AnimatedTilesSprite(tilesLeftAttackTexture, frames.ToArray(), size, 0.2f, 2f);

        var tilesRightAttackTexture = Raylib.LoadTexture($"{directoryName}/S_Attack.png");
        BodyRightAttack = new AnimatedTilesSprite(tilesRightAttackTexture, frames.ToArray(), size, 0.2f, 2f);
        BodyRightAttack.FlipHorizontally = true;

        var tilesUpAttackTexture = Raylib.LoadTexture($"{directoryName}/U_Attack.png");
        BodyUpAttack = new AnimatedTilesSprite(tilesUpAttackTexture, frames.ToArray(), size, 0.2f, 2f);

        var tilesDownAttackTexture = Raylib.LoadTexture($"{directoryName}/D_Attack.png");
        BodyDownAttack = new AnimatedTilesSprite(tilesDownAttackTexture, frames.ToArray(), size, 0.2f, 2f);

        CurrentBody.StartAnimation();
    }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        if (CurrentBody.IsAnimationStopped)
        {
            ToDelete = true;
            WOTGame.Current.GameObjects.Add(new Rat(Location, LookingDirection));
        }
    }
    public override void Draw(float delta)
    {
        CurrentBody.DrawAnimation(Location, delta);
    }
    public AnimatedTilesSprite CurrentBody
    {
        get
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
    }
    #endregion

    #region Properties
    public Vector2 Location { get; set; }
    public Go LookingDirection { get; set; }

    public AnimatedTilesSprite BodyLeftAttack { get; set; }
    public AnimatedTilesSprite BodyRightAttack { get; set; }
    public AnimatedTilesSprite BodyUpAttack { get; set; }
    public AnimatedTilesSprite BodyDownAttack { get; set; }
    #endregion
}