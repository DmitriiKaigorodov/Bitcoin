using System;
using Bitcoin.Core.Models.Rpc;

namespace Bitcoin.Core.Exceptions
{
    public class RpcErrorException : Exception
    {
        public RpcError Error {get; set;}

        public RpcErrorException(RpcError error)
        {
            Error = error;
        }
    }
}