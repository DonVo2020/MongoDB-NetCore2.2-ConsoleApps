using System;

namespace DonVo.MongoDb.Console2018.Restaurant
{
    class Program
    {

        static void Main(string[] args)
        {
            CrudExampleRunner crudExampleRunner = new CrudExampleRunner();
            crudExampleRunner.BulkWrites();
            crudExampleRunner.Updates();
            crudExampleRunner.SearchesAndInsertions();
            //crudExampleRunner.Replacements();
            crudExampleRunner.Deletions();

            Console.WriteLine("\n\n-----------IndexExamplesRunner ------------------");
            IndexExamplesRunner indexExamplesRunner = new IndexExamplesRunner();
            indexExamplesRunner.RunIndexCreationExamples();
            indexExamplesRunner.RunViewIndexingExamples();
            indexExamplesRunner.RunDropIndexExamples();
            indexExamplesRunner.RunViewIndexingExamples();

            Console.WriteLine("\n\n-----------AggregationExamplesRunner ------------------");
            AggregationExamplesRunner aggregationExamplesRunner = new AggregationExamplesRunner();
            aggregationExamplesRunner.RunAggregationWithAppend();
            aggregationExamplesRunner.RunAggregationWithDedicatedMethods();

            Console.WriteLine("\n\nPress Enter key to exit.");
            Console.ReadKey();
        }
    }
}
