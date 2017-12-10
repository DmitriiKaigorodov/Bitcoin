using System.Collections.Generic;
using System.Threading.Tasks;
using Bitcoin.Core.Models;
using Bitcoin.Core.Models.ApiResources;
using Bitcoin.Core.Models.Rpc.Responses;

namespace Bitcoin.Core.Services
{
    public interface IBitcoinService
    {
        Task<string> SendBitcoins(SendBitcoinModel sendBitcoinModel);
    }
}