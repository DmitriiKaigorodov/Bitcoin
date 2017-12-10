using System.Collections.Generic;
using System.Threading.Tasks;
using Bitcoin.Core.Models;


namespace Bitcoin.Core.Services
{
    public interface ITransactionService
    {
         Task<Transaction> GetTransaction(string txid);
         Task<IEnumerable<Transaction>> GetRecentTransactions(string lastBlockHash);
         Task<IEnumerable<Transaction>> GetTransactionsSinceBlock(string blockHash);
         void Add(Transaction transaction);
         Transaction FindInDatabase(string txid);
         Task<IEnumerable<TransactionDetails>> FindLastTransactions();
         Task<IEnumerable<Transaction>> GetAllTransactions(bool includeDetails = false);
         void UpdateConfirmations(string txId, int confirmations);
         Task<IEnumerable<TransactionDetails>> GetAllTransactionsDetails();
    }
}