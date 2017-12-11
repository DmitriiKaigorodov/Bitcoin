using System.Threading.Tasks;
using Bitcoin.Core;
using Bitcoin.Core.Exceptions;
using Bitcoin.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bitcoin.Controllers
{
    public class CallbackController : Controller
    {
        private readonly ITransactionService transactionService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IWalletService walletService;

        public CallbackController(ITransactionService transactionService,
                                  IUnitOfWork unitOfWork, IWalletService walletService)
        {
            this.walletService = walletService;
            this.transactionService = transactionService;
            this.unitOfWork = unitOfWork;
            
        }

        [HttpGet]
        [Route("/api/walletnotify/{txid}")]
        public async Task<IActionResult> WalletNotified(string txid)
        {
            try
            {
                var existentTransaction = transactionService.FindInDatabase(txid);
                var transaction = await transactionService.GetTransaction(txid);

                if (existentTransaction == null)
                {
                    transactionService.Add(transaction);
                }
                else
                {
                    transactionService.UpdateConfirmations(transaction.TxId, transaction.Confirmations);
                }

                if(walletService.GetWallet() == null)
                {
                    await walletService.CreateWalletIfNecessary();
                }
                else
                {
                   await walletService.UpdateWalletBalance();
                }

                await unitOfWork.SaveChanges();
                existentTransaction = transactionService.FindInDatabase(txid);
                return Ok(existentTransaction.TxId);
            }
            catch(RpcErrorException ex)
            {
                return BadRequest(ex.Error);
            }


        }


        [HttpGet]
        [Route("/api/blocknotify/{blockHash}")]
        public async Task<IActionResult> BlockNotified(string blockHash)
        {
    
            try
            {
                var updatedTransactions = await transactionService.GetRecentTransactions(blockHash);
                foreach (var updatedTransaction in updatedTransactions)
                {
                    var transaction = transactionService.FindInDatabase(updatedTransaction.TxId);

                    if (transaction == null)
                    {
                        transactionService.Add(updatedTransaction);
                    }
                    else
                    {
                        transactionService.UpdateConfirmations(transaction.TxId, updatedTransaction.Confirmations);
                    }

                }

                await unitOfWork.SaveChanges();
                return Ok();
            }
            catch(RpcErrorException ex)
            {
                return BadRequest(ex.Error);
            }
        }
    }
}