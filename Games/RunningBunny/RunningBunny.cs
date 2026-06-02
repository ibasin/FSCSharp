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
        var tilesTexture = Raylib.LoadTexture("Resources/running-bunny-tiles.png");

        var frames = new List<Vector2>();
        for (var y = 405; y <= 923; y += 518)
        {
            for (var x = 300; x <= 1800; x += 475)
            {
                frames.Add(new Vector2(x, y));
            }
        }
        var size = new Vector2(475, 518);
        Body = new AnimatedTilesSprite(tilesTexture, frames.ToArray(), size, 1f, 0.5f);
        Body.StartAnimation(true);

        ////Pre - render
        //Raylib.BeginDrawing();
        //Raylib.ClearBackground(Color.White);
        //for (var i = 0; i < frames.Count; i++)
        //{
        //    Body.Draw(size, i);
        //}
        //Raylib.EndDrawing();
    }
    public override void Dispose()
    {
        Body.Dispose();
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