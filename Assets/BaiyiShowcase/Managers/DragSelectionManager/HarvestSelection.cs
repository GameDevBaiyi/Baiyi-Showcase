using BaiyiShowcase.OrderSystem;
using BaiyiShowcase.Plants.Crops;
using UnityEngine;

namespace BaiyiShowcase.Managers.DragSelectionManager
{
    public class HarvestSelection : DragToSelect_State
    {
        protected override void DoWithRaycastHit2DArray(  Collider2D[] collider2Ds)
        {
            foreach (Collider2D item in collider2Ds)
            {
                if (item.TryGetComponent(out Crop crop))
                {
                    crop.OrderTypeProperty = OrderType.Harvest;
                }
            }
        }
    }
}