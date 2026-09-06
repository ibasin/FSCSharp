namespace Tetris;

internal class Program
{
    static void Main()
    {
        using (new TetrisGame().Run()) { }
    }
}