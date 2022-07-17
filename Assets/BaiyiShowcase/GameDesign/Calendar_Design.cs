using System;
using Sirenix.OdinInspector;

namespace BaiyiShowcase.GameDesign
{
    [Serializable]
    public class Calendar_Design
    {
        [ValidateInput("ValidateGreaterThanZero", "最小值必须大于等于1")]
        public int secondsInRealityToAMinuteInGame;

        bool ValidateGreaterThanZero(int number)
        {
            return number > 0;
        }

        [ValidateInput("ValidateGreaterThanZero", "最小值必须大于等于1")]
        public int minutesInGameToAnHour;

        [ValidateInput("ValidateGreaterThanZero", "最小值必须大于等于1")]
        public int hoursInGameToADay;

        [ValidateInput("ValidateGreaterThanZero", "最小值必须大于等于1")]
        public int daysInGameToAMonth;

        [ValidateInput("ValidateGreaterThanZero", "最小值必须大于等于1")]
        public int monthsInGameToAYear;
    }
}