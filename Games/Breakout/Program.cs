namespace Breakout;

internal class Program
{
    static void Main()
    {
        using var game = new BreakoutGame();
        game.Run();

    }
}