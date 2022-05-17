using Microsoft.AspNetCore.Mvc;
using MyGL.Data;
using MyGL.Models;

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
            // _context.DimDates.RemoveRange(_context.DimDates);
            // _context.SaveChanges();

            DateTime CurrentDate = StartDate;

            while (CurrentDate <= FinishDate)
            {
                if (_context.DimDates.Where(d => d.Date == CurrentDate).Count() == 0)
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
                        FinancialQuarterFormat = "FQ" + (int)((((CurrentDate.Month + 5) % 12) / 3) + 1),
                        FinancialQuarterMonthFormat = "FQ" + (int)((((CurrentDate.Month + 5) % 12) / 3) + 1) + "/" + CurrentDate.Month.ToString("D2") + " - " + CurrentDate.ToString("MMMM"),
                        FinancialQuarterMonthShortFormat = "FQ" + (int)((((CurrentDate.Month + 5) % 12) / 3) + 1) + "/" + CurrentDate.Month.ToString("D2") + " - " + CurrentDate.ToString("MMM")
                    };
                    _context.Update(dimDate);
                }
                CurrentDate = CurrentDate.AddDays(1);
            }
            _context.SaveChanges();
        }
    }
}