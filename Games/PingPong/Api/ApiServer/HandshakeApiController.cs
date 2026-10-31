using PingPong.Networking;
using Supermodel.Presentation.WebMonk.Controllers.Api;

namespace PingPong.Api.ApiServer;

public class HandshakeInput
{
    public string RequestIP { get; set; } = null!;
}
public class HandshakeOutput
{
    public string ResponseIP { get; set; } = null!;
}

public class HandshakeApiController : CommandApiController<HandshakeInput, HandshakeOutput>
{
    protected override Task<HandshakeOutput> ExecuteAsync(HandshakeInput input)
    {
        Program.Peer = input.RequestIP;
        return Task.FromResult(new HandshakeOutput { ResponseIP = LanDiscovery.GetLocalIpAddress() });
    }
}
