using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Helpers;
using Newtonsoft.Json;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities
{
    public class BingoEntity
    {
        public override bool Equals(object obj)
        {
            return this.DeepEquals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
