using System;
using System.Linq;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.Helpers
{
    public static class Utilities
    {
        public static string GetRandomString(int length = 6)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetRandomHexString(int digits = 24)
        {
            Random random = new Random();

            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
                return result;
            return result + random.Next(16).ToString("X");
        }

        public static bool GetRandomBool()
        {
            Random random = new Random();

            var val = random.Next(0, 1);

            return val == 1;
        }

        public static int GetRandomInteger(int minValue, int maxValue)
        {
            var random = new Random();
            return random.Next(minValue, maxValue);
        }

        public static int GetRandomInteger(int digits)
        {
            var random = new Random();
            var integers = "";
            for (int i = 0; i < digits; i++)
            {
                integers += random.Next(0, 9).ToString();
            }
            return int.Parse(integers);
        }

        public static string GetRandomValueFromStrings(params string[] values)
        {
            var random = new Random();

            var indexToReturn = random.Next(0, values.Length - 1);

            return values[indexToReturn];
        }
    }
}
