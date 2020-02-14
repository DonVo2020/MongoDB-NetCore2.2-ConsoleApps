using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Models;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.Helpers;

using System.Collections.Generic;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.TestData
{
    public static class Muscles
    {
        public static IEnumerable<Muscle> GetAllMusclesForCollection()
        {
            return ContractMuscles;
        }

        public static Muscle ContractMuscle => new Muscle
        {
            Id = "012345678901234567894216",
            Name = "Pectorals",
            LongName = "Pectoralis Major",
            ShortName = "Pecs",
            GroupId = "123456799012345678901827",
            RegionId = "123456789042767678589743"
        };

        public static Muscle ContractMuscle2 => new Muscle
        {
            Id = "052343678961234567894578",
            Name = "Biceps",
            LongName = "Biceps Brachialis",
            ShortName = "Bi's",
            GroupId = "72304987150894721597893",
            RegionId = "87584901692645925672935"
        };

        public static List<Muscle> ContractMuscles => new List<Muscle>
        {
            ContractMuscle,
            ContractMuscle2
        };

        public static PostMuscleDto ContractMusclePostDto => new PostMuscleDto
        {
            Name = "Latissimus",
            LongName = "Latissimus Dorsi",
            ShortName = "Lats",
            GroupId = "123456789012345678901827",
            RegionId = "123456789012345678901239"
        };

        public static Muscle MuscleWithoutId => new Muscle
        {
            Name = "Latissimus",
            LongName = "Latissimus Dorsi",
            ShortName = "Lats",
            GroupId = "123456789012345678901827",
            RegionId = "123456789012345678901239"
        };

        public static Muscle RandomizedMuscle => new Muscle
        {
            ShortName = Utilities.GetRandomString(),
            Name = Utilities.GetRandomString(),
            LongName = Utilities.GetRandomString(),
            GroupId = Utilities.GetRandomHexString(),
            RegionId = Utilities.GetRandomHexString()
        };
    }
}
