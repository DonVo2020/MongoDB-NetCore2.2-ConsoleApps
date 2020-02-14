using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Models
{
    public class PostExerciseDto : RequestObject
    {
        [Required, MaxLength(30)]
        public string Name { get; set; }

        [Required, MaxLength(20)]
        public string ShortName { get; set; }

        [Required, MaxLength(60)]
        public string LongName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }

        public Exercise ToExercise()
        {
            return new Exercise
            {
                Name = Name,
                ShortName = ShortName,
                LongName = LongName,
            };
        }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
