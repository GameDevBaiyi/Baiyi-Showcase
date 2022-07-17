using System;
using BaiyiShowcase.GameDesign;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Plants.Crops
{
    public class CropSaveDataHandler : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private Crop _crop;

        public CropSaveData CollectCropSaveData()
        {
            return new CropSaveData()
            {
                position = this.transform.localPosition,
                cropSOIndex = Array.IndexOf(_gameDesignSO.plantsDesign.cropSOs, _crop.cropSO),
                growthDays = _crop.GrowthDays
            };
        }

        public void RestoreCropSaveData(CropSaveData cropSaveData)
        {
            _crop.transform.position = cropSaveData.position;
            _crop.cropSO = _gameDesignSO.plantsDesign.cropSOs[cropSaveData.cropSOIndex];
            _crop.GrowthDays = cropSaveData.growthDays;
        }
    }
}