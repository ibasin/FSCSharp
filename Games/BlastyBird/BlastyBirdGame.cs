using System.Numerics;
using FSCSharp;
using Raylib_cs;

namespace BlastyBird;

public class BlastyBirdGame : Game<BlastyBirdGame>
{
    public BlastyBirdGame() : base("Blasty Bird", 1150, 750, Color.SkyBlue)
    {
        var location = new Vector2(WindowWidth * 0.1f, WindowHeight / 2f);
        var bird = new Bird(location);
        GameObjects.Add(bird);
    }
}
