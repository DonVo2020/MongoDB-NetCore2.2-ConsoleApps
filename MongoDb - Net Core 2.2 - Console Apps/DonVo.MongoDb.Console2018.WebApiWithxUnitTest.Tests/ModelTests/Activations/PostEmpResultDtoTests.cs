using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Models.Activations;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.Helpers;

using Shouldly;

using System.Linq;

using Xunit;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.ModelTests.Activations
{
    [Trait("Api", nameof(PostEmpResultDtoTests))]
    public class PostEmpResultDtoTests
    {
        #region EmgResult ToEmgResult()

        [Fact]
        public void ToEmgResult_ConvertsDtoToEmgResultEntity_WhenValid()
        {
            // Arrange
            var postDto = TestData.Activations.ContractActivationPostDto.Electromyography;
            postDto.ShouldNotHaveNullDataMembers<PostEmgResultDto>();

            // Act
            var emgResult = postDto.ToEmgResult();

            emgResult.MeanEmg = (double)postDto.MeanEmg;
            emgResult.PeakEmg = (double)postDto.PeakEmg;

            emgResult.ShouldNotHaveNullDataMembers<EmgResult>();
        }

        #endregion

        #region ASP.NET ComponentModel DataAnnotation Validation

        [Fact]
        public void ModelValidation_ReturnsNoErrors_WhenModelStateIsValid()
        {
            // Arrange
            var postDto = TestData.Activations.ContractActivationPostDto.Electromyography;

            // Act
            var modelValidation = AspHelpers.ValidateDto(postDto);

            // Assert
            modelValidation.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void ModelValidation_MeanEmg_CannotBeNull()
        {
            // Arrange
            var postDto = new PostEmgResultDto();

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.MeanEmg));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.First().ErrorMessage.ShouldContain($"{nameof(postDto.MeanEmg)} field is required");
        }

        [Fact]
        public void ModelValidation_MeanEmg_CannotBeLessThanZero()
        {
            // Arrange
            var postDto = new PostEmgResultDto { MeanEmg = -1 };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.MeanEmg));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.First().ErrorMessage.ShouldContain($"{nameof(postDto.MeanEmg)} must be between 0 and");
        }

        [Fact]
        public void ModelValidation_PeakEmg_CannotBeNull()
        {
            // Arrange
            var postDto = new PostEmgResultDto();

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.PeakEmg));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.First().ErrorMessage.ShouldContain($"{nameof(postDto.PeakEmg)} field is required");
        }

        [Fact]
        public void ModelValidation_PeakEmg_CannotBeLessThanZero()
        {
            // Arrange
            var postDto = new PostEmgResultDto { PeakEmg = -1 };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.PeakEmg));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.First().ErrorMessage.ShouldContain($"{nameof(postDto.PeakEmg)} must be between 0 and");
        }

        #endregion
    }
}
