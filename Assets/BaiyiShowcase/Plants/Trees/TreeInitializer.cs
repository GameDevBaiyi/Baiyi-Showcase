using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Plants.Trees
{
    public class TreeInitializer : MonoBehaviour
    {
        [Required]
        [SerializeField] private Tree _tree;
        [Required]
        public TreeSaveDataHandler saveDataHandler;

        //在TreesGenerator里面直接调用即可,用Event也可以,但此处并不复杂,只有一个Method,没有必要.
        public void InitializeTree(TreeSO treeSO)
        {
            _tree.treeSO = treeSO;
            _tree.spriteRenderer.sprite = treeSO.sprite;
            _tree.animator.runtimeAnimatorController = treeSO.runtimeAnimatorController;
            _tree.GrowthDays = Random.Range(0, _tree.treeSO.stages[^1].y);
        }
    }
}