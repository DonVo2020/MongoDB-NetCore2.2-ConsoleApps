using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Services
{
    public interface IActivationsService
    {
        Task<Activation> ReadOneAsync(string id);
        Task<IEnumerable<Activation>> ReadAllAsync();
        Task<Activation> CreateOneAsync(Activation activationToCreate);
        Task<Activation> DeleteOneAsync(string id);
    }
}
