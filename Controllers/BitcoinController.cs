using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bitcoin.Core;
using Bitcoin.Core.Exceptions;
using Bitcoin.Core.Extentions;
using Bitcoin.Core.Models;
using Bitcoin.Core.Models.ApiResources;
using Bitcoin.Core.Models.Rpc.Responses;
using Bitcoin.Core.Repositories;
using Bitcoin.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bitcoin.Controllers
{
    
    public class BitcoinController : Controller
    {
        private readonly ITransactionService transactionService;
        private readonly IBitcoinService bitcoinService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public BitcoinController(ITransactionService transactionService,
                                 IBitcoinService bitcoinService,
                                 IWalletService walletService,
                                 IUnitOfWork unitOfWork,
                                 IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.bitcoinService = bitcoinService;
            this.transactionService = transactionService;
        }


        [HttpPost]
        [Route("/api/sendbtc")]
        public async Task<IActionResult> SendBtc([FromBody] SendBitcoinModel sendBitcoinModel)
        {
            try
            {
                 var txId =  await bitcoinService.SendBitcoins(sendBitcoinModel);
                 return  Ok(txId);
            }
            catch(UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch(RpcErrorException ex)
            {
                return BadRequest(ex.Error);
            }
        }


    }
}