using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TheOne.Data.Extensions;

namespace ServerLib
{
    public class CustomValueSource : IFoxAmbientValueSource
    {
        public object GetValue(string key)
        {
            switch(key.ToLower())
            {
                case "host": return Environment.MachineName;
                case "ip": return GetLocalIPAddress();
                case "process": return GetProcessName();
                default:
                    throw new ArgumentException($"Unknown key : {key}");
            }
        }

        private static string GetLocalIPAddress()
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

        private string GetProcessName()
        {
            return System.Diagnostics.Process.GetCurrentProcess().ProcessName;
        }
    }
}
