using DonVo.MongoDb.Console2018.Transactions.IoC;
using DonVo.MongoDb.Console2018.Transactions.ViewModels;

using MongoDB.Driver;

using System;

namespace DonVo.MongoDb.Console2018.Transactions.Commands
{
    public class CreateOrderCommandProcessor : ICommandProcessor
    {
        private readonly IMongoCollection<Order> _ordersCollection;

        public CreateOrderCommandProcessor()
        {
            _ordersCollection = ServicesContainer.GetService<IMongoCollection<Order>>();
        }

        public bool CanProcess(ICommand command)
        {
            return command is CreateOrderCommand;
        }

        public void Process(ICommand command, Transaction transaction)
        {
            var processedCommand = command as CreateOrderCommand;

            if (processedCommand == null)
                throw new InvalidOperationException("Unsupported or empty command passed to processor");

            var order = new Order(processedCommand.CustomerId, processedCommand.Products);
            order.Transactions.Add(transaction.TransactionId);

            _ordersCollection.InsertOne(order);
        }

        public void RollBack(ICommand command, Transaction transaction)
        {
            var processedCommand = command as CreateOrderCommand;

            if (processedCommand == null)
                throw new InvalidOperationException("Unsupported or empty command passed to processor");

            var insertedOrder = _ordersCollection.Find(order =>
                order.Transactions.Contains(transaction.TransactionId) &&
                order.ProductsAndQuantity == processedCommand.Products &&
                order.CustomerId == processedCommand.CustomerId).FirstOrDefault();

            if (insertedOrder != null)
            {
                _ordersCollection.DeleteOne(order => order._id == insertedOrder._id);
            }
        }
    }
}
