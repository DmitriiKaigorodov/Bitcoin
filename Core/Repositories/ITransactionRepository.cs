using System.Collections.Generic;
using System.Threading.Tasks;
using Bitcoin.Core.Models;

namespace Bitcoin.Core.Repositories
{
    public interface ITransactionRepository
    {
         void Add(Transaction transaction);
         Transaction FindByTxId(string txid);
         Task<IEnumerable<TransactionDetails>> FindLastTransactions();
         Task<IEnumerable<Transaction>> GetAllTransactions(bool includeDetails = false);
         void UpdateConfirmations(string txId, int confirmations);
        Task<IEnumerable<TransactionDetails>> GetAllTransactionsDetails();
    }
}