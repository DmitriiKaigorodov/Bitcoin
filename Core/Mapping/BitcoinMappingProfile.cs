using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bitcoin.Core.Models;
using Bitcoin.Core.Models.ApiResources;
using Bitcoin.Core.Models.Rpc.Responses;

namespace Bitcoin.Core.Mapping
{
    public class BitcoinMappingProfile : Profile
    {
        public BitcoinMappingProfile()
        {
            CreateMap<TransactionRpcResponse, Transaction>()
            .ForMember( t => t.TimeReceived,  
                    opt => opt.MapFrom( trr => Utils.DateTimeFromTimeStamp(trr.TimeReceived)));

            CreateMap<TransactionRpcResponseDetails, TransactionDetails>()
            .ForMember( td => td.Category,
                     opt => opt.MapFrom(trr => Enum.Parse<TransactionCategories>(Utils.BeautifyString(trr.Category))));

            CreateMap<GetWalletInfoRpcResponse, Wallet>();
            CreateMap<List<ListSinceBlockTransaction>, List<Transaction>>()
            .AfterMap( (listSinceBlockTransactions, transactions) =>
            {
                foreach(var listSinceBlockTransaction in listSinceBlockTransactions)
                {
                    var transaction = transactions.FirstOrDefault(t => t.TxId == listSinceBlockTransaction.TxId );
                    var category = Utils.BeautifyString(listSinceBlockTransaction.Category);
                    var details = new TransactionDetails()
                    {
                        Account = listSinceBlockTransaction.Account,
                        Amount = listSinceBlockTransaction.Amount,
                        Address = listSinceBlockTransaction.Address,
                        Category = Enum.Parse<TransactionCategories>(category)

                    };
                    if(transaction == null)
                    {
                        transaction = new Transaction()
                        {
                            TxId = listSinceBlockTransaction.TxId,
                            TimeReceived = Utils.DateTimeFromTimeStamp(listSinceBlockTransaction.TimeReceived),
                            Confirmations = listSinceBlockTransaction.Confirmations

                        };

                        transactions.Add(transaction);
                    }
                    transaction.Details.Add(details);                
                }          
            }).ForAllMembers(opt => opt.Ignore());
            
    
            CreateMap<TransactionDetails, LastIncomingModel>()
            .ForMember(lim => lim.Date, opt => opt.MapFrom( td => td.Transaction.TimeReceived) )
            .ForMember(lim => lim.Address, opt => opt.MapFrom( td => td.Address))
            .ForMember(lim => lim.Amount, opt => opt.MapFrom( td => td.Amount))
            .ForMember(lim => lim.Confirmations, opt => opt.MapFrom( td => td.Transaction.Confirmations));
        
        }
    }
}