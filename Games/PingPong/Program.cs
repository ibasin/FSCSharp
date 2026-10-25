using FSCSharp;
using PingPong.Networking;
using Raylib_cs;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace PingPong;

public class Program
{
    static async Task Main()
    {
        Console.Write("Single player, multiplayer, demo, two computer multiplayer? (s/m/d/m2): ");
        var gameMode = Console.ReadLine()!.Trim().ToLower();

        if (gameMode == "m2")
        {
            LanDiscovery.CheckNetworkConnection();
            Console.WriteLine($"Your IP is: {LanDiscovery.GetLocalIpAddress()}. Listening for connections.");

            using var discovery = new LanDiscovery();
            using var cts = new CancellationTokenSource();

            _ = discovery.ListenAsync(cts.Token);

            Console.Write("Press enter when ready to find LAN games...");
            Console.ReadLine();

            Peers = await discovery.FindPeersAsync(TimeSpan.FromSeconds(10));
            for(var i = 0; i < Peers.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] Found peer: {Peers[i]}");
            }
            _ = cts.CancelAsync();
            Console.ReadLine();
        }

        using var game = new PingPongGame(gameMode);
        game.Run();
    }

    public static List<IPAddress> Peers { get; private set; } = new();
}
