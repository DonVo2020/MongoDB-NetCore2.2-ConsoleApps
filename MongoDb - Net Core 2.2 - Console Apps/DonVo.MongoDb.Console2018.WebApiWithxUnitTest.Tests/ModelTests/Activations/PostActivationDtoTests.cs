using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Models.Activations;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.Helpers;

using Shouldly;

using System.ComponentModel.DataAnnotations;
using System.Linq;

using Xunit;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.ModelTests.Activations
{
    [Trait("Api", nameof(PostActivationDtoTests))]
    public class PostActivationDtoTests
    {
        #region IEnumerable<ValidationResult> Validate(ValidationContext validationContext)

        [Fact]
        public void Validate_ReturnsNoErrors_WhenValidateMethodIsCalledWithValidObject()
        {
            // Arrange
            var postDto = TestData.Activations.ContractActivationPostDto;
            var context = new ValidationContext(postDto);

            // Act
            var results = postDto.Validate(context);

            // Assert
            results.ShouldBeEmpty();
        }

        [Fact]
        public void Validate_ReturnsError_WhenExerciseIdIsNot24BitHexString()
        {
            // Arrange
            var postDto = TestData.Activations.ContractActivationPostDto;
            var context = new ValidationContext(postDto);
            postDto.ExerciseId = "Non24BitHex";

            // Act
            var results = postDto.Validate(context);

            // Assert
            results.Count().ShouldBe(1);
            results.First().ErrorMessage.ShouldContain(nameof(postDto.ExerciseId));
            results.First().MemberNames.ShouldContain(nameof(postDto.ExerciseId));
        }

        [Fact]
        public void Validate_ReturnsError_WhenMuscleIdIsNot24BitHexString()
        {
            // Arrange
            var postDto = TestData.Activations.ContractActivationPostDto;
            var context = new ValidationContext(postDto);
            postDto.MuscleId = "Non24BitHex";

            // Act
            var results = postDto.Validate(context);

            // Assert
            results.Count().ShouldBe(1);
            results.First().ErrorMessage.ShouldContain(nameof(postDto.MuscleId));
            results.First().MemberNames.ShouldContain(nameof(postDto.MuscleId));
        }

        #endregion

        #region Activation ToActivation()

        [Fact]
        public void ToActivation_ConvertsDtoToActivationEntity_WhenValid()
        {
            // Arrange - Assure Test Data is up to date w/o null values
            var postDto = TestData.Activations.ContractActivationPostDto;
            postDto.ShouldNotHaveNullDataMembers<PostActivationDto>();

            // Act
            var activation = postDto.ToActivation();

            // Assert
            activation.Id.ShouldBeNull();
            activation.ExerciseId.ShouldBe(postDto.ExerciseId);
            activation.MuscleId.ShouldBe(postDto.MuscleId);
            activation.RangeOfMotion.ShouldBe(postDto.RangeOfMotion);
            activation.ForceOutputPercentage.ShouldBe(postDto.ForceOutputPercentage);
            activation.RepetitionTempo.ShouldBe(postDto.RepetitionTempo.ToRepetitionTempo());
            activation.Electromyography.ShouldBe(postDto.Electromyography.ToEmgResult());

            activation.ShouldNotHaveNullDataMembersExcept<Activation>(nameof(activation.Id));
        }

        [Fact]
        public void ToActivation_DoesNotThrowError_WhenElectroMygraphyIsNull()
        {
            // Arrange - Assure Test Data is up to date w/o null values
            var postDto = TestData.Activations.ContractActivationPostDto;
            postDto.ShouldNotHaveNullDataMembers<PostActivationDto>();
            postDto.Electromyography = null;

            // Act - Assert
            Should.NotThrow(() => postDto.ToActivation());
        }

        [Fact]
        public void ToActivation_DoesNotThrowError_WhenRepetitionTempoIsNull()
        {
            // Arrange - Assure Test Data is up to date w/o null values
            var postDto = TestData.Activations.ContractActivationPostDto;
            postDto.ShouldNotHaveNullDataMembers<PostActivationDto>();
            postDto.RepetitionTempo = null;

            // Act - Assert
            Should.NotThrow(() => postDto.ToActivation());
        }

        [Fact]
        public void ToActivation_DoesNotThrowError_WhenLactateProductionIsNull()
        {
            // Arrange - Assure Test Data is up to date w/o null values
            var postDto = TestData.Activations.ContractActivationPostDto;
            postDto.ShouldNotHaveNullDataMembers<PostActivationDto>();
            postDto.LactateProduction = null;

            // Act - Assert
            Should.NotThrow(() => postDto.ToActivation());
        }

        #endregion

        #region ASP.NET ComponentModel DataAnnotation Validation

        [Fact]
        public void ModelValidation_ReturnsNoErrors_WhenModelStateIsValid()
        {
            // Arrange
            var postDto = TestData.Activations.ContractActivationPostDto;

            // Act
            var modelValidation = AspHelpers.ValidateDto(postDto);

            // Assert
            modelValidation.IsValid.ShouldBeTrue();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void ModelValidation_ExerciseId_CannotBeNullOrWhitespace(string exerciseId)
        {
            // Arrange
            var postDto = new PostActivationDto { ExerciseId = exerciseId };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.ExerciseId));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.First().ErrorMessage.ShouldContain($"{nameof(postDto.ExerciseId)} field is required");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void ModelValidation_MuscleId_CannotBeNullOrWhitespace(string muscleId)
        {
            // Arrange
            var postDto = new PostActivationDto { MuscleId = muscleId };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.MuscleId));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.First().ErrorMessage.ShouldContain($"{nameof(postDto.MuscleId)} field is required");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(101)]
        public void ModelValidation_RangeOfMotion_MustBeBetween0And100(double value)
        {
            // Arrange
            var postDto = new PostActivationDto { RangeOfMotion = value };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.RangeOfMotion));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.Count.ShouldBe(1);
            modelValidation.Results.First().ErrorMessage.ShouldContain("0");
            modelValidation.Results.First().ErrorMessage.ShouldContain("100");
        }

        [Fact]
        public void ModelValidation_RepetitionTempo_CannotBeNull()
        {
            // Arrange
            var postDto = new PostActivationDto { RepetitionTempo = null };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.RepetitionTempo));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.First().ErrorMessage.ShouldContain($"{nameof(postDto.RepetitionTempo)} field is required");
        }

        #endregion
    }
}
