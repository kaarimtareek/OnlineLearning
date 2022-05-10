using System;

namespace OnlineLearning.Utilities
{
    public static class DatetimeHelper
    {
        public static bool IsIntervalsOverlap(DateTime startA,DateTime endA, DateTime startB, DateTime endB)
        {
            return !((endA < startB && startA < startB ) || (endB < startA && startB < startA));
        }
        public static int GetDurationFromDates(DateTime start, DateTime end)
        {
            return (int)(start - end).TotalMinutes;
        }
    }
}
