using DonVo.MongoDb.Console2018.Transactions.ViewModels.Mongo;

using System;
using System.Collections.Generic;

namespace DonVo.MongoDb.Console2018.Transactions.ViewModels
{
    public class Product : Entity
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int InStockAmmount { get; set; }
        public List<Guid> Transactions { get; set; }

        public Product()
        {
            ProductId = Guid.NewGuid();
            Transactions = new List<Guid>();
        }
    }
}
