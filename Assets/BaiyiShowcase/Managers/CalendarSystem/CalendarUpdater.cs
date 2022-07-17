using BaiyiShowcase.GameDesign;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaiyiShowcase.Managers.CalendarSystem
{
    public class CalendarUpdater : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private Calendar _calendar;

        private float _secondsInRealityToAMinuteInGame;
        private float _timer;

        private void Awake()
        {
            _secondsInRealityToAMinuteInGame = _gameDesignSO.calendarDesign.secondsInRealityToAMinuteInGame;
        }

        private void OnEnable()
        {
            Calendar.OnEndAMinute += UpdateMinuteAndHour;
            Calendar.OnEndAHour += UpdateHourAndDay;
            Calendar.OnEndADay += UpdateDayAndMonth;
            Calendar.OnEndAMonth += UpdateMonthAndYear;
        }

        private void OnDisable()
        {
            Calendar.OnEndAMinute -= UpdateMinuteAndHour;
            Calendar.OnEndAHour -= UpdateHourAndDay;
            Calendar.OnEndADay -= UpdateDayAndMonth;
            Calendar.OnEndAMonth -= UpdateMonthAndYear;
        }

        private void Update()
        {
            if (SceneManager.GetActiveScene().buildIndex == _gameDesignSO.commonDesign.mainMenuSceneIndex) return;

            UpdateMinute();

            void UpdateMinute()
            {
                _timer += Time.deltaTime;
                if (_timer > _secondsInRealityToAMinuteInGame)
                {
                    _calendar.Minute++;
                    _timer = 0f;
                }
            }
        }

        private void UpdateMinuteAndHour(int currentMinutes)
        {
            if (currentMinutes > _gameDesignSO.calendarDesign.minutesInGameToAnHour)
            {
                _calendar.Hour++;
                _calendar.Minute = 1;
            }
        }

        private void UpdateHourAndDay(int currentHours)
        {
            if (currentHours > _gameDesignSO.calendarDesign.hoursInGameToADay)
            {
                _calendar.Day++;
                _calendar.Hour = 1;
            }
        }

        private void UpdateDayAndMonth(int currentDays)
        {
            if (currentDays > _gameDesignSO.calendarDesign.daysInGameToAMonth)
            {
                _calendar.Month++;
                _calendar.Day = 1;
            }
        }

        private void UpdateMonthAndYear(int currentMonths)
        {
            if (currentMonths > _gameDesignSO.calendarDesign.monthsInGameToAYear)
            {
                _calendar.Year++;
                _calendar.Month = 1;
            }
        }
    }
}