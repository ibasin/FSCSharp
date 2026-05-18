namespace Tetris
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (new TetrisGame().Run()) { }
        }
    }
}
