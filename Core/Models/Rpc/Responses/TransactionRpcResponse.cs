using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bitcoin.Core.Models.Rpc.Responses
{

    public class TransactionRpcResponse
    {
        [JsonProperty("amount")]
        public double Amount { get; set; }
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
        [JsonProperty("walletconflicts")]
        public List<object> WalletConflicts { get; set; }
        [JsonProperty("time")]
        public int Time { get; set; }
        [JsonProperty("timereceived")]
        public int TimeReceived { get; set; }
        [JsonProperty("bip125-replaceable")]
        public string Bip125Replaceable { get; set; }
        [JsonProperty("details")]
        public List<TransactionRpcResponseDetails> Details { get; set; }
        [JsonProperty("hex")]
        public string Hex { get; set; }
    }
}