using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class ImprotantDateTime
    {
        //static void Main(string[] args)
        //{
        //    //实验目的，获取上个星期一的开始点和上个星期天的结束点的时间


        //    //ImprotantDateTime p = new ImprotantDateTime();
        //    Console.WriteLine("LastOneWeekMondayStartTime: " + ImprotantDateTime.GetLastOneWeekMondayStartTime());
        //    Console.WriteLine("LastOneWeekSundayEndTime: " + ImprotantDateTime.GetLastOneWeekSundayEndTime());
        //    Console.WriteLine("LastTwoWeekMondayStartTime: " + ImprotantDateTime.GetLastTwoWeekMondayStartTime());
        //    Console.WriteLine("LastTwoWeekSundayEndTime: " + ImprotantDateTime.GetLastTwoWeekSundayEndTime());
        //    Console.WriteLine(ImprotantDateTime.GetLastMonthStartTime());
        //    Console.WriteLine(ImprotantDateTime.GetLastMonthEndTime());
        //    Console.ReadKey();
        //}
        public static DateTime DummyToday = new DateTime(2013,11,15);
        //Warning!!!This is dummy date,if in real envrionment should be changed into DateTime.Today
        public static DateTime GetLastOneWeekMondayStartTime()
        {
            DateTime lastMonday = new DateTime(1, 1, 1);
            switch (DummyToday.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    lastMonday = DummyToday.AddDays(-11);
                    break;
                case DayOfWeek.Monday:
                    lastMonday = DummyToday.AddDays(-7);
                    break;
                case DayOfWeek.Saturday:
                    lastMonday = DummyToday.AddDays(-12);
                    break;
                case DayOfWeek.Sunday:
                    lastMonday = DummyToday.AddDays(-13);
                    break;
                case DayOfWeek.Thursday:
                    lastMonday = DummyToday.AddDays(-10);
                    break;
                case DayOfWeek.Tuesday:
                    lastMonday = DummyToday.AddDays(-8);
                    break;
                case DayOfWeek.Wednesday:
                    lastMonday = DummyToday.AddDays(-9);
                    break;

            }
            return lastMonday;
        }
        public static DateTime GetLastOneWeekSundayEndTime()
        {
            return ImprotantDateTime.GetLastOneWeekMondayStartTime().AddDays(7).AddMilliseconds(-1);
        }

        public static DateTime GetLastTwoWeekMondayStartTime()
        {
            return ImprotantDateTime.GetLastOneWeekMondayStartTime().AddDays(-7);

        }
        public static DateTime GetLastTwoWeekSundayEndTime()
        {
            //return ImprotantDateTime.GetLastTwoWeekMondayStartTime().AddDays(7).AddMilliseconds(-1);
            return ImprotantDateTime.GetLastOneWeekSundayEndTime();
        }
        public static DateTime GetLastMonthStartTime()
        {
            DateTime d1 = new DateTime(DummyToday.Year, DummyToday.Month, 1);
            return d1.AddMonths(-1);
 
        }

        public static DateTime GetLastMonthEndTime()
        {
            DateTime d1 = new DateTime(DummyToday.Year, DummyToday.Month, 1).AddMonths(-1);
            DateTime d2 = d1.AddMonths(1).AddMilliseconds(-1);
            return d2;

        }
        
    }
}
