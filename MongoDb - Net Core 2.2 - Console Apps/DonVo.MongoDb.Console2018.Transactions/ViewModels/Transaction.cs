using DonVo.MongoDb.Console2018.Transactions.Commands;
using DonVo.MongoDb.Console2018.Transactions.Enums;
using DonVo.MongoDb.Console2018.Transactions.ViewModels.Mongo;

using System;
using System.Collections.Generic;

namespace DonVo.MongoDb.Console2018.Transactions.ViewModels
{
    public class Transaction : Entity
    {
        public Guid TransactionId { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public TransactionStatus Status { get; set; }
        public List<ICommand> Commands { get; set; }

        public Transaction()
        {
            TransactionId = Guid.NewGuid();
            TimeStamp = DateTime.Now;
            Status = TransactionStatus.Pending;
        }
    }
}
