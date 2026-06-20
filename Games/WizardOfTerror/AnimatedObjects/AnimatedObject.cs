using System.Numerics;
using FSCSharp;
using Raylib_cs;

namespace WizardOfTerror.AnimatedObjects;

public abstract class AnimatedObject : TangibleGameObject
{
    #region Constrcutors
    protected AnimatedObject(Vector2 location, string textureFile, Vector2 size, int count)
    {
        Location = location;

        var frames = new List<Vector2>();
        for (var x = size.X/2; x <= size.X * count; x += size.X)
        {
            frames.Add(new Vector2(x, 3*size.Y/2));
        }

        var texture = Raylib.LoadTexture(textureFile);
        Body = new AnimatedTilesSprite(texture, frames.ToArray(), size, 0.1f, 2f);
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
        Body.DrawAnimation(Location, delta);
    }
    #endregion

    #region Propgress
    public Vector2 Location { get; set; }
    public AnimatedTilesSprite Body { get; set; }
    #endregion
}