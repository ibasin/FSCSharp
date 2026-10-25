namespace PingPong.Networking;

using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

public sealed class LanDiscovery : IDisposable
{
    #region Constructors
    public LanDiscovery()
    {
        Udp = new(Port);
        Udp.EnableBroadcast = true;
    }
    #endregion

    #region IDisposable Implementation
    public void Dispose() => Udp.Dispose();
    #endregion

    #region Methods
    // Run continuously in the background
    public async Task ListenAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            UdpReceiveResult result;

            try
            {
                result = await Udp.ReceiveAsync(ct);
            }
            catch (OperationCanceledException)
            {
                break;
            }

            string message = Encoding.UTF8.GetString(result.Buffer);
            if (message.StartsWith(DiscoveryMsg))
            {
                byte[] reply = Encoding.UTF8.GetBytes(ResponseMsg + Guid);
                await Udp.SendAsync(reply, result.RemoteEndPoint, ct);
            }
        }
    }

    //static async Task BroadcastDiscoveryAsync(byte[] request, int port)
    //{
    //    foreach (var (local, broadcast) in GetBroadcastAddresses())
    //    {
    //        using var client = new UdpClient(new IPEndPoint(local, 0));

    //        client.EnableBroadcast = true;

    //        await client.SendAsync(request, new IPEndPoint(broadcast, port));

    //        Console.WriteLine($"Broadcast: {local} -> {broadcast}");
    //    }
    //}

    static IEnumerable<(IPAddress Local, IPAddress Broadcast)> GetBroadcastAddresses()
    {
        foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (nic.OperationalStatus != OperationalStatus.Up) continue;

            if (nic.NetworkInterfaceType == NetworkInterfaceType.Loopback) continue;

            foreach (var info in nic.GetIPProperties().UnicastAddresses)
            {
                if (info.Address.AddressFamily != AddressFamily.InterNetwork) continue;

                //if (info.IPv4Mask == null) continue;

                byte[] ip = info.Address.GetAddressBytes();
                byte[] mask = info.IPv4Mask.GetAddressBytes();
                byte[] broadcast = new byte[4];

                for (int i = 0; i < 4; i++)
                {
                    broadcast[i] = (byte)(ip[i] | ~mask[i]);
                }

                yield return (info.Address, new IPAddress(broadcast));
            }
        }
    }

    // Discover other computers running the program
    public async Task<List<IPAddress>> FindPeersAsync(TimeSpan timeout)
    {
        Console.Write("Discovering peers...");
        var peers = new HashSet<IPAddress>();
        foreach (var (local, _) in GetBroadcastAddresses())
        {
            using var client = new UdpClient(new IPEndPoint(local, 0));
            client.EnableBroadcast = true;

            byte[] request = Encoding.UTF8.GetBytes(DiscoveryMsg + Guid);

            await client.SendAsync(request, new IPEndPoint(IPAddress.Broadcast, Port));

            using var cts = new CancellationTokenSource(timeout);

            while (!cts.IsCancellationRequested)
            {
                try
                {
                    var result = await client.ReceiveAsync(cts.Token);
                    string message = Encoding.UTF8.GetString(result.Buffer);
                    if (message.StartsWith(ResponseMsg) && message != ResponseMsg + Guid) peers.Add(result.RemoteEndPoint.Address);
                    Console.Write(".");
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
        Console.WriteLine();
        return peers.ToList();
    }

    public static void CheckNetworkConnection()
    {
        if (!NetworkInterface.GetIsNetworkAvailable())
        {
            Console.WriteLine("Network is not available");
            Environment.Exit(0);
        }
    }
    public static string GetLocalIpAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }
    #endregion

    #region Constants and Fields
    private const int Port = 50000;
    private const string DiscoveryMsg = "DISCOVER_PING_PONG_MSG_";
    private const string ResponseMsg = "RESPONSE_PING_PONG_MSG_";

    private UdpClient Udp { get; }
    private Guid Guid { get; } = Guid.NewGuid();
    #endregion
}

