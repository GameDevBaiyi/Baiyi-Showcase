using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Plants
{
    public abstract class PlantSO : ScriptableObject
    {
        [VerticalGroup("1")]
        public string plantName;
        [TextArea(5, 10)]
        public string description;
        [MinMaxSlider(0, 50,true)]
        public Vector2Int[] stages;
    }
}