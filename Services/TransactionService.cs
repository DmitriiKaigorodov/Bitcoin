using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bitcoin.Core;
using Bitcoin.Core.Factories;
using Bitcoin.Core.Models;
using Bitcoin.Core.Models.Rpc.Responses;
using Bitcoin.Core.Repositories;
using Bitcoin.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace Bitcoin.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IRpcService rpcService;
        private readonly IMapper mapper;
        public TransactionService(ITransactionRepository transactionRepository,
                                  IRpcService rpcService,IMapper mapper)
        {
            this.mapper = mapper;
            this.rpcService = rpcService;
            this.transactionRepository = transactionRepository;

        }
        public void Add(Transaction transaction)
        {
            transactionRepository.Add(transaction);
        }

        public Transaction FindInDatabase(string txid)
        {
            return transactionRepository.FindByTxId(txid);
        }

        public async Task<IEnumerable<TransactionDetails>> FindLastTransactions()
        {
           return await transactionRepository.FindLastTransactions();                  
        }

        public Task<IEnumerable<TransactionDetails>> GetAllTransactionsDetails()
        {
            return transactionRepository.GetAllTransactionsDetails();
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions(bool includeDetails = false)
        {
            return await transactionRepository.GetAllTransactions();
        }

        public async Task<IEnumerable<Transaction>> GetRecentTransactions(string lastBlockHash)
        {
            string recentBlockHash = await GetRecentBlockHash(lastBlockHash);
            return await GetTransactionsSinceBlock(recentBlockHash);

        }

        public async Task<Transaction> GetTransaction(string txId)
        {
            var rpcRequest = RpcRequestFactory.CreateRpcRequest(1, BitcoinMethods.GetTransaction, txId);
            var rpcResponse = await rpcService.SendRequest<TransactionRpcResponse>(rpcRequest);
            return mapper.Map<TransactionRpcResponse, Transaction>(rpcResponse.Result);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsSinceBlock(string blockHash)
        {
            var rpcRequest = RpcRequestFactory.CreateRpcRequest(1, BitcoinMethods.ListSinceBlock, blockHash);
            var rpcResponse = await rpcService.SendRequest<ListSinceBlockRpcResponse>(rpcRequest);
            return mapper.Map<List<ListSinceBlockTransaction>, List<Transaction>>(rpcResponse.Result.Transactions.ToList());
        }

        private async Task<string> GetRecentBlockHash(string lastBlockHash, int depth = 5)
        {
            string currentBlockHash = lastBlockHash;

            for(int i = 0; i < depth; i++)
            {
                var rpcRequest = RpcRequestFactory.CreateRpcRequest(1, BitcoinMethods.GetBlock, currentBlockHash);
                var rpcResponse = await rpcService.SendRequest<Block>(rpcRequest);
                currentBlockHash = rpcResponse.Result.PreviousBlockHash;
            }

            return currentBlockHash;  

        }

        public void UpdateConfirmations(string txId, int confirmations)
        {
            transactionRepository.UpdateConfirmations(txId, confirmations);
        }
    }
}