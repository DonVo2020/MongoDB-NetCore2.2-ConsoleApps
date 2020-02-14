using DonVo.MongoDb.Console2018.Transactions.IoC;
using DonVo.MongoDb.Console2018.Transactions.ViewModels;

using MongoDB.Driver;

using System.Collections.Generic;

namespace DonVo.MongoDb.Console2018.Transactions.Setup
{
    public class TestDbInitializer
    {
        private readonly IMongoCollection<Product> _productsCollection;
        private readonly IMongoCollection<Order> _ordersCollection;
        private readonly IMongoCollection<Transaction> _transactionsCollection;

        public TestDbInitializer()
        {
            _productsCollection = ServicesContainer.GetService<IMongoCollection<Product>>();
            _ordersCollection = ServicesContainer.GetService<IMongoCollection<Order>>();
            _transactionsCollection = ServicesContainer.GetService<IMongoCollection<Transaction>>();
        }

        public void InitTestDb()
        {
            ResetAllCollections();
            InsertDemoProducts();
        }

        private void ResetAllCollections()
        {
            _productsCollection.DeleteMany(FilterDefinition<Product>.Empty);
            _ordersCollection.DeleteMany(FilterDefinition<Order>.Empty);
            _transactionsCollection.DeleteMany(FilterDefinition<Transaction>.Empty);
        }

        void InsertDemoProducts(int numberOfProducts = 5, int initialQuantity = 10)
        {
            var sampleProducts = new List<Product>();

            for (int i = 0; i < numberOfProducts; i++)
            {
                var product = new Product
                {
                    Name = string.Format("Sample Product #{0}", i + 1),
                    Description = "Great product - Good value for money",
                    InStockAmmount = initialQuantity
                };

                sampleProducts.Add(product);
            }

            _productsCollection.InsertMany(sampleProducts);
        }
    }
}
