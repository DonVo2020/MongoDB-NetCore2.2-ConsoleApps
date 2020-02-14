using DonVo.MongoDb.Console2018.Transactions.Enums;
using DonVo.MongoDb.Console2018.Transactions.ViewModels.Mongo;

using System;
using System.Collections.Generic;

namespace DonVo.MongoDb.Console2018.Transactions.ViewModels
{
    public class Order : Entity
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; set; }
        public List<OrderedProduct> ProductsAndQuantity { get; set; }
        public List<Guid> Transactions { get; set; }
        public OrderStatus Status { get; set; }

        public Order()
        {
            OrderId = Guid.NewGuid();
            Status = OrderStatus.Pending;
            Transactions = new List<Guid>();
        }
        public Order(Guid customerId, List<OrderedProduct> products) : this()
        {
            CustomerId = customerId;
            ProductsAndQuantity = products;
        }
    }
}
