using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Interfaces
{
    public interface IActivationsRepository
    {
        Task<Activation> ReadOneAsync(string id);
        Task<IEnumerable<Activation>> ReadManyAsync(string exerciseId);
        Task<IEnumerable<Activation>> ReadAllAsync();
        Task<Activation> CreateOneAsync(Activation activation);
        Task<Activation> DeleteOneAsync(string id);
    }
}
