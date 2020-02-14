using DonVo.MongoDb.Console2018.Transactions.ViewModels;

using System;
using System.Collections.Generic;

namespace DonVo.MongoDb.Console2018.Transactions.Commands
{
    public class CreateOrderCommand : ICommand
    {
        public Guid CustomerId { get; set; }
        public List<OrderedProduct> Products { get; set; }
        public Guid TransactionId { get; set; }
    }
}
