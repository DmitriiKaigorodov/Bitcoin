using Newtonsoft.Json;

namespace Bitcoin.Core.Models.Rpc.Responses
{

    public class ListSinceBlockTransaction
    {
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("amount")]
        public double Amount { get; set; }
        [JsonProperty("vout")]
        public int Vout { get; set; }
        [JsonProperty("fee")]
        public double Fee { get; set; }
        [JsonProperty("confirmations")]
        public int Confirmations { get; set; }
        [JsonProperty("blockhash")]
        public string BlockHash { get; set; }
        [JsonProperty("blockindex")]
        public int BlockIndex { get; set; }
        [JsonProperty("blocktime")]
        public int BlockTime { get; set; }
        [JsonProperty("txid")]
        public string TxId { get; set; }
        [JsonProperty("time")]
        public int Time { get; set; }
        [JsonProperty("timereceived")]
        public int TimeReceived { get; set; }
        [JsonProperty("bip125-replaceable")]
        public string Bip125Replaceable { get; set; }
        [JsonProperty("abandoned")]
        public bool Abandoned { get; set; }
        [JsonProperty("comment")]
        public string Comment { get; set; }
        [JsonProperty("label")]
        public string Label { get; set; }
        [JsonProperty("to")]
        public string To { get; set; }
        [JsonProperty("account")]
        public string Account { get; set; }
    }
}