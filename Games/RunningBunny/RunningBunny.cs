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

        Vector2[] frames = [new(0, 0)];
        Body = new AnimatedTilesSprite(timesTexture, frames, new(1000, 1000), 0.2f);
        Body.Looping = true;
    }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        //do nothing
    }
    public override void Draw(float delta)
    {
        Body.DrawAllTiles();
    }
    #endregion

    #region Properties
    public Vector2 Location { get; set; }
    public AnimatedTilesSprite Body { get; set; }
    #endregion

}