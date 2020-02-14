using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Enums;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Models.Activations;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.Helpers;

using Shouldly;

using System.Linq;

using Xunit;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.ModelTests.Activations
{
    [Trait("Api", nameof(PostRepetitionTempoDtoTests))]
    public class PostRepetitionTempoDtoTests
    {
        #region RepetitionTempo ToRepetitionTempo()

        [Fact]
        public void ToRepetitionTempo_ConvertsDtoToRepetitionTempoEntity_WhenValid()
        {
            // Arrange
            var postDto = TestData.Activations.ContractActivationPostDto.RepetitionTempo;
            postDto.ShouldNotHaveNullDataMembers<PostRepetitionTempoDto>();

            // Act
            var repetitionTempo = postDto.ToRepetitionTempo();

            repetitionTempo.Type = postDto.Type;
            repetitionTempo.Duration = (double)postDto.Duration;
            repetitionTempo.ConcentricDuration = (double)postDto.ConcentricDuration;
            repetitionTempo.EccentricDuration = (double)postDto.EccentricDuration;
            repetitionTempo.IsometricDuration = (double)postDto.IsometricDuration;

            repetitionTempo.ShouldNotHaveNullDataMembers<RepTempoType>();
        }

        #endregion

        #region ASP.NET ComponentModel DataAnnotation Validation

        [Fact]
        public void ModelValidation_ReturnsNoErrors_WhenModelStateIsValid()
        {
            // Arrange
            var postDto = TestData.Activations.ContractActivationPostDto.RepetitionTempo;

            // Act
            var modelValidation = AspHelpers.ValidateDto(postDto);

            // Assert
            modelValidation.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void ModelValidation_Type_CannotBeNull()
        {
            // Arrange
            var postDto = new PostRepetitionTempoDto();

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.Type));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.First().ErrorMessage.ShouldContain($"{nameof(postDto.Type)} field is required");
        }

        [Fact]
        public void ModelValidation_Duration_CannotBeLessThanZero()
        {
            // Arrange
            var postDto = new PostRepetitionTempoDto { Duration = -1 };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.Duration));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.Count.ShouldBe(1);
            modelValidation.Results.First().ErrorMessage.ShouldContain($"{nameof(postDto.Duration)} must be between 0 and");
        }

        [Fact]
        public void ModelValidation_ConcentricDuration_CannotBeLessThanZero()
        {
            // Arrange
            var postDto = new PostRepetitionTempoDto { ConcentricDuration = -1 };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.ConcentricDuration));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.Count.ShouldBe(1);
            modelValidation.Results.First().ErrorMessage.ShouldContain($"{nameof(postDto.ConcentricDuration)} must be between 0 and");
        }

        [Fact]
        public void ModelValidation_EccentricDuration_CannotBeLessThanZero()
        {
            // Arrange
            var postDto = new PostRepetitionTempoDto { EccentricDuration = -1 };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.EccentricDuration));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.Count.ShouldBe(1);
            modelValidation.Results.First().ErrorMessage.ShouldContain($"{nameof(postDto.EccentricDuration)} must be between 0 and");
        }

        [Fact]
        public void ModelValidation_IsometricDuration_CannotBeLessThanZero()
        {
            // Arrange
            var postDto = new PostRepetitionTempoDto { IsometricDuration = -1 };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.IsometricDuration));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.Count.ShouldBe(1);
            modelValidation.Results.First().ErrorMessage.ShouldContain($"{nameof(postDto.IsometricDuration)} must be between 0 and");
        }

        #endregion
    }
}
