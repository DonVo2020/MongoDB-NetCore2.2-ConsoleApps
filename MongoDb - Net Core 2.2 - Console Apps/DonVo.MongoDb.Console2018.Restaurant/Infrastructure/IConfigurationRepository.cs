namespace DonVo.MongoDb.Console2018.Restaurant.Infrastructure
{
    public interface IConfigurationRepository
    {
        T GetConfigurationValue<T>(string key);
        T GetConfigurationValue<T>(string key, T defaultValue);
    }
}
