using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Helpers;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Models
{
    public class PostMuscleDto : RequestObject
    {
        [Required, MaxLength(30)]
        public string Name { get; set; }

        [Required, MaxLength(20)]
        public string ShortName { get; set; }

        [Required, MaxLength(60)]
        public string LongName { get; set; }

        [StringLength(24)]
        public string GroupId { get; set; }

        [StringLength(24)]
        public string RegionId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (GroupId.IsNot24BitHex())
                results.Add(new ValidationResult($"{nameof(GroupId)} must be a valid 24-bit hex string.", new[] { nameof(GroupId) }));

            if (RegionId.IsNot24BitHex())
                results.Add(new ValidationResult($"{nameof(RegionId)} must be a valid 24-bit hex string.", new[] { nameof(RegionId) }));

            return results;
        }

        public Muscle ToMuscle()
        {
            return new Muscle
            {
                Name = Name,
                ShortName = ShortName,
                LongName = LongName,
                GroupId = GroupId,
                RegionId = RegionId
            };
        }
    }
}
