using PingPong.ApiClient;
using PingPong.Networking;
using Supermodel.ApiClient.Models;
using WebMonk;

namespace PingPong;

public static class Program
{
    static async Task Main()
    {
        Console.Write("Single player, multiplayer, demo, two computer multiplayer? (s/m/d/m2): ");
        var gameMode = Console.ReadLine()!.Trim().ToLower();

        if (gameMode == "m2")
        {
            LanDiscovery.CheckNetworkConnection();
            var myIp = LanDiscovery.GetLocalIpAddress();
            Console.WriteLine($"\nYour IP is: {myIp}");

            if (!Firewall.IsPortOpen(HttpPort)) Firewall.OpenPort(HttpPort);
            var webServer = new WebServer(HttpPort, $"http://{myIp}:{HttpPort}");
            _ = webServer.RunAsync();

            using var discovery = new LanDiscovery();
            using var cts = new CancellationTokenSource();

            _ = discovery.ListenAsync(cts.Token);

            Console.Write("Press enter when ready to find LAN games...");
            Console.ReadLine();

            var peers = await discovery.FindPeersAsync(TimeSpan.FromSeconds(2));
            if (peers.Count == 0)
            {
                Console.WriteLine("No peers found. Press enter to exit.");
                Console.ReadLine();
                return;
            }
            for (var i = 0; i < peers.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] Found peer: {peers[i]}");
            }
            Console.Write("Pick a peer by entering its number: ");
            while (true)
            {
                await Task.Delay(100, cts.Token);

                if (Peer != null)
                {
                    Console.WriteLine($"\nHandshake with {Peer} successful!");
                    IsPlayingLeft = false;
                    break;
                }
                if (!Console.KeyAvailable) continue;

                var keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("\nEscape pressed. Exiting...");
                    return;
                }

                var ch = keyInfo.KeyChar;
                Console.WriteLine(ch);
                if (char.IsDigit(ch))
                {
                    var index = int.Parse(ch.ToString()) - 1;
                    if (index >= 0 && index < peers.Count)
                    {
                        Console.WriteLine($"Connecting to {peers[index]}...");
                        Peer = peers[index].ToString();
                        _ = cts.CancelAsync();
                        
                        var webApiDataContext = new PingPongWebApiDataContext();
                        var handshakeOutput = await webApiDataContext.HandshakeAsync(new HandshakeInput { RequestIP = myIp });
                        if (handshakeOutput.ResponseIP == Peer)
                        {
                            Console.WriteLine($"Handshake with {Peer} successful!");
                            IsPlayingLeft = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Handshake failed. Expected {Peer}, but got {handshakeOutput.ResponseIP}. Exiting...");
                            return;
                        }
                    }
                }
                Console.Write("Invalid selection. Try again: ");
            }

            //handshake
            

            Console.ReadLine();
        }

        using var game = new PingPongGame(gameMode);
        game.Run();
    }

    public static string? Peer { get; set; }
    public const int HttpPort = 50001;
    public static bool IsPlayingLeft { get; set; }
}
