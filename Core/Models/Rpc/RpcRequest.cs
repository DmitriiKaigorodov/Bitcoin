using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Bitcoin.Core.Models.Rpc
{
    public class RpcRequest
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("params")]
        public IList<object> Parameters { get; set; }
        public byte[] ToByteArray()
        {
             var json = JsonConvert.SerializeObject(this);
            return Encoding.UTF8.GetBytes(json);
        }
    }
}