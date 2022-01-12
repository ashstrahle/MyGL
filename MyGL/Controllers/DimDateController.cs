using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyGL.Data;
using MyGL.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyGL
{
    public class DimDateController : Controller
    {
        private MyGLContext _context;

        public DimDateController(MyGLContext context)
        {
            _context = context;
        }

        public void Generate(DateTime StartDate, DateTime FinishDate)
        {
            // Clear DimDates
            _context.DimDates.RemoveRange(_context.DimDates);
            _context.SaveChanges();

            DateTime CurrentDate = StartDate;

            while (CurrentDate <= FinishDate)
            {
                DimDate dimDate = new()
                {
                    DateId = Int32.Parse(CurrentDate.Year.ToString("D4") + CurrentDate.Month.ToString("D2") + CurrentDate.Day.ToString("D2")),
                    Date = CurrentDate,
                    Year = Int32.Parse(CurrentDate.Year.ToString("D4")),
                    Month = CurrentDate.Month,
                    Day = CurrentDate.Day,
                    MonthName = CurrentDate.ToString("MMMM"),
                    MonthNameShort = CurrentDate.ToString("MMM"),
                    WeekDay = CurrentDate.ToString("dddd"),
                    WeekDayShort = CurrentDate.ToString("ddd"),
                    DayOfWeek = (int)(((int)CurrentDate.DayOfWeek + 6) % 7 + 1),
                    Quarter = (int)((CurrentDate.Month + 2) / 3),
                    QuarterFormat = "Q" + ((int)(CurrentDate.Month + 2) / 3),
                    DayOfYear = CurrentDate.DayOfYear,
                    WeekNumber = (int)((CurrentDate.DayOfYear / 7) + 1),
                    WeekNumberFormat = "W" + (int)((CurrentDate.DayOfYear / 7) + 1),
                    MonthNameFormat = CurrentDate.Month.ToString("D2") + " - " + CurrentDate.ToString("MMMM"),
                    MonthNameShortFormat = CurrentDate.Month.ToString("D2") + " - " + CurrentDate.ToString("MMM"),
                    FinancialYear = (CurrentDate.Month < 7 ? (int)(CurrentDate.Year - 1) + "/" + CurrentDate.Year : CurrentDate.Year + "/" + (int)(CurrentDate.Year + 1)),
                    FinancialQuarter = (int)(((CurrentDate.Month + 5) % 12) / 3) + 1,
                    FinancialQuarterFormat = "Q" + (int)((((CurrentDate.Month + 5) % 12) / 3) + 1),
                    FinancialQuarterMonthFormat = "Q" + (int)((((CurrentDate.Month + 5) % 12) / 3) + 1) + "/" + CurrentDate.Month.ToString("D2") + " - " + CurrentDate.ToString("MMMM"),
                    FinancialQuarterMonthShortFormat = "Q" + (int)((((CurrentDate.Month + 5) % 12) / 3) + 1) + "/" + CurrentDate.Month.ToString("D2") + " - " + CurrentDate.ToString("MMM")
                };
                _context.Add(dimDate);
                CurrentDate = CurrentDate.AddDays(1);
            }
            _context.SaveChanges();
        }
    }
}