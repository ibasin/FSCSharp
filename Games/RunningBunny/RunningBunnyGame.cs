using System.Numerics;
using FSCSharp;
using Raylib_cs;

namespace RunningBunny;

public class RunningBunnyGame : Game<RunningBunnyGame>
{
    public RunningBunnyGame() : base("Running Bunny", 1800, 1300, Color.Gray)
    {
        GameObjects.Add(new RunningBunny(Vector2.Zero));
    }
}