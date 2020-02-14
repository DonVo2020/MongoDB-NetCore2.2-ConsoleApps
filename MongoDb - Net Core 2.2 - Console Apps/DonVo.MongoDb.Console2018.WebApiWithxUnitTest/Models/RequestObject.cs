namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Models
{
    public abstract class RequestObject
    {
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
