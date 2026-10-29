using Supermodel.Client.Backend.DataContext.WebApi;

namespace PingPong.ApiClient;

public class PingPongWebApiDataContext : WebApiDataContext
{
    public override string BaseUrl => $"http://{Program.Peer}:{Program.HttpPort}/api/";
}
