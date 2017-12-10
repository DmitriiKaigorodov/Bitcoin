using Bitcoin.Core.Models;

namespace Bitcoin.Core.Repositories
{
    public interface IWalletRepository
    {
         void CreateWallet(Wallet wallet);
         Wallet GetWallet();
         void UpdateBalance(double balance);
    }
}