using Newtonsoft.Json;

namespace Bitcoin.Core.Models.Rpc.Responses
{
    public class GetWalletInfoRpcResponse
    {
        [JsonProperty("balance")]
        public double Balance { get; set; }
        [JsonProperty("walletname")]
        public string Name { get; set; }
        [JsonProperty("walletversion")]
        public string Version { get; set; }
    }
}