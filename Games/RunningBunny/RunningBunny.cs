using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace RunningBunny;

public class RunningBunny : TangibleGameObject
{
    #region Constructor
    public RunningBunny(Vector2 location)
    {
        Location = location;
        var timesTexture = Raylib.LoadTexture("Resources/running-bunny-tiles.png");

        Vector2[] frames = [new(290, 405), new(770, 405), new(1250, 405), new(2310, 405),
                            new(290, 923), new(770, 923), new(1250, 923), new(2310, 923)];
        Body = new AnimatedTilesSprite(timesTexture, frames, new(390, 470), 0.7f, 0.5f);
        Body.StartAnimation(true);
    }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        //do nothing
    }
    public override void Draw(float delta)
    {
        //Body.DrawAllTiles();

        // ReSharper disable PossibleLossOfFraction
        Body.DrawAnimation(new Vector2(RunningBunnyGame.Current.WindowWidth/2, RunningBunnyGame.Current.WindowHeight / 2), delta);
        // ReSharper restore PossibleLossOfFraction
    }
    #endregion

    #region Properties
    public Vector2 Location { get; set; }
    public AnimatedTilesSprite Body { get; set; }
    #endregion

}