using System.Threading.Tasks;
using Bitcoin.Core.Models.Rpc;

namespace Bitcoin.Core.Services
{
    public interface IRpcService
    {
        Task<RpcResponse<T>> SendRequest<T>(RpcRequest request);
    }
}