namespace Breakout
{
    internal class Program
    {
        static void Main()
        {
            using (new BreakoutGame().Run()) { }
        }
    }
}
