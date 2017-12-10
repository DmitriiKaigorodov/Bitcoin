using System;

namespace Bitcoin.Core.Models.Rpc
{
    public class RpcError 
    {    
         public int Code { get; set; }
        public string Message { get; set; }
    }
}