using System.Linq;
using Bitcoin.Core.Models;
using Bitcoin.Core.Repositories;

namespace Bitcoin.Persistent.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly BitcoinDbContext dbContext;

        public WalletRepository(BitcoinDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void CreateWallet(Wallet wallet)
        {
            dbContext.Wallets.Add(wallet);
        }

        public Wallet GetWallet()
        {
            return dbContext.Wallets.FirstOrDefault();
        }

        public void UpdateBalance(double balance)
        {
            var wallet = GetWallet();
            wallet.Balance = balance;
        }
    }
}