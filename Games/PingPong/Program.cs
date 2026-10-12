using System.Runtime.InteropServices;

namespace PingPong;

public class Program
{
    static void Main()
    {
        using var game = new PingPongGame();
        game.Run();
    }
}
