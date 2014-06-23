using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
//using System.Windows.Controls;

namespace DateExtensions
{
    public static class WeekDayUtilities
    {
        /// <summary>
        /// Find the closest weekday to the given date
        /// </summary>
        /// <param name="includeStartDate">if the supplied date is on the specified day of the week, return that date or continue to the next date</param>
        /// <param name="searchForward">search forward or backward from the supplied date. if a null parameter is given, the closest weekday (ie in either direction) is returned</param>
        /// <returns></returns>
        public static DateTime ClosestWeekDay(this DateTime date, DayOfWeek weekday, bool includeStartDate = true, bool? searchForward=true)
        {
            if (!searchForward.HasValue && !includeStartDate) 
            {
                throw new ArgumentException("if searching in both directions, start date must be a valid result");
            }
            var day = date.DayOfWeek;
            int add = ((int)weekday - (int)day);
            if (searchForward.HasValue)
            {
                if (add < 0 && searchForward.Value)
                {
                    add += 7;
                }
                else if (add > 0 && !searchForward.Value)
                {
                    add -= 7;
                }
                else if (add == 0 && !includeStartDate)
                {
                    add = searchForward.Value ? 7 : -7;
                }
            }
            else if (add < -3) 
            {
                add += 7; 
            }
            else if (add > 3)
            {
                add -= 7;
            }
            return date.AddDays(add);
        }
        public static DateTime PriorWeekDay(this DateTime date, DayOfWeek weekday)
        {
            return date.AddDays(-Enumerable.Range(1, 7).First(r => date.AddDays(-r).DayOfWeek == weekday));
        }
        /*
        public static void AddDatesExcept(this CalendarBlackoutDatesCollection cal,DateTime startDate, DateTime endDate, DateTime[] selectableDates)
        {
            var lastBlackout = startDate;
            foreach (var d in selectableDates)
            {
                if (lastBlackout == d)
                {
                    lastBlackout = lastBlackout.AddDays(1);
                    continue;
                }
                cal.Add(new CalendarDateRange(lastBlackout, d.AddDays(-1)));
                lastBlackout = d.AddDays(1);
            }
            if (lastBlackout < endDate) 
            {
                cal.Add(new CalendarDateRange(lastBlackout, endDate));
            };
        }
         * */


        public static DateTime NthWeekdayOfMonth(int year, int month, int nthOfMonth, DayOfWeek weekday = DayOfWeek.Monday)
        {
            int furtherDays = (nthOfMonth - 1) * 7;
            return new DateTime(year, month, 1).ClosestWeekDay(weekday, true).AddDays(furtherDays);
        }
    }
}
