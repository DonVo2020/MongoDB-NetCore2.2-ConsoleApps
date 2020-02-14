using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Services
{
    public class ExercisesService : IExercisesService
    {
        private readonly IActivationsRepository _activationsRepository;
        private readonly IExercisesRepository _exercisesRepository;

        public ExercisesService(IActivationsRepository activationsRepository, IExercisesRepository exercisesRepository)
        {
            _activationsRepository = activationsRepository;
            _exercisesRepository = exercisesRepository;
        }

        #region Find

        public async Task<Exercise> FindExercise(string id)
        {
            var result = await _exercisesRepository.ReadOneAsync(id);
            return result;
        }

        public async Task<IEnumerable<Exercise>> FindExercises()
        {
            var results = await _exercisesRepository.ReadAllAsync();
            return results;
        }

        public async Task<Activation> FindActivation(string exerciseId, string activationId)
        {
            var exercise = await _exercisesRepository.ReadOneAsync(exerciseId);

            if (exercise == null)
                return null;

            return await _activationsRepository.ReadOneAsync(activationId);
        }

        public async Task<IEnumerable<Activation>> FindActivations(string exerciseId)
        {
            var exercise = await _exercisesRepository.ReadOneAsync(exerciseId);

            if (exercise == null)
                return null;

            var results = await _activationsRepository.ReadManyAsync(exerciseId);
            return results;
        }

        #endregion

        #region Create

        public async Task<Exercise> CreateExercise(Exercise exerciseToCreate)
        {
            var createdExercise = await _exercisesRepository.CreateOneAsync(exerciseToCreate);

            return createdExercise;
        }

        public async Task<Activation> CreateActivation(string exerciseId, Activation activationToCreate)
        {
            var exercise = await _exercisesRepository.ReadOneAsync(exerciseId);

            if (exercise == null)
                return null;

            return await _activationsRepository.CreateOneAsync(activationToCreate);
        }

        #endregion

        #region Delete

        public async Task<Exercise> DeleteExercise(string id)
        {
            return await _exercisesRepository.DeleteOneAsync(id);
        }

        public async Task<Activation> DeleteActivation(string exerciseId, string activationId)
        {
            var exercise = await _exercisesRepository.ReadOneAsync(exerciseId);

            if (exercise == null)
                return null;

            var result = await _activationsRepository.DeleteOneAsync(activationId);

            if (result == null)
                return new Activation();

            return result;
        }

        #endregion
    }
}
