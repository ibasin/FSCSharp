namespace Cube;

public class Program
{
    static void Main()
    {
        using var game = new CubeGame();
        game.Run();
    }
}