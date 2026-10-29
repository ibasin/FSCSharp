namespace PingPong.Networking;

public static class Firewall
{
    public static void OpenPort(int port, bool udp = false)
    {
        if (port < 1 || port > 65535) throw new ArgumentOutOfRangeException(nameof(port));

        Type policyType = Type.GetTypeFromProgID("HNetCfg.FwPolicy2", throwOnError: true)!;

        Type ruleType = Type.GetTypeFromProgID("HNetCfg.FWRule", throwOnError: true)!;

        dynamic policy = Activator.CreateInstance(policyType)!;
        dynamic rule = Activator.CreateInstance(ruleType)!;

        rule.Name = $"MyApp {(udp ? "UDP" : "TCP")} {port}";
        rule.Description = "Allow incoming connections for MyApp";

        rule.Protocol = udp ? 17 : 6;  // UDP = 17, TCP = 6
        rule.LocalPorts = port.ToString();

        rule.Direction = 1;           // Inbound
        rule.Action = 1;              // Allow
        rule.Enabled = true;

        rule.Profiles = 2;            // Private networks only
        rule.RemoteAddresses = "LocalSubnet";

        policy.Rules.Add(rule);
    }

    public static void ClosePort(int port, bool udp = false)
    {
        Type policyType = Type.GetTypeFromProgID("HNetCfg.FwPolicy2", throwOnError: true)!;

        dynamic policy = Activator.CreateInstance(policyType)!;

        string name = $"MyApp {(udp ? "UDP" : "TCP")} {port}";

        policy.Rules.Remove(name);
    }
}