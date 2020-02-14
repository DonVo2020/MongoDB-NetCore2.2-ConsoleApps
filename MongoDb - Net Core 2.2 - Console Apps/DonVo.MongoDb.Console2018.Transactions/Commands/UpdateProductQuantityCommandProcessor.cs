using DonVo.MongoDb.Console2018.Transactions.Enums;
using DonVo.MongoDb.Console2018.Transactions.IoC;
using DonVo.MongoDb.Console2018.Transactions.ViewModels;

using MongoDB.Bson;
using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Linq;

namespace DonVo.MongoDb.Console2018.Transactions.Commands
{
    public class UpdateProductQuantityCommandProcessor : ICommandProcessor
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public UpdateProductQuantityCommandProcessor()
        {
            _productsCollection = ServicesContainer.GetService<IMongoCollection<Product>>();
        }

        public bool CanProcess(ICommand command)
        {
            return command is UpdateProductQuantityCommand;
        }

        public void Process(ICommand command, Transaction transaction)
        {
            var processedCommand = ValidateCommand(command);

            var updateDefinitions = new BsonDocument(new Dictionary<string, object>{
                { "$inc", new BsonDocument("InStockAmmount", processedCommand.Value)},
                { "$push", new BsonDocument("Transactions", transaction.TransactionId)}
            });

            var updatedProduct = _productsCollection.FindOneAndUpdate(
                o => o.ProductId == processedCommand.Product.ProductId,
                updateDefinitions);
        }

        public void RollBack(ICommand command, Transaction transaction)
        {
            var processedCommand = ValidateCommand(command);

            var filterObject = new BsonDocument("ProductId", processedCommand.Product.ProductId);

            var savedProduct = _productsCollection.Find(filterObject).FirstOrDefault();

            // If the product is not found in the collection
            if (savedProduct == null || savedProduct.Transactions == null || !savedProduct.Transactions.Any())
                return;

            // If the product wasn't affected by the current transaction
            if (!savedProduct.Transactions.Contains(transaction.TransactionId))
                return;

            var updateDefinitions = new BsonDocument(new Dictionary<string, object>{
                { "$inc", new BsonDocument("InStockAmmount", - processedCommand.Value)},
                { "$pop", new BsonDocument("Transactions", transaction.TransactionId)}
            });

            var updatedProduct = _productsCollection.FindOneAndUpdate(filterObject, updateDefinitions);
        }

        private UpdateProductQuantityCommand ValidateCommand(ICommand command)
        {
            var validatedCommand = command as UpdateProductQuantityCommand;

            // Only update operations are allowed, since "replace" operations can't be rolled back securely
            if (validatedCommand == null || validatedCommand.Operator != CommandOperator.Add)
                throw new InvalidOperationException("Unsupported command or command-operator passed to processor");

            return validatedCommand;
        }
    }
}
