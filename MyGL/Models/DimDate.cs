using System.ComponentModel.DataAnnotations;

namespace MyGL.Models
{
    public class DimDate
    {
        public int Id { get; set; }
        public int DateId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        [Display(Name = "Month Name")]
        public string MonthName { get; set; }
        [Display(Name = "Month Name (Short)")]
        public string MonthNameShort { get; set; }
        [Display(Name = "Week Day")]
        public string WeekDay { get; set; }
        [Display(Name = "Week Day (Short)")]
        public string WeekDayShort { get; set; }
        [Display(Name = "Day Of Week")]
        public string DayOfWeek { get; set; }
        public int Quarter { get; set; }
        [Display(Name = "Quarter (Format)")]
        public string QuarterFormat { get; set; }
        [Display(Name = "Day Of Year")]
        public int DayOfYear { get; set; }
        [Display(Name = "Week Number")]
        public int WeekNunber { get; set; }
        [Display(Name = "Week Number (Format)")]
        public string WeekNumberFormat { get; set; }
        [Display(Name = "Month Name (Format)")]
        public string MonthNameFormat { get; set; }
        [Display(Name = "Month Name (Short Format)")]
        public string MonthNameShortFormat { get; set; }
        [Display(Name = "Financial Year")]
        public string FinancialYear { get; set; }
        [Display(Name = "Financial Quarter")]
        public int FinancialQuarter { get; set; }
        [Display(Name = "Financial Quarter (Format)")]
        public string FinancialQuarterFormat { get; set; }
        [Display(Name = "Financial Quarter Month (Format")]
        public string FinancialQuarterMonthFormat { get; set; }
        [Display(Name = "Financial Quarter Month (Short Format")]
        public string FinancialQuarterMonthShortFormat { get; set; }
    }
}
