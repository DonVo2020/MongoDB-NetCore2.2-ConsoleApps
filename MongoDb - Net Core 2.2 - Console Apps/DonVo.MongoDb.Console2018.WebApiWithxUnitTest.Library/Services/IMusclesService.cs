using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Services
{
    public interface IMusclesService
    {
        Task<Muscle> ReadOneAsync(string id);
        Task<IEnumerable<Muscle>> ReadAllAsync();
        Task<Muscle> CreateOneAsync(Muscle muscleToCreate);
        Task<Muscle> DeleteOneAsync(string id);
    }
}
