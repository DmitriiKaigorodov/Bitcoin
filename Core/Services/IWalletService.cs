using System.Threading.Tasks;
using Bitcoin.Core.Models;

namespace Bitcoin.Core.Services
{
    public interface IWalletService
    {
        Task CreateWalletIfNecessary();
        Task UpdateWalletBalance();
        Wallet GetWallet();
    }
}