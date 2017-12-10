using Bitcoin.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Bitcoin.Persistent
{
    public class BitcoinDbContext : DbContext
    {
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionDetails> TransactionDetails { get; set; }
        public BitcoinDbContext(DbContextOptions<BitcoinDbContext> options) : base(options)
        {
            
        }       
    }
}