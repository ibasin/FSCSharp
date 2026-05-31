using FSCSharp;
using Raylib_cs;

namespace Dinosaur;

public class Ground : TangibleGameObject
{
    #region Constructors
    public Ground()
    {
        GroundY = (int)MathF.Round(DinosaurGame.Current.WindowHeight * 0.80f);
    }
    #endregion
    
    #region Overrides
    public override void Update(float delta)
    {
        //do nothing
    }
    public override void Draw(float delta)
    {
        Raylib.DrawRectangle(0, GroundY, DinosaurGame.Current.WindowWidth, DinosaurGame.Current.WindowHeight, Color.DarkGreen);
    }
    #endregion

    #region Properties
    public int GroundY { get; }
    #endregion
}