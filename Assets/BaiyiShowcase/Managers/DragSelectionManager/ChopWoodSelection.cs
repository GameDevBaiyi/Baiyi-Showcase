using BaiyiShowcase.OrderSystem;
using UnityEngine;
using Tree = BaiyiShowcase.Plants.Trees.Tree;

namespace BaiyiShowcase.Managers.DragSelectionManager
{
    public class ChopWoodSelection : DragToSelect_State
    {
        protected override void DoWithRaycastHit2DArray(Collider2D[] collider2Ds)
        {
            foreach (Collider2D item in collider2Ds)
            {
                if (item.TryGetComponent(out Tree tree))
                {
                    tree.OrderTypeProperty = OrderType.ChopWood;
                }
            }
        }
    }
}