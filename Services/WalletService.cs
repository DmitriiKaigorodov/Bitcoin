using System.Threading.Tasks;
using AutoMapper;
using Bitcoin.Core;
using Bitcoin.Core.Factories;
using Bitcoin.Core.Models;
using Bitcoin.Core.Models.Rpc.Responses;
using Bitcoin.Core.Repositories;
using Bitcoin.Core.Services;

namespace Bitcoin.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository walletRepository;
        private readonly IRpcService rpcService;
        private readonly IMapper mapper;

        public  WalletService(IWalletRepository walletRepository,
                             IRpcService rpcService,
                             IMapper mapper)
        {
            this.mapper = mapper;
            this.rpcService = rpcService;
            this.walletRepository = walletRepository;      
        }


        public async Task CreateWalletIfNecessary()
        {
            var wallet = await GetWalletFromBitcoinDaemon();
            walletRepository.CreateWallet(wallet);
        }

        public Wallet GetWallet()
        {
            return walletRepository.GetWallet();
        }

        public async Task UpdateWalletBalance()
        {
            var wallet = await GetWalletFromBitcoinDaemon();
            walletRepository.UpdateBalance(wallet.Balance);
        }

        private async Task<Wallet> GetWalletFromBitcoinDaemon()
        {
            var rpcRequest = RpcRequestFactory.CreateRpcRequest(1, BitcoinMethods.GetWalletInfo);
            var rpcResponse = await rpcService.SendRequest<GetWalletInfoRpcResponse>(rpcRequest);

            return mapper.Map<GetWalletInfoRpcResponse, Wallet>(rpcResponse.Result);
        }
    }
}