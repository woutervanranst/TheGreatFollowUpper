using System;

namespace TheGreatFollowUpper.Util
{
    internal static class Utils
    {
        public static DateTime ParseFollowUpDate(object value)
        {
            if (value is string)
                value = ParseTagToType((string)value);

            if (value is DayOfWeek)
                return GetNextWeekday((DayOfWeek)value);
            if (value is int)
                return AddBusinessDays((int)value);

            throw new NotImplementedException($"Did not expect value {value}");
        }

        private static object ParseTagToType(string value2)
        {
            if (int.TryParse(value2, out int businessDays))
                return businessDays;

            if (Enum.TryParse<DayOfWeek>(value2, out DayOfWeek day))
                return day;

            throw new NotImplementedException();
        }
        private static DateTime GetNextWeekday(DayOfWeek day)
        {
            var start = DateTime.Now;

            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            var daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;

            if (daysToAdd == 0)
                daysToAdd = 7; //hack: if we say next friday and it s friday, the result is 0

            return start.AddDays(daysToAdd);
        }
        private static DateTime AddBusinessDays(int businessDays)
        {
            var startDate = DateTime.Now;

            var direction = Math.Sign(businessDays);
            if (direction == 1)
            {
                if (startDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    startDate = startDate.AddDays(2);
                    businessDays = businessDays - 1;
                }
                else if (startDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    startDate = startDate.AddDays(1);
                    businessDays = businessDays - 1;
                }
            }
            else
            {
                if (startDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    startDate = startDate.AddDays(-1);
                    businessDays = businessDays + 1;
                }
                else if (startDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    startDate = startDate.AddDays(-2);
                    businessDays = businessDays + 1;
                }
            }

            var initialDayOfWeek = Convert.ToInt32(startDate.DayOfWeek);

            var weeksBase = Math.Abs(businessDays / 5);
            var addDays = Math.Abs(businessDays % 5);

            if ((direction == 1 && addDays + initialDayOfWeek > 5) ||
                 (direction == -1 && addDays >= initialDayOfWeek))
            {
                addDays += 2;
            }

            var totalDays = (weeksBase * 7) + addDays;
            return startDate.AddDays(totalDays * direction);
        }
    }
}
