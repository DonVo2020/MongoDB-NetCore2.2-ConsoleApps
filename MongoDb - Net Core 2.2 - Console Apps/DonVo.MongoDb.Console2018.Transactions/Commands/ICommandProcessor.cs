using DonVo.MongoDb.Console2018.Transactions.ViewModels;

namespace DonVo.MongoDb.Console2018.Transactions.Commands
{
    public interface ICommandProcessor
    {
        bool CanProcess(ICommand command);
        void Process(ICommand command, Transaction transaction);
        void RollBack(ICommand command, Transaction transaction);
    }
}
