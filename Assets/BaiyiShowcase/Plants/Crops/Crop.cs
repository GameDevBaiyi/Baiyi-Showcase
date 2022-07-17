using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace BaiyiShowcase.Plants.Crops
{
    public class Crop : Plant
    {
        public CropSO cropSO;

        [Required]
        public SpriteRenderer spriteRenderer;
        [FormerlySerializedAs("_saveDataHandler")]
        [Required]
        public CropSaveDataHandler saveDataHandler;
        
        public float harvestLoad = 100f;

        private float _harvestProgress;
        [ShowInInspector]
        public float HarvestProgress
        {
            get => _harvestProgress;
            set
            {
                _harvestProgress = value;
                OnEndHarvesting?.Invoke(_harvestProgress);
            }
        }

        public event Action<float> OnEndHarvesting;
    }
}