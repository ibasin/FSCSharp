using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace Dinosaur;

public class Player : TangibleGameObject
{
    #region Constructor
    public Player(Vector2 location)
    {
        Location = location;
        var texture = Raylib.LoadTexture("Resources/yoshi.png");
        Body = new Sprite(texture, 0.5f);
    }
    #endregion
    
    #region Overrides
    public override void Update(float delta)
    {
        //do nothing
    }
    public override void Draw()
    {
        Body.Draw(Location);
    }
    #endregion

    #region Properties
    public Vector2 Location { get; set; }
    public Sprite Body { get; set; }
    #endregion
}
