namespace Tetris3D;

internal class Program
{
    static void Main(string[] args)
    {
        using (new Tetris3DGame().Run()) { }
    }
}