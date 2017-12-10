using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Bitcoin.Core;
using Bitcoin.Core.Exceptions;
using Bitcoin.Core.Factories;
using Bitcoin.Core.Models;
using Bitcoin.Core.Models.ApiResources;
using Bitcoin.Core.Models.Rpc;
using Bitcoin.Core.Models.Rpc.Responses;
using Bitcoin.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bitcoin.Services
{
    public class BitcoinService : IBitcoinService
    {
        private readonly IRpcService rpcService;
        private readonly IMapper mapper;

        public BitcoinService(IRpcService rpcService, IMapper mapper)
        {
            this.rpcService = rpcService;
            this.mapper = mapper;
        }


        public async Task<string> SendBitcoins(SendBitcoinModel sendBitcoinModel)
        {
            var rpcRequest = RpcRequestFactory.CreateRpcRequest(1, BitcoinMethods.SendToAddress, sendBitcoinModel.Address, sendBitcoinModel.Amount);
            var rpcResponse = await rpcService.SendRequest<string>(rpcRequest);

            return rpcResponse.Result;
        }

    }
}