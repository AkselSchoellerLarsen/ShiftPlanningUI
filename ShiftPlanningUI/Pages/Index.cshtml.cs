using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using ShiftPlanningLibrary;
using ShiftPlanningUI.Model;
using ShiftPlanningUI.Model.Shifts;
using ShiftPlanningUI.Model.Users;
using ShiftPlanningUI.Services;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShiftPlanningUI.Pages {
    public class IndexModel : PageModel {
        private double _viewStart = -1;
        private double _viewStop = -1;
        private DateTime _startDate = GetLastMonday();
        private DateTime _endDate = GetCommingMonday();
        private int _year = -1;
        private int _month = -1;
        private int _day = -1;

        private double ViewStart {
            get {
                if (_viewStart == -1) {
                    double minutesIntoDayOfFirstShiftStart = int.MaxValue;
                    foreach (IShift shift in Shifts) {
                        if (minutesIntoDayOfFirstShiftStart > shift.Start.TimeOfDay.TotalMinutes) {
                            minutesIntoDayOfFirstShiftStart = shift.Start.TimeOfDay.TotalMinutes;
                        }
                    }
                    if (minutesIntoDayOfFirstShiftStart == int.MaxValue) {
                        _viewStart = 0;
                    } else {
                        _viewStart = minutesIntoDayOfFirstShiftStart;
                    }
                }
                return _viewStart;
            }
        }
        private double ViewStop {
            get {
                if (_viewStop == -1) {
                    double minutesIntoDayOfLastShiftEnd = int.MinValue;
                    foreach (IShift shift in Shifts) {
                        if (minutesIntoDayOfLastShiftEnd < shift.End.TimeOfDay.TotalMinutes) {
                            minutesIntoDayOfLastShiftEnd = shift.End.TimeOfDay.TotalMinutes;
                        }
                    }
                    if (minutesIntoDayOfLastShiftEnd == int.MinValue) {
                        _viewStop = 0;
                    } else {
                        _viewStop = minutesIntoDayOfLastShiftEnd;
                    }
                }
                return _viewStop;
            }
        }
        /// <summary>
        /// How many percent of the screen height should each minute corrospond to?
        /// 100/1440 would be 100 percent of the screen height for 24 hours,
        /// while 100/480 would be 100 percent of the screen height for 8 hours.
        /// Keep in mind that both the browser, the header and the footer will take some screen space.
        /// </summary>
        public double TimeScale {
            get {
                return 100.0 / (ViewStop - ViewStart);
            }
        }

        [FromQuery(Name = "year")]
        public int Year {
            get { return _year; }
            set { _year = value; }
        }
        [FromQuery(Name = "month")]
        public int Month {
            get { return _month; }
            set { _month = value; }
        }
        [FromQuery(Name = "day")]
        public int Day {
            get { return _day; }
            set { _day = value; }
        }
        [BindProperty]
        public DateTime StartDate {
            get { return _startDate; }
            set { _startDate = GetLastMonday(value); }
        }
        [BindProperty]
        public DateTime EndDate {
            get { return _endDate; }
            set { _endDate = GetCommingMonday(value.AddDays(1)); }
        }
        private DateTime Date {
            set {
                StartDate = value;
                EndDate = value;
            }
        }

        [BindProperty]
        public string DayWidth {
            get {
                return (94 / 7) + "%";
            }
        }

        [BindProperty]
        public List<IShift> Shifts { get; set; }

        public IUserService UserService { get; set; }

        public IndexModel(IUserService userService) {
            Shifts = new List<IShift>();
            UserService = userService;
        }

        public void OnGet() {
            List<IShift> shifts = new ShiftCatalogue().GetShifts(UserService.GetCurrentUser());
            foreach(IShift shift in shifts) {
                if(shift.Start < StartDate || shift.End > EndDate) {
                    Shifts.Add(shift);
                }
            }

            if (Year == -1 || Month == -1 || Day == -1) {
                Date = DateTime.Now;
            } else {
                Date = new DateTime(Year, Month, Day);
            }
        }

        public IActionResult OnPostPrevious() {
            DateTime dt = StartDate.AddDays(-1);

            return Redirect($"~/?year={dt.Year}&month={dt.Month}&day={dt.Day}");
        }

        public IActionResult OnPostNext() {
            DateTime dt = EndDate.AddDays(-1);

            return Redirect($"~/?year={dt.Year}&month={dt.Month}&day={dt.Day}");
        }

        public List<IShift> GetShiftsForDay(double weekday) {
            DateTime date = StartDate.AddDays(weekday);

            List<IShift> shifts = new List<IShift>();
            foreach(IShift shift in Shifts) {
                if(shift.Start.Date.Equals(date)) {
                    shifts.Add(shift);
                }
            }
            return shifts;
        }

        public string GetColor(IShift shift) {
            int color = 0;
            if (shift.HasUser) {
                color = (shift.UserEmail.GetHashCode() % 3) + 1;
            }
            return GetColor(color);
        }
        private string GetColor(int color) {
            switch(color) {
                case 0: return "antiquewhite";
                case 1: return "red";
                case 2: return "green";
                case 3: return "blue";
                default: return "yellow";
            }
        }

        public async Task<string> GetShiftTooltip(IShift shift) {
            if(shift.HasUser) {
                return $"Shift assigned {shift.UserEmail}\n" +
                    $"from {shift.Start}\n" +
                    $" to {shift.End}";
            }
            return $"Unassigned shift\n" +
                    $"from {shift.Start}\n" +
                    $" to {shift.End}";
        }

        public string GetDuration(IShift shift) {
            double d = shift.End.TimeOfDay.TotalMinutes - shift.Start.TimeOfDay.TotalMinutes;
            string s = string.Format("{0:N2}", d * TimeScale).Replace(',', '.') + "vh";
            return s;
        }

        public string GetOffset(IShift shift) {
            double d = shift.Start.TimeOfDay.TotalMinutes - ViewStart;
            string s = string.Format("{0:N2}", d * TimeScale).Replace(',', '.') + "vh";
            return s;
        }

        public string GetStartTime(IShift shift) {
            TimeSpan ts = shift.Start.TimeOfDay;
            string re = "";
            if (ts.Hours < 10) { re += "0"; }
            re += ts.Hours + ":";
            if (ts.Minutes < 10) { re += "0"; }
            re += ts.Minutes;
            return re;
        }

        public string GetStopTime(IShift shift) {
            TimeSpan ts = shift.End.TimeOfDay;
            string re = "";
            if (ts.Hours < 10) { re += "0"; }
            re += ts.Hours + ":";
            if (ts.Minutes < 10) { re += "0"; }
            re += ts.Minutes;
            return re;
        }

        public int[] GetHourIndicators() {
            int startHour = (int)(ViewStart / 60);
            int stopHour = (int)(ViewStop / 60);

            int[] re = new int[(stopHour - startHour) + 1];
            for (int i = startHour; i <= stopHour; i++) {
                re[i - startHour] = i;
            }
            return re;
        }

        public string GetTimeIndicatorOffset(int i) {
            double offset = (i - (ViewStart / 60)) * 60;
            return string.Format("{0:N2}", offset * TimeScale).Replace(',', '.') + "vh";
        }

        private static DateTime GetLastMonday(DateTime? dateTime = null) {
            if(dateTime is null) {
                return GetLastMonday(DateTime.Now.Date);
            } else {
                DateTime dt = (DateTime)dateTime;
                DayOfWeek Weekday = dt.DayOfWeek;
                if (Weekday.Equals(DayOfWeek.Monday)) {
                    return dt.Date;
                } else {
                    return GetLastMonday(dt.Date.AddDays(-1));
                }
            }
        }

        private static DateTime GetCommingMonday(DateTime? dateTime = null) {
            if (dateTime is null) {
                return GetCommingMonday(DateTime.Now.Date);
            } else {
                DateTime dt = (DateTime)dateTime;
                DayOfWeek Weekday = dt.DayOfWeek;
                if (Weekday.Equals(DayOfWeek.Monday)) {
                    return dt.Date;
                } else {
                    return GetCommingMonday(dt.Date.AddDays(1));
                }
            }
        }
    }
}