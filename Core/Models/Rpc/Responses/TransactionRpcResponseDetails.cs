using Newtonsoft.Json;

namespace Bitcoin.Core.Models.Rpc.Responses
{
    public class TransactionRpcResponseDetails
    {
        [JsonProperty("account")]
        public string Account { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("amount")]
        public double Amount { get; set; }
        [JsonProperty("label")]
        public string Label { get; set; }
        [JsonProperty("vout")]
        public int Vout { get; set; }
    }
}