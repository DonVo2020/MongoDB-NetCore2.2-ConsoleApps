using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Models.Activations;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.Helpers;

using System.Collections.Generic;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.TestData
{
    public static class Activations
    {
        public static IEnumerable<Activation> GetAllActivationsForCollection()
        {
            return ContractActivations;
        }

        public static Activation ContractActivation => new Activation
        {
            Id = "012345678921834567894216",
            ExerciseId = Exercises.ContractExercise.Id,
            MuscleId = Muscles.ContractMuscle.Id,
            ForceOutputPercentage = 100.00,
            RangeOfMotion = 100,
            RepetitionTempo = new RepetitionTempo
            {
                Type = "Regular",
                Duration = 1
            },
            Electromyography = new EmgResult
            {
                MeanEmg = 134,
                PeakEmg = 433
            },
            LactateProduction = new LactateResult
            {
                AerobicRespiration = 21,
                AnaerobicRespiration = 87,
                LactateProduction = 34
            }
        };

        public static Activation ContractActivation2 => new Activation
        {
            Id = "012345678221233567894216",
            ExerciseId = Exercises.ContractExercise2.Id,
            MuscleId = Muscles.ContractMuscle2.Id,
            ForceOutputPercentage = 100.00,
            RangeOfMotion = 100,
            RepetitionTempo = new RepetitionTempo
            {
                Type = "Tempo",
                EccentricDuration = 4,
                ConcentricDuration = 1
            },
            Electromyography = new EmgResult
            {
                MeanEmg = 83,
                PeakEmg = 99
            },
            LactateProduction = new LactateResult
            {
                AerobicRespiration = 11,
                AnaerobicRespiration = 65,
                LactateProduction = 2
            }
        };

        public static List<Activation> ContractActivations => new List<Activation>
        {
            ContractActivation,
            ContractActivation2
        };

        public static PostActivationDto ContractActivationPostDto => new PostActivationDto
        {
            ExerciseId = "123456789012345678904587",
            MuscleId = "123456789012345678904963",
            ForceOutputPercentage = 100.00,
            RangeOfMotion = 100,
            RepetitionTempo = new PostRepetitionTempoDto
            {
                Type = "Tempo",
                EccentricDuration = 4,
                ConcentricDuration = 1,
                IsometricDuration = 2,
                Duration = 3
            },
            Electromyography = new PostEmgResultDto
            {
                MeanEmg = 12,
                PeakEmg = 39
            },
            LactateProduction = new PostLactateResultDto
            {
                AerobicRespiration = 211,
                AnaerobicRespiration = 635,
                LactateProduction = 34
            }
        };

        public static Activation ContractActivationPostDtoResponseMock => new Activation
        {
            Id = "123456789012345678945671",
            ExerciseId = "123456789012345678904587",
            MuscleId = "123456789012345678904963",
            ForceOutputPercentage = 100.00,
            RangeOfMotion = 100,
            RepetitionTempo = new RepetitionTempo
            {
                Type = "Tempo",
                EccentricDuration = 4,
                ConcentricDuration = 1,
                IsometricDuration = 2,
                Duration = 3
            },
            Electromyography = new EmgResult
            {
                MeanEmg = 12,
                PeakEmg = 39
            },
            LactateProduction = new LactateResult
            {
                AerobicRespiration = 211,
                AnaerobicRespiration = 635,
                LactateProduction = 34
            }
        };

        public static Activation ActivationWithoutId => new Activation
        {
            ExerciseId = "123456789012345678904587",
            MuscleId = "123456789012345678904963",
            ForceOutputPercentage = 100.00,
            RangeOfMotion = 100,
            RepetitionTempo = new RepetitionTempo
            {
                Type = "Tempo",
                EccentricDuration = 4,
                ConcentricDuration = 1
            },
            Electromyography = new EmgResult
            {
                MeanEmg = 12,
                PeakEmg = 39
            },
            LactateProduction = new LactateResult
            {
                AerobicRespiration = 211,
                AnaerobicRespiration = 635,
                LactateProduction = 34
            }
        };

        public static Activation RandomizedActivation => new Activation
        {
            ExerciseId = Utilities.GetRandomHexString(),
            MuscleId = Utilities.GetRandomHexString(),
            ForceOutputPercentage = Utilities.GetRandomInteger(2),
            RangeOfMotion = Utilities.GetRandomInteger(2),
            RepetitionTempo = new RepetitionTempo
            {
                Type = "Tempo",
                EccentricDuration = Utilities.GetRandomInteger(1),
                ConcentricDuration = Utilities.GetRandomInteger(1)
            },
            Electromyography = new EmgResult
            {
                MeanEmg = Utilities.GetRandomInteger(2),
                PeakEmg = Utilities.GetRandomInteger(2)
            },
            LactateProduction = new LactateResult
            {
                AerobicRespiration = Utilities.GetRandomInteger(2),
                AnaerobicRespiration = Utilities.GetRandomInteger(2),
                LactateProduction = Utilities.GetRandomInteger(2)
            }
        };

    }
}
