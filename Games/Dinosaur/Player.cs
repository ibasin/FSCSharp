using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace Dinosaur;

public class Player : TangibleGameObject
{
    #region Constructor
    public Player()
    {
        var texture = Raylib.LoadTexture("Resources/yoshi.png");
        Body = new Sprite(texture, 0.5f);

        GroundZeroY = DinosaurGame.Current.Ground.GroundY - Body.Size.Y / 2;
        Location = Location = new Vector2(DinosaurGame.Current.WindowWidth * 0.10f, GroundZeroY);
    }
    #endregion
    
    #region Overrides
    public override void Update(float delta)
    {
        if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.Space) && (int)Location.Y == (int)GroundZeroY)
        {
            VerticalV = -500;
        }
        Location = Location.Move(Go.Down, VerticalV * delta);
        if (Location.Y > GroundZeroY) Location = Location with { Y = GroundZeroY };
        VerticalV += 600 * delta;
    }
    public override void Draw(float delta)
    {
        Body.Draw(Location);
    }
    #endregion

    #region Properties
    public float VerticalV { get; set; } 
    public float GroundZeroY { get; set; }
    public Vector2 Location { get; set; }
    public Sprite Body { get; set; }
    #endregion
}
