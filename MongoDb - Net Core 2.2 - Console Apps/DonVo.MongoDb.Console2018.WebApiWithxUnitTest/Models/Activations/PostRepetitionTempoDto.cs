using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;

using System.ComponentModel.DataAnnotations;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Models.Activations
{
    public class PostRepetitionTempoDto
    {
        [Required]
        public string Type { get; set; }

        [Range(0, double.MaxValue)]
        public double? Duration { get; set; }

        [Range(0, double.MaxValue)]
        public double? ConcentricDuration { get; set; }

        [Range(0, double.MaxValue)]
        public double? EccentricDuration { get; set; }

        [Range(0, double.MaxValue)]
        public double? IsometricDuration { get; set; }

        public RepetitionTempo ToRepetitionTempo()
        {
            return new RepetitionTempo
            {
                Type = Type,
                Duration = Duration,
                ConcentricDuration = ConcentricDuration,
                EccentricDuration = EccentricDuration,
                IsometricDuration = IsometricDuration
            };
        }
    }
}
