using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using System.ComponentModel.DataAnnotations;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Models.Activations
{
    public class PostLactateResultDto : RequestObject
    {
        [Required, Range(0, double.MaxValue)]
        public double? LactateProduction { get; set; }

        [Range(0, double.MaxValue)]
        public double? AerobicRespiration { get; set; }

        [Range(0, double.MaxValue)]
        public double? AnaerobicRespiration { get; set; }

        public LactateResult ToLactateResult()
        {
            return new LactateResult
            {
                LactateProduction = LactateProduction,
                AerobicRespiration = AerobicRespiration,
                AnaerobicRespiration = AnaerobicRespiration
            };
        }
    }
}
