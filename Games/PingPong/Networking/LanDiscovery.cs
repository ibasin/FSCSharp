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
                byte[] reply = Encoding.UTF8.GetBytes(ResponseMsg + message.Substring(DiscoveryMsg.Length));
                await Udp.SendAsync(reply, result.RemoteEndPoint, ct);
            }
        }
    }

    // Discover other computers running the program
    public async Task<List<IPAddress>> FindPeersAsync(TimeSpan timeout)
    {
        using var client = new UdpClient(0);
        client.EnableBroadcast = true;

        var guid = Guid.NewGuid();
        byte[] request = Encoding.UTF8.GetBytes(DiscoveryMsg + guid);

        await client.SendAsync(request, new IPEndPoint(IPAddress.Broadcast, Port)); 

        var peers = new HashSet<IPAddress>();

        using var cts = new CancellationTokenSource(timeout);

        Console.Write("Discovering peers...");
        while (!cts.IsCancellationRequested)
        {
            try
            {
                var result = await client.ReceiveAsync(cts.Token);
                string message = Encoding.UTF8.GetString(result.Buffer);
                if (message == ResponseMsg + guid) peers.Add(result.RemoteEndPoint.Address);
                Console.Write(".");
            }
            catch (OperationCanceledException)
            {
                break;
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
    #endregion
}

