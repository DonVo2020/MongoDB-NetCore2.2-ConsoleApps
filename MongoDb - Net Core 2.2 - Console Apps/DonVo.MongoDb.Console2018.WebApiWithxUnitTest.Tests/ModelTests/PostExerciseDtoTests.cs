using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Models;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.Helpers;

using Shouldly;

using System.ComponentModel.DataAnnotations;
using System.Linq;

using Xunit;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.ModelTests
{
    [Trait("Api", nameof(PostExerciseDtoTests))]
    public class PostExerciseDtoTests
    {
        #region IEnumerable<ValidationResult> Validate(ValidationContext validationContext)

        [Fact]
        public void Validate_ReturnsNoErrors_WhenValidateMethodIsCalledWithValidObject()
        {
            // Arrange
            var postDto = TestData.Exercises.ContractExercisePostDto;
            var context = new ValidationContext(postDto);

            // Act
            var results = postDto.Validate(context);

            // Assert
            results.ShouldBeEmpty();
        }

        #endregion

        #region Exercise ToExercise()

        [Fact]
        public void ToExercise_ConvertsDtoToExerciseEntity_WhenValid()
        {
            // Arrange - Assure Test Data is up to date w/o null values
            var postDto = TestData.Exercises.ContractExercisePostDto;
            postDto.ShouldNotHaveNullDataMembers<PostExerciseDto>();

            // Act
            var exercise = postDto.ToExercise();

            exercise.Id.ShouldBeNull();
            exercise.Name.ShouldBe(postDto.Name);
            exercise.ShortName.ShouldBe(postDto.ShortName);
            exercise.LongName.ShouldBe(postDto.LongName);
            exercise.ShouldNotHaveNullDataMembersExcept<Exercise>(nameof(exercise.Id));
        }

        #endregion

        #region ASP.NET ComponentModel DataAnnotation Validation

        [Fact]
        public void ModelValidation_ReturnsNoErrors_WhenModelStateIsValid()
        {
            // Arrange
            var postDto = TestData.Exercises.ContractExercisePostDto;

            // Act
            var modelValidation = AspHelpers.ValidateDto(postDto);

            // Assert
            modelValidation.IsValid.ShouldBeTrue();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void ModelValidation_RequiresName_WhenNameIsNullOrWhitespace(string name)
        {
            // Arrange
            var postDto = new PostExerciseDto { Name = name };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.Name));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.First().ErrorMessage.ShouldContain($"{nameof(postDto.Name)} field is required");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void ModelValidation_RequiresShortName_WhenShortNameIsNullOrWhitespace(string shortName)
        {
            // Arrange
            var postDto = new PostExerciseDto { ShortName = shortName };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.ShortName));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.First().ErrorMessage.ShouldContain($"{nameof(postDto.ShortName)} field is required");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void ModelValidation_RequiresLongName_WhenLongNameIsNullOrWhitespace(string longName)
        {
            // Arrange
            var postDto = new PostExerciseDto { LongName = longName };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.LongName));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.First().ErrorMessage.ShouldContain($"{nameof(postDto.LongName)} field is required");
        }

        [Fact]
        public void ModelValidation_RequiresMaxLenghtOf30_ForNameProperty()
        {
            // Arrange
            var postDto = new PostExerciseDto { Name = new string('a', 31) };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.Name));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.Count.ShouldBe(1);
            modelValidation.Results.First().ErrorMessage.ShouldContain("'30'");
        }

        [Fact]
        public void ModelValidation_RequiresMaxLenghtOf20_ForShortNameProperty()
        {
            // Arrange
            var postDto = new PostExerciseDto { ShortName = new string('a', 21) };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.ShortName));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.Count.ShouldBe(1);
            modelValidation.Results.First().ErrorMessage.ShouldContain("'20'");
        }

        [Fact]
        public void ModelValidation_RequiresMaxLenghtOf30_ForLongNameProperty()
        {
            // Arrange
            var postDto = new PostExerciseDto { LongName = new string('a', 61) };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.LongName));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.Count.ShouldBe(1);
            modelValidation.Results.First().ErrorMessage.ShouldContain("'60'");
        }

        #endregion
    }
}
