namespace Bingo.Specification.IntegrationTests
{
    //    [Trait("Integration", nameof(ActivationsControllerTest))]
    //    public class ActivationsControllerTest : TestBase
    //    {
    //        private readonly ServiceFixture _service;
    //
    //        public ActivationsControllerTest(ServiceFixture service)
    //        {
    //            _service = service;
    //        }
    //        
    //        [Fact]
    //        public async void GetActivations_WhenDataExists_ReturnsListContainingExpectedActivation200()
    //        {
    //            var expectedActivations = Activations.ContractActivations;
    //
    //            var response = await _service.Api.GetActivations();
    //            var actualActivations = response.GetContent();
    //
    //            this.ShouldSatisfyAllConditions(
    //                    () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.OK),
    //                    () => expectedActivations.ForEach(activation =>
    //                            actualActivations.ShouldContain(activation))
    //                );
    //        }
    //
    //        [Fact]
    //        public async void GetActivation_ByActivationId_ReturnsExpectedActivation200()
    //        {
    //            var expectedActivation = Activations.ContractActivation;
    //
    //            var response = await _service.Api.GetActivationById(expectedActivation.Id);
    //            var actualActivation = response.GetContent();
    //
    //            this.ShouldSatisfyAllConditions(
    //                    () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.OK),
    //                    () => actualActivation.ShouldBe(expectedActivation),
    //                    () => _service.ActivationsCollection.ShouldContain(actualActivation)
    //                );
    //        }
    //
    //        [Fact]
    //        public async void PostActivation_ByValidDto_ReturnsPostedActivation201()
    //        {
    //            var postDto = Activations.ContractActivationPostDto;
    //
    //            var response = await _service.Api.PostActivation(postDto);
    //            var postedActivation = response.GetContent();
    //
    //            this.ShouldSatisfyAllConditions(
    //                    () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.Created),
    //                    () => postedActivation.Id.ShouldNotBeNull(),
    //                    () => postedActivation.ShouldBe(postDto.ToActivation()),
    //                    () => _service.ActivationsCollection.ShouldContain(postedActivation)
    //                );
    //        }
    //
    //        [Fact]
    //        public async void DeleteActivation_ByActivationId_ReturnsNoData204()
    //        {
    //            var activationToDelete = Activations.RandomizedActivation;
    //            _service.ActivationsCollection.InsertOne(activationToDelete);
    //
    //            var response = await _service.Api.DeleteActivationById(activationToDelete.Id);
    //
    //            this.ShouldSatisfyAllConditions(
    //                    () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NoContent),
    //                    () => response.StringContent.ShouldBeEmpty(),
    //                    () => _service.ActivationsCollection.ShouldNotContain(activationToDelete),
    //                    () => _service.ActivationsCollection.ShouldNotBeEmpty()
    //                );
    //        }
    //    }
}
