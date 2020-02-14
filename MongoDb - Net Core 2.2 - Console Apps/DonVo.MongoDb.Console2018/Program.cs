using DonVo.MongoDb.Console2018.Demo01;
using DonVo.MongoDb.Console2018.Demo02;
using DonVo.MongoDb.Console2018.Demo03;
using System;
using System.Threading;

namespace DonVo.MongoDb.Console2018
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Demo01
            //UsersRepository mongoDbRepo = new UsersRepository("mongodb://localhost:27017"); // GOOD
            //UsersRepository mongoDbRepoFailed = new UsersRepository("mongodb://localhost:27016"); // FAIL

            //DemoUser demo01 = new DemoUser(mongoDbRepo);

            //Console.WriteLine("{0} Connection is {1}", "mongodb://localhost:27017", demo01.IsConnectionEstablished(mongoDbRepo) ? "Established" : "Not Established");
            //Console.WriteLine("\n");
            ////Console.WriteLine("{0} Connection is {1}", "mongodb://localhost:27016", demo01.IsConnectionEstablished(mongoDbRepoFailed) ? "Established" : "Not Established");

            //demo01.DeleteDb("blog");

            //bool isSuccess = demo01.Initialize().IsCompletedSuccessfully;
            //Thread.Sleep(200);
            //isSuccess = demo01.GetAllUsers().IsCompletedSuccessfully;
            //Thread.Sleep(200);
            //isSuccess = demo01.GetUserByField("name", "Jenifer Hook").IsCompletedSuccessfully;
            //Thread.Sleep(200);
            //isSuccess = demo01.GetUserCount(10).IsCompletedSuccessfully;
            //Thread.Sleep(200);
            //isSuccess = demo01.InsertUser().IsCompletedSuccessfully;
            //Thread.Sleep(200);
            //isSuccess = demo01.GetAllUsers().IsCompletedSuccessfully;
            //Thread.Sleep(200);
            //isSuccess = demo01.UpdateFirstUserByField("name", "Jenifer Hook").IsCompletedSuccessfully;
            //Thread.Sleep(200);
            //isSuccess = demo01.UpdateUserExtendingWithNewField().IsCompletedSuccessfully;
            //Thread.Sleep(200);
            //isSuccess = demo01.DeleteAllUsers().IsCompletedSuccessfully;
            //Thread.Sleep(200);
            //isSuccess = demo01.GetUserCount(10).IsCompletedSuccessfully;
            //Thread.Sleep(200);

            //demo01.DeleteDb("blog");
            #endregion

            #region Demo02
            //BsonDemo bsonDemo = new BsonDemo();
            //bsonDemo.DeleteDatabase();
            //bsonDemo.InsertOne();
            //bsonDemo.Query();
            #endregion

            #region Demo03
            //MappingDemo.DeleteDatabase();
            //MappingDemo.InsertOne();
            //Thread.Sleep(100);
            //MappingDemo.UpdateOne();
            //Thread.Sleep(100);
            //MappingDemo.QueryAndAggregate();
            #endregion

            Console.WriteLine("End of Program.  Press enter key to exit.");
            Console.ReadKey();
        }
    }
}
