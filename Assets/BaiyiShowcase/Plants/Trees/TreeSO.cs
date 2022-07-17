using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Plants.Trees
{
    [CreateAssetMenu(fileName = "TreeSO", menuName = "SO/Plants/TreeSO")]
    public class TreeSO : PlantSO
    {
        [VerticalGroup("1")]
        public Sprite sprite;
        [VerticalGroup("1")]
        public RuntimeAnimatorController runtimeAnimatorController;
    }
}