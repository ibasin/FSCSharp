using FSCSharp;
using System.Numerics;
using Raylib_cs;

namespace PingPong;

public abstract class Paddle : TangibleGameObject
{
    #region Constructors
    protected Paddle(Vector2 location, Color color)
    {
        Location = location;
        Color = color;
    }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        Location = Location with { Y = Math.Clamp(Location.Y, Size.Y / 2, PingPongGame.Current.WindowHeight - Size.Y / 2) };
    }
    public override void Draw(float delta)
    {
        Raylib.DrawRectangle((Location.X - Size.X / 2).RoundToInt(), (Location.Y - Size.Y / 2).RoundToInt(), Size.X.RoundToInt(), Size.Y.RoundToInt(), Color);
    }
    #endregion

    #region Properties
    public Vector2 Location { get; set; }
    public Color Color { get; }

    protected float Cooldown { get; set; }

    public static Vector2 Size { get; } = new(15, 120);
    public static float Velocity => 850;
    #endregion
}
