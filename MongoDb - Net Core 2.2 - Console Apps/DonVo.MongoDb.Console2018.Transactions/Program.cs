using DonVo.MongoDb.Console2018.Transactions.IoC;
using DonVo.MongoDb.Console2018.Transactions.Services;
using DonVo.MongoDb.Console2018.Transactions.Setup;
using DonVo.MongoDb.Console2018.Transactions.ViewModels;

using MongoDB.Driver;

using System;
using System.Collections.Generic;

namespace DonVo.MongoDb.Console2018.Transactions
{
    class Program
    {
        static void Main(string[] args)
        {
            new ServicesInitializer().InitServices();

            new TestDbInitializer().InitTestDb();

            PerformDemoOrder();

            GetAllOrders();
            GetAllProducts();

            Console.WriteLine("Press enter key to exit.");
            Console.ReadKey();
        }

        static void PerformDemoOrder()
        {
            var productsCollection = ServicesContainer.GetService<IMongoCollection<Product>>();
            var ordersService = ServicesContainer.GetService<OrdersService>();

            var productsForOrder = productsCollection.Find(p => p.InStockAmmount > 0).ToList();

            var orderProducts = new List<OrderedProduct>();

            foreach (var product in productsForOrder)
            {
                orderProducts.Add(new OrderedProduct(product, 1));
            }

            ordersService.CreateOrder(Guid.NewGuid(), orderProducts);

            Console.WriteLine("Order and Products have beeen created.\n");
        }

        static void GetAllOrders()
        {
            var ordersCollection = ServicesContainer.GetService<IMongoCollection<Order>>();
            Console.WriteLine("----- Get All Orders -----");
            var orders = ordersCollection.Find(p => p.ProductsAndQuantity.Count > 0).ToList();
            foreach (var order in orders)
            {
                Console.WriteLine("OrderId: {0} | CustomerId: {1} | ProductsAndQuantity: {2} | Status: {3}", order.OrderId, order.CustomerId, order.ProductsAndQuantity.Count, order.Status);
            }
            Console.WriteLine("\n");
        }

        static void GetAllProducts()
        {
            var productsCollection = ServicesContainer.GetService<IMongoCollection<Product>>();
            Console.WriteLine("----- Get All Products -----");
            var productsForOrder = productsCollection.Find(p => p.InStockAmmount > 0).ToList();
            foreach (var product in productsForOrder)
            {
                Console.WriteLine("Name: {0} | Description: {1} | InStockAmmount: {2} | ProductId: {3}", product.Name, product.Description, product.InStockAmmount, product.ProductId);
            }
            Console.WriteLine("\n");
        }
    }
}
