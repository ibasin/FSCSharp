using System.Net;
using PingPong.Networking;

namespace PingPong;

public class Program
{
    static async Task Main()
    {
        Console.Write("Single player, multiplayer, demo, two computer multiplayer? (s/m/d/m2): ");
        var gameMode = Console.ReadLine()!.Trim().ToLower();

        if (gameMode == "m2")
        {
            using var discovery = new LanDiscovery();
            using var cts = new CancellationTokenSource();

            Task listener = discovery.ListenAsync(cts.Token);

            Console.WriteLine("Press enter when ready to find LAN games...");
            Console.ReadLine();

            Peers = await discovery.FindPeersAsync(TimeSpan.FromSeconds(2));
            foreach (var peer in Peers)
            {
                Console.WriteLine($"Found peer: {peer}");
            }
            Console.ReadLine();
        }

        using var game = new PingPongGame(gameMode);
        game.Run();
    }

    public static List<IPAddress> Peers { get; private set; } = new();
}
