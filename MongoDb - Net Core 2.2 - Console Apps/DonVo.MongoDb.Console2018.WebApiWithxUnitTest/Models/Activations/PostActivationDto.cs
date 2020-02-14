using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Helpers;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Models.Activations
{
    public class PostActivationDto : RequestObject
    {
        [Required, StringLength(24)]
        public string ExerciseId { get; set; }

        [Required, StringLength(24)]
        public string MuscleId { get; set; }

        [Range(0, 100)]
        public double? RangeOfMotion { get; set; }

        [Range(0, 100)]
        public double? ForceOutputPercentage { get; set; }

        [Required]
        public PostRepetitionTempoDto RepetitionTempo { get; set; }

        public PostEmgResultDto Electromyography { get; set; }

        public PostLactateResultDto LactateProduction { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (ExerciseId.IsNot24BitHex())
                results.Add(new ValidationResult($"{nameof(ExerciseId)} must be a valid 24-bit hex string.", new[] { nameof(ExerciseId) }));

            if (MuscleId.IsNot24BitHex())
                results.Add(new ValidationResult($"{nameof(MuscleId)} must be a valid 24-bit hex string.", new[] { nameof(MuscleId) }));


            return results;
        }

        public Activation ToActivation()
        {
            var activation = new Activation
            {
                ExerciseId = ExerciseId,
                MuscleId = MuscleId,
                RangeOfMotion = RangeOfMotion,
                ForceOutputPercentage = ForceOutputPercentage
            };

            if (RepetitionTempo != null)
                activation.RepetitionTempo = RepetitionTempo.ToRepetitionTempo();
            if (Electromyography != null)
                activation.Electromyography = Electromyography.ToEmgResult();
            if (LactateProduction != null)
                activation.LactateProduction = LactateProduction.ToLactateResult();

            return activation;
        }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
