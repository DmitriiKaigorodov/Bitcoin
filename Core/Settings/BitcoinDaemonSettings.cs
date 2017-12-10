using System;
using System.Text;

namespace Bitcoin.Core.Settings
{
    public class BitcoinDaemonSettings
    {
        public string ServerUrl { get; set; }
        public string RpcUsername { get; set; }
        public string RpcPassword { get; set; }

        public string ToBase64()
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(RpcUsername+ ":" + RpcPassword));
        }
    }
}