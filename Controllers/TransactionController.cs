using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bitcoin.Core;
using Bitcoin.Core.Extentions;
using Bitcoin.Core.Models;
using Bitcoin.Core.Models.ApiResources;
using Bitcoin.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bitcoin.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService transactionService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public TransactionController(ITransactionService transactionService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.transactionService = transactionService;

        }
        [HttpGet]
        [Route("/api/getlast")]
        public async Task<IActionResult> GetLastTransactions()
        {
            var transactionsDetails = await transactionService.FindLastTransactions();
            var result = mapper.Map<IEnumerable<TransactionDetails>, IEnumerable<LastIncomingModel>>(transactionsDetails);
            transactionsDetails.Select(td => td.Transaction).MarkAsRequested();
            await unitOfWork.SaveChanges();

            return Ok(result);
        }
    }
}