using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.Helpers
{
    public class AspHelpers
    {
        public static ModelValidationResult ValidateDto(object dto)
        {
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, new ValidationContext(dto), results);

            return new ModelValidationResult
            {
                IsValid = isValid,
                Results = results
            };
        }

        public static ModelValidationResult ValidateDtoProperty(object dto, string propertyName)
        {
            var context = new ValidationContext(dto) { MemberName = propertyName };
            var results = new List<ValidationResult>();

            var propertyValue = dto.GetType().GetProperty(propertyName).GetValue(dto);

            var isValid = Validator.TryValidateProperty(propertyValue, context, results);

            return new ModelValidationResult
            {
                IsValid = isValid,
                Results = results
            };
        }


    }

    public class ModelValidationResult
    {
        public bool IsValid { get; set; }
        public List<ValidationResult> Results { get; set; }
    }
}
