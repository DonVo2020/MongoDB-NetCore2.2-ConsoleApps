using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Interfaces
{
    public interface IExercisesRepository
    {
        Task<Exercise> ReadOneAsync(string id);
        Task<IEnumerable<Exercise>> ReadAllAsync();
        Task<Exercise> CreateOneAsync(Exercise exercise);
        Task<Exercise> DeleteOneAsync(string id);
    }
}
