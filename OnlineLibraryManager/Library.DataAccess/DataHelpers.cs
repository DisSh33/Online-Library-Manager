using Library.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess
{
    public static class DataHelpers
    {
        public static List<string> HumanizeBusinessHours(IEnumerable<BranchHours> branchHours)
        {
            var hours = new List<string>();

            foreach (var time in branchHours)
            {
                var day = HumanizeDayOfWeek(time.DayOfWeek);
                var openTime = HumanizeTime(time.OpenTime);
                var closeTime = HumanizeTime(time.CloseTime);
                var timeEntry = $"{day} {openTime} to {closeTime}";
                hours.Add(timeEntry);
            }

            return hours;
        }

        private static string HumanizeDayOfWeek(int number)
        {
            return Enum.GetName(typeof(DayOfWeek), number - 1);
        }

        private static string HumanizeTime(int time)
        {
            var result = TimeSpan.FromHours(time);
            return result.ToString("hh':'mm");
        }
    }
}
