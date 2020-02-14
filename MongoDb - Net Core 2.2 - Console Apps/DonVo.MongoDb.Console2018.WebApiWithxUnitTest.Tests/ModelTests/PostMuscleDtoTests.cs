using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Models;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.Helpers;

using Shouldly;

using System.ComponentModel.DataAnnotations;
using System.Linq;

using Xunit;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.ModelTests
{
    [Trait("Api", nameof(PostMuscleDtoTests))]
    public class PostMuscleDtoTests
    {
        #region IEnumerable<ValidationResult> Validate(ValidationContext validationContext)

        [Fact]
        public void Validate_ReturnsNoErrors_WhenValidateMethodIsCalledWithValidObject()
        {
            // Arrange
            var postDto = TestData.Muscles.ContractMusclePostDto;
            var context = new ValidationContext(postDto);

            // Act
            var results = postDto.Validate(context);

            // Assert
            results.ShouldBeEmpty();
        }

        [Fact]
        public void Validate_ReturnsError_WhenGroupIdIsNot24BitHexString()
        {
            // Arrange
            var postDto = TestData.Muscles.ContractMusclePostDto;
            var context = new ValidationContext(postDto);
            postDto.GroupId = "Non24BitHex";

            // Act
            var results = postDto.Validate(context);

            // Assert
            results.Count().ShouldBe(1);
            results.First().ErrorMessage.ShouldContain(nameof(postDto.GroupId));
            results.First().MemberNames.ShouldContain(nameof(postDto.GroupId));
        }

        [Fact]
        public void Validate_ReturnsError_WhenRegionIdIsNot24BitHexString()
        {
            // Arrange
            var postDto = TestData.Muscles.ContractMusclePostDto;
            var context = new ValidationContext(postDto);
            postDto.RegionId = "Non24BitHex";

            // Act
            var results = postDto.Validate(context);

            // Assert
            results.Count().ShouldBe(1);
            results.First().ErrorMessage.ShouldContain(nameof(postDto.RegionId));
            results.First().MemberNames.ShouldContain(nameof(postDto.RegionId));
        }

        #endregion

        #region Muscle ToMuscle()

        [Fact]
        public void ToMuscle_ConvertsDtoToMuscleEntity_WhenValid()
        {
            // Arrange - Assure Test Data is up to date w/o null values
            var postDto = TestData.Muscles.ContractMusclePostDto;
            postDto.ShouldNotHaveNullDataMembers<PostMuscleDto>();

            // Act
            var muscle = postDto.ToMuscle();

            muscle.Id.ShouldBeNull();
            muscle.Name.ShouldBe(postDto.Name);
            muscle.ShortName.ShouldBe(postDto.ShortName);
            muscle.LongName.ShouldBe(postDto.LongName);
            muscle.GroupId.ShouldBe(postDto.GroupId);
            muscle.RegionId.ShouldBe(postDto.RegionId);
            muscle.ShouldNotHaveNullDataMembersExcept<Muscle>(nameof(muscle.Id));
        }

        #endregion

        #region ASP.NET ComponentModel DataAnnotation Validation

        [Fact]
        public void ModelValidation_ReturnsNoErrors_WhenModelStateIsValid()
        {
            // Arrange
            var postDto = TestData.Muscles.ContractMusclePostDto;

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
            var postDto = new PostMuscleDto { Name = name };

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
            var postDto = new PostMuscleDto { ShortName = shortName };

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
            var postDto = new PostMuscleDto { LongName = longName };

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
            var postDto = new PostMuscleDto { Name = new string('a', 31) };

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
            var postDto = new PostMuscleDto { ShortName = new string('a', 21) };

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
            var postDto = new PostMuscleDto { LongName = new string('a', 61) };

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
