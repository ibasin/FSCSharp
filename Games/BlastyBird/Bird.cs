using FSCSharp;
using Raylib_cs;
using System.Data.Common;
using System.Numerics;

namespace BlastyBird;

public class Bird : VisibleGameObject
{
    #region Constructor
    public Bird(Vector2 location)
    {
        Location = location;

        var texture = Raylib.LoadTexture("Resources/bird.png");
        Body = new Sprite(texture, 0.5f);
    }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        
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
