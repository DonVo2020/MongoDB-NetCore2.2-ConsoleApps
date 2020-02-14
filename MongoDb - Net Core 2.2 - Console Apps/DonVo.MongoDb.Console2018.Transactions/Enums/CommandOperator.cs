using System;

namespace DonVo.MongoDb.Console2018.Transactions.Enums
{
    [Flags]
    public enum CommandOperator
    {
        Add = 1,
        SetValue = 2,
        Delete = 4,
        CreateNew = 8
    }
}
