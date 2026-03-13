using System.Net;
using NeoDEEX.Data;

namespace ServerLib;

public class CustomValueSource : IFoxAmbientValueSource
{
    public object? GetValue(string key)
    {
        return key.ToLower() switch
        {
            "host" => Environment.MachineName,
            "ip" => GetLocalIPAddress(),
            "process" => GetProcessName(),
            _ => throw new ArgumentException($"Unknown key : {key}"),
        };
    }

    private static string? GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(System.Net.Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        return null;
    }

    private static string? GetProcessName()
    {
        return System.Diagnostics.Process.GetCurrentProcess().ProcessName;
    }
}
