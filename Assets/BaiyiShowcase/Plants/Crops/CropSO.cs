using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Plants.Crops
{
    [CreateAssetMenu(fileName = "CropSO", menuName = "SO/Plants/CropSO")]
    public class CropSO : PlantSO
    {
        [ListDrawerSettings(Expanded = true)]
        public Sprite[] sprites;
    }
}