using Bitcoin.Core.Models.Rpc;

namespace Bitcoin.Core.Factories
{
    public static class RpcRequestFactory
    {
        public static RpcRequest CreateRpcRequest(int id, BitcoinMethods method, params object[] parameters)
        {
            var rpcRequest = new RpcRequest()
            {
                Id = id,
                Method = method.ToString().ToLowerInvariant(),
                Parameters = parameters
            };

            return rpcRequest;
        }
    }
}