namespace Tetris3D;

internal class Program
{
    static void Main()
    {
        using var game = new Tetris3DGame();
        game.Run();
    }
}