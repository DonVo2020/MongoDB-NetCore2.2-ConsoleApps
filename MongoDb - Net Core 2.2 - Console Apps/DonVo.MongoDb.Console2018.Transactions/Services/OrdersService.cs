using DonVo.MongoDb.Console2018.Transactions.Commands;
using DonVo.MongoDb.Console2018.Transactions.Enums;
using DonVo.MongoDb.Console2018.Transactions.IoC;
using DonVo.MongoDb.Console2018.Transactions.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DonVo.MongoDb.Console2018.Transactions.Services
{
    public class OrdersService
    {
        private readonly TransactionsService _transactionsService;

        public OrdersService()
        {
            _transactionsService = ServicesContainer.GetService<TransactionsService>();
        }

        public void CreateOrder(Guid customerId, List<OrderedProduct> productsAndAmounts)
        {
            List<ICommand> createOrderTransactionCommands = new List<ICommand>
            {
                new CreateOrderCommand
                {
                    CustomerId = customerId,
                    Products = productsAndAmounts
                }
            };

            createOrderTransactionCommands.AddRange(
                productsAndAmounts.Select(t => new UpdateProductQuantityCommand
                {
                    Product = t.Product,
                    Operator = CommandOperator.Add,
                    Value = -t.Quantity
                }));

            var createOrderTransaction = _transactionsService.InitTransaction(createOrderTransactionCommands);

            try
            {
                _transactionsService.CommitTransaction(createOrderTransaction);

                //_transactionsService.RollBackTransaction(createOrderTransaction);
            }
            catch
            {
                _transactionsService.RollBackTransaction(createOrderTransaction);
            }
        }
    }
}
