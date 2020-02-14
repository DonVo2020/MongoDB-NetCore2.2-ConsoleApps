using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Services
{
    public interface IExercisesService
    {
        Task<Exercise> FindExercise(string id);
        Task<IEnumerable<Exercise>> FindExercises();
        Task<Exercise> CreateExercise(Exercise exerciseToCreate);
        Task<Exercise> DeleteExercise(string id);

        Task<Activation> FindActivation(string exerciseId, string activationId);
        Task<IEnumerable<Activation>> FindActivations(string exerciseId);
        Task<Activation> CreateActivation(string exerciseId, Activation activationToCreate);
        Task<Activation> DeleteActivation(string exerciseId, string activationId);
    }
}
