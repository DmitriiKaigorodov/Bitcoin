using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bitcoin.Core.Models.Rpc.Responses
{

    public class ListSinceBlockRpcResponse
    {
        [JsonProperty("transactions")]
        public IList<ListSinceBlockTransaction> Transactions { get; set; }
        [JsonProperty("removed")]
        public IList<ListSinceBlockTransaction> Removed { get; set; }
        [JsonProperty("lastblock")]
        public string LastBlock { get; set; }
    }
}