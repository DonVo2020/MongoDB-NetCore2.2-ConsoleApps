using ConfigurationSection = DonVo.MongoDb.Console2018.ForGridFS.Configuration.ConfigurationSection;

using Microsoft.Extensions.Configuration;

using System.Collections.Generic;
using System.Linq;

namespace DonVo.MongoDb.Console2018.ForGridFS.Mongo
{
    public static class Extensions
    {
        internal static Dictionary<string, ConfigurationSection> TypesConfiguration { get; private set; } = new Dictionary<string, ConfigurationSection>();

        public static IConfiguration AddMongolino(this IConfiguration configuration)
        {
            var section = configuration.GetSection("DonVo.MongoDb.Console2018.ForGridFS");

            if (section != null && section.Exists())
            {
                TypesConfiguration = section.GetChildren()
                           .Select(sub => new
                           {
                               Type = sub.Key,
                               Section = sub.Get<ConfigurationSection>()
                           })
                          .ToDictionary(x => x.Type, x => x.Section);
            }

            return configuration;
        }
    }
}
