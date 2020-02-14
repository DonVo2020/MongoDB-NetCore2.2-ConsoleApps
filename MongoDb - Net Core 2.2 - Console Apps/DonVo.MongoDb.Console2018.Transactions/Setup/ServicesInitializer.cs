using DonVo.MongoDb.Console2018.Transactions.Commands;
using DonVo.MongoDb.Console2018.Transactions.IoC;
using DonVo.MongoDb.Console2018.Transactions.Services;
using DonVo.MongoDb.Console2018.Transactions.ViewModels;
using DonVo.MongoDb.Console2018.Transactions.ViewModels.Mongo;

using Microsoft.Extensions.Configuration;

using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.IO;

namespace DonVo.MongoDb.Console2018.Transactions.Setup
{
    public class ServicesInitializer
    {
        public void InitServices()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true);

                var config = builder.Build();

                var mongodbProvider = new MongoDatabaseProvider(config["ConnectionStrings:DefaultConnection"]);

                ServicesContainer.RegisterService<IMongoCollection<Order>>(mongodbProvider.GetMongoCollection<Order>());
                ServicesContainer.RegisterService(mongodbProvider.GetMongoCollection<Product>());
                ServicesContainer.RegisterService(mongodbProvider.GetMongoCollection<Transaction>());

                var commandsProcessors = new List<ICommandProcessor>();
                commandsProcessors.Add(new CreateOrderCommandProcessor());
                commandsProcessors.Add(new UpdateProductQuantityCommandProcessor());

                ServicesContainer.RegisterService<List<ICommandProcessor>>(commandsProcessors);

                ServicesContainer.RegisterService(new TransactionsService());
                ServicesContainer.RegisterService(new OrdersService());
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
        }
    }
}
