using System.Configuration;

namespace DonVo.MongoDb.Console2018.Restaurant.Infrastructure
{
    public class AppConfigConnectionStringRepository : IConnectionStringRepository
    {
        public string ReadConnectionString(string connectionStringName)
        {
            return ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }
    }
}
