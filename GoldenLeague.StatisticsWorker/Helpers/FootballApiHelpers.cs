using System;
using System.Text.RegularExpressions;

namespace GoldenLeague.StatisticsWorker.Helpers
{
    public static class FootballApiHelpers
    {
        public static int ParseRound(string round)
        {
            Regex regex = new Regex(@"\d+$");
            var value = regex.Match(round).Value;

            if (string.IsNullOrEmpty(value))
            {
                return 0;   // np. Preliminary Round - eliminacje
            }

            if (!int.TryParse(value, out int result))
            {
                throw new ArgumentException($"Cannot parse round to integer: {round} | regex value: {value}");
            }

            return result;
        }

        public static bool IsMatchFinished(string statusShort)
        {
            return statusShort == "FT";
        }
    }
}
