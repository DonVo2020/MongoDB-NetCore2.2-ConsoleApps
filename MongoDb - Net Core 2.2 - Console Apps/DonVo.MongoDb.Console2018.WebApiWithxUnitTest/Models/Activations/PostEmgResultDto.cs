using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;

using System.ComponentModel.DataAnnotations;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Models.Activations
{
    public class PostEmgResultDto : RequestObject
    {
        [Required, Range(0, double.MaxValue)]
        public double? MeanEmg { get; set; }

        [Required, Range(0, double.MaxValue)]
        public double? PeakEmg { get; set; }

        public EmgResult ToEmgResult()
        {
            return new EmgResult
            {
                MeanEmg = MeanEmg,
                PeakEmg = PeakEmg
            };
        }
    }
}
