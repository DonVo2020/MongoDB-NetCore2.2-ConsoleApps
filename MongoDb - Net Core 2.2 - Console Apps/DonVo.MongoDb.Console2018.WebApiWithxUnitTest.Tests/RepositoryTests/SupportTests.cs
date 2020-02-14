using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Helpers;

using Shouldly;

using Xunit;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.RepositoryTests
{
    [Trait("Repository", nameof(SupportTests))]
    public class SupportTests
    {
        #region bool DeepEquals(this object actual, object expected)

        private readonly ParentObject _expected = new ParentObject
        {
            BsonId = "12341223",
            Name = "Name1",
            NestedObject = new NestedObject
            {
                JsonIgnoreValue = "JsonIgnoreThing",
                SomeValue = "dfsadf"
            }
        };

        private readonly ParentObject _actual = new ParentObject
        {
            BsonId = "12341223",
            Name = "Name1",
            NestedObject = new NestedObject
            {
                JsonIgnoreValue = "JsonIgnoreThing",
                SomeValue = "dfsadf"
            }
        };

        [Fact]
        public void DeepEquals_ReturnsTrue_When2ObjectsContainSameData()
        {
            // Act
            var isEqual = _actual.DeepEquals(_expected);

            // Assert
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void DeepEquals_ReturnsFalse_WhenRootObjectContainsMismatch()
        {
            // Arrange
            _actual.Name = "DifferentName";

            // Act
            var isEqual = _actual.DeepEquals(_expected);

            // Assert
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void DeepEquals_ReturnsFalse_WhenNestedObjectContainsMismatch()
        {
            // Arrange
            _actual.NestedObject.SomeValue = "SomeOtherValue";

            // Act
            var isEqual = _actual.DeepEquals(_expected);

            // Assert
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void DeepEquals_ReturnsTrueOnMismatch_IfJsonIgnoreAttributeExistsOnProperty()
        {
            // Arrange
            _actual.NestedObject.JsonIgnoreValue = "SomeOtherIgnoredValue";

            // Act
            var isEqual = _actual.DeepEquals(_expected);

            // Assert
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void DeepEquals_ReturnsTrueOnMismatch_IfBsonIdAttributeExistsOnProperty()
        {
            // Arrange
            _actual.BsonId = "SomeOtherIgnoredValue";

            // Act
            var isEqual = _actual.DeepEquals(_expected);

            // Assert
            isEqual.ShouldBeTrue();
        }

        #endregion

        #region bool Is24BitHex(this string actual)

        [Theory]
        [InlineData("123456789012345678904567")]
        [InlineData("ABCDEFabcdef1234567890ac")]
        public void Is24BitHex_ReturnsTrue_WhenStringIs24BitHex(string stringValue)
        {
            // Act
            var is24BitHex = stringValue.Is24BitHex();

            // Assert
            is24BitHex.ShouldBeTrue();
        }

        [Fact]
        public void Is24BitHex_ReturnsFalse_WhenStringIsNot24Bits()
        {
            // Arrange
            var stringValue = "ABCDEF123456789";

            // Act
            var is24BitHex = stringValue.Is24BitHex();

            // Assert
            is24BitHex.ShouldBeFalse();
        }

        [Fact]
        public void Is24BitHex_ReturnsFalse_WhenStringContainsNonHexChar()
        {
            // Arrange
            var stringValue = "12345678901234567890478z";

            // Act
            var is24BitHex = stringValue.Is24BitHex();

            // Assert
            is24BitHex.ShouldBeFalse();
        }

        #endregion

        #region bool IsNot24BitHex(this string actual)

        [Theory]
        [InlineData("123456789012345678904567")]
        [InlineData("ABCDEFabcdef12345678905a")]
        public void IsNot24BitHex_ReturnsFalse_WhenStringIs24BitHex(string stringValue)
        {
            // Act
            var isNot24BitHex = stringValue.IsNot24BitHex();

            // Assert
            isNot24BitHex.ShouldBeFalse();
        }

        [Fact]
        public void Is24NotBitHex_ReturnsTrue_WhenStringIsNot24Bits()
        {
            // Arrange
            var stringValue = "ABCDEF123456789";

            // Act
            var isNot24BitHex = stringValue.IsNot24BitHex();

            // Assert
            isNot24BitHex.ShouldBeTrue();
        }

        [Fact]
        public void IsNot24BitHex_ReturnsTrue_WhenStringContainsNonHexChar()
        {
            // Arrange
            var stringValue = "12345678901234567890478z";

            // Act
            var isNot24BitHex = stringValue.IsNot24BitHex();

            // Assert
            isNot24BitHex.ShouldBeTrue();
        }

        #endregion
    }

    internal class ParentObject
    {
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public string BsonId { get; set; }
        public string Name { get; set; }
        public NestedObject NestedObject { get; set; }
    }

    internal class NestedObject
    {
        [Newtonsoft.Json.JsonIgnore]
        public string JsonIgnoreValue { get; set; }
        public string SomeValue { get; set; }
    }
}
