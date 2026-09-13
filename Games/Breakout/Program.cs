namespace Breakout
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (new BreakoutGame().Run()) { }
        }
    }
}
