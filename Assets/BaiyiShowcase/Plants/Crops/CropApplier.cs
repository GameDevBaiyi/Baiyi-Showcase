using BaiyiShowcase.Managers.CalendarSystem;
using BaiyiShowcase.OrderSystem;
using BaiyiUtilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Plants.Crops
{
    public class CropApplier : MonoBehaviour
    {
        [Required]
        [SerializeField] private Crop _crop;

        private OrderCollections OrderCollectionsProperty => OrderCollections.Instance;

        private void Awake()
        {
            Calendar.OnEndADay += HandleTimeLapse;
            _crop.OnEndGrowingADay += HandleCropGrow;
            _crop.OnEndHarvesting += HandleHarvest;
        }

        private void OnDestroy()
        {
            Calendar.OnEndADay -= HandleTimeLapse;
            _crop.OnEndGrowingADay -= HandleCropGrow;
            _crop.OnEndHarvesting -= HandleHarvest;
        }

        private void HandleTimeLapse(int _)
        {
            _crop.GrowthDays++;
        }

        //简单的生长系统.
        private void HandleCropGrow(int growthDays)
        {
            int stage = CalculateStage();
            _crop.spriteRenderer.sprite = _crop.cropSO.sprites[stage];

            int CalculateStage()
            {
                int localStage = 0;
                foreach (Vector2Int cropSOStage in _crop.cropSO.stages)
                {
                    if (growthDays.IsInRange(cropSOStage.x, cropSOStage.y))
                    {
                        return localStage;
                    }

                    localStage++;
                }

                Debug.Log("未找到匹配的生长周期");
                return 0;
            }
        }

        //Harvest进度到达工作量, 简单的摧毁该Crop.
        private void HandleHarvest(float progress)
        {
            if (progress >= _crop.harvestLoad)
            {
                OrderCollectionsProperty.RemoveOrder(OrderType.Harvest, this.transform);
                Destroy(this.gameObject);
            }
        }

        public void Harvested(float progress)
        {
            _crop.HarvestProgress += progress;
        }
    }
}