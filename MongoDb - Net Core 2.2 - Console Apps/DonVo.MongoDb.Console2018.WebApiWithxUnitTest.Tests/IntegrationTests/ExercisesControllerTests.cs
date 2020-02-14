namespace Bingo.Specification.IntegrationTests
{
    //    [Trait("Integration", nameof(ExercisesControllerTest))]
    //    public class ExercisesControllerTest : TestBase
    //    {
    //        private readonly ServiceFixture _service;
    //
    //        public ExercisesControllerTest(ServiceFixture service)
    //        {
    //            _service = service;
    //        }
    //        
    //        [Fact]
    //        public async void GetExercises_WhenDataExists_ReturnsListContainingExpectedExercise200()
    //        {
    //            var expectedExercises = Exercises.ContractExercises;
    //
    //            var response = await _service.Api.GetExercises();
    //            var actualExercises = response.GetContent();
    //
    //            this.ShouldSatisfyAllConditions(
    //                    () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.OK),
    //                    () => expectedExercises.ForEach(exercise =>
    //                            actualExercises.ShouldContain(exercise))
    //                );
    //        }
    //
    //        [Fact]
    //        public async void GetExercise_ByExerciseId_ReturnsExpectedExercise200()
    //        {
    //            var expectedExercise = Exercises.ContractExercise;
    //
    //            var response = await _service.Api.GetExerciseById(expectedExercise.Id);
    //            var actualExercise = response.GetContent();
    //
    //            this.ShouldSatisfyAllConditions(
    //                    () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.OK),
    //                    () => actualExercise.ShouldBe(expectedExercise)
    //                );
    //        }
    //
    //        [Fact]
    //        public async void GetActivationsForExercise_ByExerciseId_ReturnsExpectedActivations200()
    //        {
    //            var expectedExercise = Exercises.ContractExercise;
    //
    //            var response = await _service.Api.GetActivationsForExercise(expectedExercise.Id);
    //            var actualExercises = response.GetContent();
    //
    //            this.ShouldSatisfyAllConditions(
    //                    () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.OK),
    //                    () => actualExercises.TrueForAll(x => x.ExerciseId == expectedExercise.Id)
    //                );
    //        }
    //
    //        [Fact]
    //        public async void GetActivationForExercise_ByExerciseId_ReturnsExpectedActivation200()
    //        {
    //            var expectedExercise = Exercises.ContractExercise;
    //            var expectedActivation = Activations.ContractActivation;
    //
    //            var response = await _service.Api.GetActivationForExercise(expectedExercise.Id, expectedActivation.Id);
    //            var actualActivation = response.GetContent();
    //
    //            this.ShouldSatisfyAllConditions(
    //                    () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.OK),
    //                    () => actualActivation.ShouldBe(expectedActivation)
    //                );
    //        }
    //
    //        [Fact]
    //        public async void PostExercise_ByValidDto_ReturnsPostedExercise201()
    //        {
    //            var postDto = Exercises.ContractExercisePostDto;
    //
    //            var response = await _service.Api.PostExercise(postDto);
    //            var postedExercise = response.GetContent();
    //
    //            this.ShouldSatisfyAllConditions(
    //                    () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.Created),
    //                    () => postedExercise.Id.ShouldNotBeNull(),
    //                    () => postedExercise.ShouldBe(postDto.ToExercise()),
    //                    () => _service.ExercisesCollection.ShouldContain(postedExercise)
    //                );
    //        }
    //
    //        [Fact]
    //        public async void PostActivationToExercise_WithValidRqo_ReturnsPostedExercise201()
    //        {
    //            var expectedExercise = Exercises.ContractExercise;
    //            var postDto = Activations.ContractActivationPostDto;
    //
    //            var response = await _service.Api.PostActivationToExercise(expectedExercise.Id, postDto);
    //            var postedActivation = response.GetContent();
    //
    //            this.ShouldSatisfyAllConditions(
    //                    () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.Created),
    //                    () => postedActivation.ShouldBe(postDto.ToActivation()),
    //                    () => _service.ActivationsCollection.ShouldContain(postedActivation)
    //                );
    //        }
    //
    //        [Fact]
    //        public async void DeleteExercise_ByExerciseId_ReturnsNoData204()
    //        {
    //            var exerciseToDelete = Exercises.RandomizedExercise;
    //            _service.ExercisesCollection.InsertOne(exerciseToDelete);
    //
    //            var response = await _service.Api.DeleteExerciseById(exerciseToDelete.Id);
    //
    //            this.ShouldSatisfyAllConditions(
    //                    () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NoContent),
    //                    () => response.StringContent.ShouldBeEmpty(),
    //                    () => _service.ExercisesCollection.ShouldNotContain(exerciseToDelete),
    //                    () => _service.ExercisesCollection.ShouldNotBeEmpty()
    //                );
    //        }
    //
    //        [Fact]
    //        public async void DeleteActivationFromExercise_ByIds_ReturnsNoData204()
    //        {
    //            var exercise = Exercises.RandomizedExercise;
    //            var activationToDelete = Activations.RandomizedActivation;
    //            activationToDelete.ExerciseId = exercise.Id;
    //            _service.ExercisesCollection.InsertOne(exercise);
    //            _service.ActivationsCollection.InsertOne(activationToDelete);
    //
    //            var response = await _service.Api.DeleteActivationFromExercise(exercise.Id, activationToDelete.Id);
    //
    //            this.ShouldSatisfyAllConditions(
    //                    () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NoContent),
    //                    () => response.StringContent.ShouldBeEmpty(),
    //                    () => _service.ExercisesCollection.ShouldContain(exercise),
    //                    () => _service.ActivationsCollection.ShouldNotBeEmpty(),
    //                    () => _service.ActivationsCollection.ShouldNotContain(activationToDelete)
    //                );
    //        }
    //    }
}
