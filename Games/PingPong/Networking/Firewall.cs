using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PingPong.Networking;

public static class Firewall
{
    public static void OpenPort(int port)
    {
        var psi = new ProcessStartInfo("netsh")
        {
            UseShellExecute = true,
            Verb = "runas",
            Arguments =
                $"advfirewall firewall add rule " +
                $"name=\"MyApp TCP {port}\" " +
                $"dir=in action=allow protocol=TCP " +
                $"localport={port} profile=any"
        };

        using var process = Process.Start(psi) ?? throw new InvalidOperationException("Could not start netsh.");

        process.WaitForExit();

        if (process.ExitCode != 0) throw new Exception("Failed to configure Windows Firewall.");
    }

    public static bool IsPortOpen(int port)
    {
        Type policyType = Type.GetTypeFromProgID("HNetCfg.FwPolicy2", throwOnError: true)!;

        dynamic policy = Activator.CreateInstance(policyType)!;

        string name = $"MyApp TCP {port}";

        try
        {
            dynamic rule = policy.Rules.Item(name);

            return (bool)rule.Enabled
                   && (int)rule.Action == 1        // Allow
                   && (int)rule.Direction == 1     // Inbound
                   && (int)rule.Protocol == 6
                   && (string)rule.LocalPorts == port.ToString()
                   && ((int)rule.Profiles & (int)policy.CurrentProfileTypes) != 0;
        }
        catch (FileNotFoundException ex) when ((uint)ex.HResult == 0x80070002)
        {
            return false; // Rule doesn't exist
        }
    }
}