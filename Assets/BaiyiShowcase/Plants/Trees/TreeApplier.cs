using BaiyiShowcase.Managers.CalendarSystem;
using BaiyiShowcase.OrderSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Plants.Trees
{
    public class TreeApplier : MonoBehaviour
    {
        [Required]
        [SerializeField] private Tree _tree;

        private OrderCollections OrderCollectionsProperty => OrderCollections.Instance;

        private void Awake()
        {
            Calendar.OnEndADay += HandleTimeLapse;
            _tree.OnEndGrowingADay += HandleTreeGrow;
            _tree.OnEndChopping += HandleChopWood;
        }

        private void OnDestroy()
        {
            Calendar.OnEndADay -= HandleTimeLapse;
            _tree.OnEndGrowingADay -= HandleTreeGrow;
            _tree.OnEndChopping -= HandleChopWood;
        }

        private void HandleTimeLapse(int _)
        {
            _tree.GrowthDays++;
        }

        private void HandleTreeGrow(int growthDays)
        {
            float treeSize = CalculateTreeSize();
            _tree.transform.localScale = new Vector3(treeSize, treeSize, 1f);

            float CalculateTreeSize()
            {
                int maxGrowthDays = _tree.treeSO.stages[^1].y;
                return Mathf.InverseLerp(0f, maxGrowthDays, growthDays);
            }
        }

        //砍伐进度到达工作量, 简单的摧毁该Tree.
        private void HandleChopWood(float progress)
        {
            if (progress >= _tree.chopWorkLoad)
            {
                //砍伐完成移除对应指令, 可以在OrderCollections实现Delegate,在此处激活.
                OrderCollectionsProperty.RemoveOrder(OrderType.ChopWood, this.transform);
                Destroy(this.gameObject);
            }
        }

        public void Chopped(float progress)
        {
            _tree.ChopProgress += progress;
        }
    }
}