using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Models;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Models.Activations;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bingo.Specification.IntegrationTests.Support
{
    [AllowAnyStatusCode]
    public interface IBingoApi
    {
        #region Activations

        [Get("api/activations")]
        Task<Response<List<Activation>>> GetActivations();

        [Get("api/activations/{id}")]
        Task<Response<Activation>> GetActivationById([Path] string id);

        [Post("api/activations")]
        Task<Response<Activation>> PostActivation([Body] PostActivationDto postDto);

        [Delete("api/activations/{id}")]
        Task<Response<string>> DeleteActivationById([Path] string id);

        #endregion

        #region Exercises

        [Get("api/exercises")]
        Task<Response<List<Exercise>>> GetExercises();

        [Get("api/exercises/{exerciseId}")]
        Task<Response<Exercise>> GetExerciseById([Path] string exerciseId);

        [Get("api/exercises/{exerciseId}/activations")]
        Task<Response<List<Activation>>> GetActivationsForExercise([Path] string exerciseId);

        [Get("api/exercises/{exerciseId}/activations/{activationId}")]
        Task<Response<Activation>> GetActivationForExercise([Path] string exerciseId, [Path] string activationId);

        [Post("api/exercises")]
        Task<Response<Exercise>> PostExercise([Body] PostExerciseDto postDto);

        [Post("api/exercises/{exerciseId}/activations")]
        Task<Response<Activation>> PostActivationToExercise([Path] string exerciseId, [Body] PostActivationDto postDto);

        [Delete("api/exercises/{exerciseId}")]
        Task<Response<string>> DeleteExerciseById([Path] string exerciseId);

        [Delete("api/exercises/{exerciseId}/activations/{activationId}")]
        Task<Response<string>> DeleteActivationFromExercise([Path] string exerciseId, [Path] string activationId);

        #endregion

        #region Muscles Controller

        [Get("api/muscles")]
        Task<Response<List<Muscle>>> GetMuscles();

        [Get("api/muscles/{id}")]
        Task<Response<Muscle>> GetMuscleById([Path] string id);

        [Post("api/muscles")]
        Task<Response<Muscle>> PostMuscle([Body] PostMuscleDto postDto);

        [Delete("api/muscles/{id}")]
        Task<Response<string>> DeleteMuscleById([Path] string id);

        #endregion
    }
}
