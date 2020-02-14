using DonVo.MongoDb.Console2018.Transactions.Enums;
using DonVo.MongoDb.Console2018.Transactions.ViewModels;

namespace DonVo.MongoDb.Console2018.Transactions.Commands
{
    public class UpdateProductQuantityCommand : ICommand
    {
        public Product Product { get; set; }
        public CommandOperator Operator { get; set; }
        public int Value { get; set; }
    }
}
