using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Plants.Crops
{
    public class CropInitializer : MonoBehaviour
    {
        [Required]
        [SerializeField] private Crop _crop;
        [Required]
        public CropSaveDataHandler saveDataHandler;

        public void InitializeCrop(CropSO cropSO)
        {
            _crop.cropSO = cropSO;
            _crop.GrowthDays = Random.Range(0, _crop.cropSO.stages[^1].y);
        }
    }
}