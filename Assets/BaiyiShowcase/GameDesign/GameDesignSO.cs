using BaiyiShowcase.GameDesign.Plants;
using BaiyiShowcase.GameDesign.Terrains;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.GameDesign
{
    [LabelWidth(200)]
    [CreateAssetMenu(fileName = "GameDesignSO", menuName = "SO/GameDesignSO")]
    public class GameDesignSO : ScriptableObject
    {
        [TabGroup("1", "地板生成")] [HideLabel]
        public GroundGeneration_Design groundGenerationDesign;
        
        [TabGroup("1", "通用底层设计")] [HideLabel]
        public Common_Design commonDesign;

        [TabGroup("1", "游戏静态设置")] [HideLabel]
        public GameStaticSettings_Design gameStaticSettingsDesign;

        [TabGroup("1", "新游戏")] [HideLabel]
        public NewGameSettings_Design newGameSettingsDesign;

        [TabGroup("1", "日期系统")] [HideLabel]
        public Calendar_Design calendarDesign;


        [TabGroup("1", "植物")] [HideLabel]
        public Plants_Design plantsDesign;

        [TabGroup("1", "矿石")] [HideLabel]
        public Ores_Design oresDesign;

        [TabGroup("1", "动物")] [HideLabel]
        public Animals_Design animalsDesign;

        [TabGroup("1", "殖民者")] [HideLabel]
        public Colonists_Design colonistsDesign;
    }
}