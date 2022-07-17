using System;
using BaiyiUtilities.Singleton;

namespace BaiyiShowcase.Managers.CalendarSystem
{
    public class Calendar : Singleton<Calendar>
    {
        private int _minute;
        public int Minute
        {
            get => _minute;
            set
            {
                _minute = value;
                OnEndAMinute?.Invoke(_minute);
            }
        }
        public static event Action<int> OnEndAMinute;

        private int _hour;
        public int Hour
        {
            get => _hour;
            set
            {
                _hour = value;
                OnEndAHour?.Invoke(_hour);
            }
        }
        public static event Action<int> OnEndAHour;

        private int _day;
        public int Day
        {
            get => _day;
            set
            {
                _day = value;
                OnEndADay?.Invoke(_day);
            }
        }
        public static event Action<int> OnEndADay;

        private int _month;
        public int Month
        {
            get => _month;
            set
            {
                _month = value;
                OnEndAMonth?.Invoke(_month);
            }
        }
        public static event Action<int> OnEndAMonth;

        private int _year;
        public int Year
        {
            get => _year;
            set
            {
                _year = value;
                OnEndAYear?.Invoke(_year);
            }
        }
        public static event Action<int> OnEndAYear;
    }
}