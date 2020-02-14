using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Interfaces
{
    public interface IMusclesRepository
    {
        Task<Muscle> ReadOneAsync(string id);
        Task<IEnumerable<Muscle>> ReadAllAsync();
        Task<Muscle> CreateOneAsync(Muscle muscle);
        Task<Muscle> DeleteOneAsync(string id);
    }
}
