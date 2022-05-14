using System;

namespace OnlineLearning.Utilities
{
    public static class DatetimeHelper
    {
        public static bool IsIntervalsOverlap(DateTime startA,DateTime endA, DateTime startB, DateTime endB)
        {

            return (startA < endB &&  startB < endA);
        }
        public static int GetDurationFromDates(DateTime start, DateTime end)
        {
            var result = (int)Math.Abs(end.Subtract(start).TotalMinutes);
            return result;
        }
    }
}
