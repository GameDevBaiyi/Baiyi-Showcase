using UnityEditor;
using UnityEngine;

namespace BaiyiUtilities.UI
{
    public static class BaiyiUIGenerator
    {
        [MenuItem("GameObject/UI/BaiyiUI/Button")]
        public static void GenerateButton()
        {
            PrefabUtility.InstantiatePrefab(Resources.Load("Button Template"), Selection.activeTransform);
        }

        [MenuItem("GameObject/UI/BaiyiUI/Scroll View")]
        public static void GenerateScrollView()
        {
            PrefabUtility.InstantiatePrefab(Resources.Load("Scroll View Template"), Selection.activeTransform);
        }

        [MenuItem("GameObject/UI/BaiyiUI/Horizontal Layout Group")]
        public static void GenerateHorizontalLayoutGroup()
        {
            PrefabUtility.InstantiatePrefab(Resources.Load("Horizontal Layout Group Template"),
                Selection.activeTransform);
        }

        [MenuItem("GameObject/UI/BaiyiUI/Vertical Layout Group")]
        public static void GenerateVerticalLayoutGroup()
        {
            PrefabUtility.InstantiatePrefab(Resources.Load("Vertical Layout Group Template"),
                Selection.activeTransform);
        }

        [MenuItem("GameObject/UI/BaiyiUI/Tooltip With Buttons")]
        public static void GenerateTooltipWithButtons()
        {
            PrefabUtility.InstantiatePrefab(Resources.Load("Tooltip With Buttons"),
                Selection.activeTransform);
        }


        [MenuItem("GameObject/UI/BaiyiUI/Slider")]
        public static void GenerateSlider()
        {
            PrefabUtility.InstantiatePrefab(Resources.Load("Slider Template"),
                Selection.activeTransform);
        }


        [MenuItem("GameObject/UI/BaiyiUI/Dropdown")]
        public static void GenerateDropdown()
        {
            PrefabUtility.InstantiatePrefab(Resources.Load("Dropdown Template"),
                Selection.activeTransform);
        }
        
        [MenuItem("GameObject/UI/BaiyiUI/Toggle")]
        public static void GenerateToggle()
        {
            PrefabUtility.InstantiatePrefab(Resources.Load("Toggle Template"),
                Selection.activeTransform);
        }
    }
}