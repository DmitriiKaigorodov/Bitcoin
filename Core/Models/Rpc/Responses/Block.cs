using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bitcoin.Core.Models.Rpc.Responses
{
    public class Block
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }
        [JsonProperty("confirmations")]
        public int Confirmations { get; set; }
        [JsonProperty("strippedsize")]
        public long StrippedSize { get; set; }
        [JsonProperty("size")]
        public long Size { get; set; }
        [JsonProperty("weight")]
        public int Weight { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("version")]
        public int Version { get; set; }
        [JsonProperty("versionhex")]
        public string VersionHex { get; set; }
        [JsonProperty("merkleroot")]
        public string Merkleroot { get; set; }
        [JsonProperty("tx")]
        public List<string> Transactions { get; set; }
        [JsonProperty("time")]
        public int Time { get; set; }
        [JsonProperty("mediantime")]
        public int MedianTime { get; set; }
        [JsonProperty("nonce")]
        public long Nonce { get; set; }
        [JsonProperty("bits")]
        public string Bits { get; set; }
        [JsonProperty("difficulty")]
        public double Difficulty { get; set; }
        [JsonProperty("chainwork")]
        public string Chainwork { get; set; }
        [JsonProperty("previousblockhash")]
        public string PreviousBlockHash { get; set; }
        [JsonProperty("nextblockhash")]
        public string NextBlockHash { get; set; }      
    }
}