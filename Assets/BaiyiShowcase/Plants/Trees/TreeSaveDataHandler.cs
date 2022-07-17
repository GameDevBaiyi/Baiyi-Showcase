using System;
using BaiyiShowcase.GameDesign;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Plants.Trees
{
    public class TreeSaveDataHandler : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private Tree _tree;

        public TreeSaveData CollectTreeSaveData()
        {
            return new TreeSaveData()
            {
                position = this.transform.localPosition,
                treeSOIndex = Array.IndexOf(_gameDesignSO.plantsDesign.treeSOs, _tree.treeSO),
                growthDays = _tree.GrowthDays
            };
        }

        public void RestoreTreeSaveData(TreeSaveData treeSaveData)
        {
            _tree.transform.position = treeSaveData.position;
            _tree.treeSO = _gameDesignSO.plantsDesign.treeSOs[treeSaveData.treeSOIndex];
            _tree.spriteRenderer.sprite = _tree.treeSO.sprite;
            _tree.animator.runtimeAnimatorController = _tree.treeSO.runtimeAnimatorController;
            _tree.GrowthDays = treeSaveData.growthDays;
        }
    }
}