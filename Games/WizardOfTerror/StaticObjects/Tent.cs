using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace WizardOfTerror.StaticObjects;

public class Tent : TangibleGameObject
{
    #region Constrcutors
    public Tent(Vector2 location)
    {
        Location = location;
        var texture = Raylib.LoadTexture("Resources/2 Objects/8 Camp/1.png");
        Body = new Sprite(texture, 2f);
    }
    #endregion
    
    #region Overrides
    public override void Update(float delta)
    {
        //Do nothing
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