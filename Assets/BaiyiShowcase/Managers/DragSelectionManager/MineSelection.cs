using BaiyiShowcase.OrderSystem;
using BaiyiShowcase.Ores;
using UnityEngine;

namespace BaiyiShowcase.Managers.DragSelectionManager
{
    public class MineSelection : DragToSelect_State
    {
        protected override void DoWithRaycastHit2DArray(Collider2D[] collider2Ds)
        {
            foreach (Collider2D item in collider2Ds)
            {
                if (item.TryGetComponent(out Ore ore))
                {
                    ore.OrderTypeProperty = OrderType.Mine;
                }
            }
        }
    }
}