using System;

namespace IsMaintenanceWindow
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Is within maintenance " + DateTimeExtensions.IsWithinMaintenanceWindow(DateTime.Now));
            Console.WriteLine("Is within maintenance DateTime(2020, 5, 16, 21, 0, 0) : " + new DateTime(2020, 5, 16, 21, 0, 0).IsWithinMaintenanceWindow());
            Console.WriteLine("Is within maintenance DateTime(2020, 5, 16, 19, 59, 0) : " + DateTimeExtensions.IsWithinMaintenanceWindow(new DateTime(2020, 5, 16, 19, 59, 0)));
            Console.WriteLine("Is within maintenance DateTime(2020, 5, 17, 3, 59, 0) : " + DateTimeExtensions.IsWithinMaintenanceWindow(new DateTime(2020, 5, 17, 3, 59, 0)));
            Console.WriteLine("Is within maintenance DateTime(2020, 5, 17, 4, 0, 0) : " + DateTimeExtensions.IsWithinMaintenanceWindow(new DateTime(2020, 5, 17, 4, 0, 0)));
            Console.WriteLine("Is within maintenance DateTime(2020, 5, 18, 21, 0, 0) : " + DateTimeExtensions.IsWithinMaintenanceWindow(new DateTime(2020, 5, 18, 21, 0, 0)));
            Console.WriteLine("Is within maintenance DateTime(2020, 5, 23, 21, 0, 0) : " + DateTimeExtensions.IsWithinMaintenanceWindow(new DateTime(2020, 5, 23, 21, 0, 0)));
            Console.WriteLine("Is within maintenance DateTime(2020, 5, 24, 0, 0, 0) : " + DateTimeExtensions.IsWithinMaintenanceWindow(new DateTime(2020, 5, 24, 0, 0, 0)));
            Console.WriteLine("Is within maintenance DateTime(2020, 5, 24, 3, 0, 0) : " + DateTimeExtensions.IsWithinMaintenanceWindow(new DateTime(2020, 5, 24, 3, 0, 0)));
        }

        
    }

    public static class DateTimeExtensions
    {
        public static bool IsWithinMaintenanceWindow(this DateTime datetime)
        {
            var utcDatetime = datetime.ToUniversalTime();

            DateTime startDateTime = DateTime.MinValue;
            if(datetime.DayOfWeek == DayOfWeek.Sunday)
            {
                startDateTime = GetNextWeekday(datetime.Date.AddDays(-1), DayOfWeek.Saturday);
            }
            else
            {
                startDateTime = GetNextWeekday(datetime.Date, DayOfWeek.Saturday);
            }

            TimeSpan ts = new TimeSpan(20, 0, 0);
            startDateTime = startDateTime.Date + ts;

            var endDateTime = GetNextWeekday(datetime.Date, DayOfWeek.Sunday);
            ts = new TimeSpan(4, 0, 0);
            endDateTime = endDateTime.Date + ts;

            if (utcDatetime > startDateTime.ToUniversalTime() && utcDatetime < endDateTime.ToUniversalTime())
                return true;
            else
                return false;
        }

        public static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysUntilNextDay = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysUntilNextDay);
        }
    }
}
