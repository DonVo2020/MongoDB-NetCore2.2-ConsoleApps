using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities
{
    public class Activation : BingoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ExerciseId { get; set; }
        public string MuscleId { get; set; }
        public double? RangeOfMotion { get; set; }
        public double? ForceOutputPercentage { get; set; }

        public RepetitionTempo RepetitionTempo { get; set; }
        public EmgResult Electromyography { get; set; }
        public LactateResult LactateProduction { get; set; }
    }

    public class EmgResult : BingoEntity
    {
        public double? MeanEmg { get; set; }
        public double? PeakEmg { get; set; }
    }

    public class LactateResult : BingoEntity
    {
        public double? LactateProduction { get; set; }
        public double? AerobicRespiration { get; set; }
        public double? AnaerobicRespiration { get; set; }
    }

    public class RepetitionTempo : BingoEntity
    {
        public string Type { get; set; }
        public double? Duration { get; set; }
        public double? ConcentricDuration { get; set; }
        public double? EccentricDuration { get; set; }
        public double? IsometricDuration { get; set; }
    }
}
