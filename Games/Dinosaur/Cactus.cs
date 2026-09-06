using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace Dinosaur;

public class Cactus : TangibleGameObject
{
    #region Constructors
    public Cactus()
    {
        var texture = Raylib.LoadTexture("Resources/cactus.png");
        Body = new Sprite(texture, 0.5f);

        Location = new Vector2(DinosaurGame.Current.WindowWidth + Body.Size.X/2, DinosaurGame.Current.Ground.GroundY - 50);
    }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        var player = DinosaurGame.Current.Player;
        if (Body.IsCollidingByPixelAtLocation(Location, player.Body, player.Location))
        {
            throw new GameOverException("You touched a cactus! You suck!");
        }

        Location = Location.Move(Go.Left, (int)(delta * 200));
        if (Location.X < -Body.Size.X/2) ToDelete = true;
    }
    public override void Draw(float delta)
    {
        Body.Draw(Location);
    }
    #endregion

    #region Properties
    public Vector2 Location { get; set; }
    public Sprite Body { get; set; }
    #endregion
}