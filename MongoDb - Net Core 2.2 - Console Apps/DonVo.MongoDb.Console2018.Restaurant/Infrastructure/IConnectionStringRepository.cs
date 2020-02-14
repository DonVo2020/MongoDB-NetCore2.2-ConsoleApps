namespace DonVo.MongoDb.Console2018.Restaurant.Infrastructure
{
    public interface IConnectionStringRepository
    {
        string ReadConnectionString(string connectionStringName);
    }
}
