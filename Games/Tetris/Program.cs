namespace Tetris;

internal class Program
{
    static void Main()
    {
        using var game = new TetrisGame();
        game.Run();
    }
}