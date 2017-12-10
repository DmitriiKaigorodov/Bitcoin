using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bitcoin.Core;
using Bitcoin.Core.Models;
using Bitcoin.Core.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Bitcoin.Persistent.Repositories
{

    public class TransactionRepository : ITransactionRepository
    {
        private readonly BitcoinDbContext dbContext;
        private static readonly int MinConfirmationsForLastTransaction = 3;
        public TransactionRepository(BitcoinDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
        public void Add(Transaction transaction)
        {
            dbContext.Transactions.Add(transaction);
        }

        public  Transaction FindByTxId(string txid)
        {
            return  dbContext.Transactions.Include( t=> t.Details).FirstOrDefault( t => t.TxId == txid);
        }

        public async Task<IEnumerable<TransactionDetails>> FindLastTransactions()
        {
             var transactionDetails =  dbContext.TransactionDetails.Include(tr => tr.Transaction).AsQueryable();
       
            return await transactionDetails.Where( td => td.Transaction.Confirmations < MinConfirmationsForLastTransaction
                    && !td.Transaction.WasRequested && td.Category == TransactionCategories.Receive).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions(bool includeDetails = false)
        {
            var transactions = dbContext.Transactions.AsQueryable();

            if(includeDetails)
            {
                transactions = transactions.Include( t => t.Details);
            }

            return await transactions.ToListAsync();
        }

        public async Task<IEnumerable<TransactionDetails>> GetAllTransactionsDetails()
        {
            return await dbContext.TransactionDetails.Include(tr => tr.Transaction).ToListAsync();
        }

        public void UpdateConfirmations(string txId, int confirmations)
        {
            var transaction = FindByTxId(txId);
            transaction.Confirmations = confirmations;
            dbContext.Transactions.Update(transaction);
        }
    }
}