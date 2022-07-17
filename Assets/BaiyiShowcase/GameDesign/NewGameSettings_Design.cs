using System;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BaiyiShowcase.GameDesign
{
    [Serializable]
    public class NewGameSettings_Design
    {
        [ValueDropdown("characterCountChoices", HideChildProperties = true)]
        public int characterCountDefault;

        [ListDrawerSettings(ShowIndexLabels = true, NumberOfItemsPerPage = 10, Expanded = true)]
        [ValidateInput("ValidateCharacterCountChoices", "最少生成角色个数必须大于等于1")]
        public int[] characterCountChoices;

        bool ValidateCharacterCountChoices(int[] choices)
        {
            return choices.All(t => t > 0);
        }

        [Space(30f)]
        [ListDrawerSettings(ShowPaging = true, NumberOfItemsPerPage = 10, Expanded = true)]
        public string[] namesToRandomize = new[]
        {
            "Ara", "Vazgen", "Hagop", "Karekin", "Siran", "Arax", "Angelina", "Arevik", "Nerses", "Samvel", "Harutyun",
            "Vartan", "Khajag", "Zabel", "Mane"
        };

        [Space(30f)]
        [ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "Name", NumberOfItemsPerPage = 10,
            Expanded = true)]
        [Tooltip("x为最小值,y为最大值.")]
        [ValidateInput("ValidateSkillValueRange", "技能最小值必须大于等于1,最大值必须大于最小值")]
        public Vector2Int skillValueRange;

        bool ValidateSkillValueRange(Vector2Int range)
        {
            return range.x > 0 && range.y > range.x;
        }

        [ListDrawerSettings(ShowIndexLabels = true, NumberOfItemsPerPage = 10, Expanded = true)]
        [ValidateInput("ValidateSkills", "技能名字不能为空")]
        public string[] skills;

        bool ValidateSkills(string[] skillArray)
        {
            return skillArray.All(t => !string.IsNullOrEmpty(t));
        }
    }
}