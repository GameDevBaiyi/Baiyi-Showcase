using System;
using System.Collections.Generic;
using System.Linq;
using BaiyiShowcase.Plants.Crops;
using BaiyiShowcase.Plants.Trees;
using BaiyiUtilities;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace BaiyiShowcase.GameDesign.Plants
{
    [Serializable]
    public class Plants_Design
    {
        public TreeInitializer treePrefab;

        public CropInitializer cropPrefab;

        [ValueDropdown("growthStages")]
        public string stageToYield;

        [ListDrawerSettings(Expanded = true)]
        public string[] growthStages;

        [Space(30f)]
        [TableList(AlwaysExpanded = true, ShowIndexLabels = true)]
        public PlantsGenerationRule[] plantsGenerationRules;

        [Space(30f)]
        [FolderPath] [InlineButton("RefreshTreeSOsArray", Label = "Refresh")]
        [SerializeField]
        private string _treeSOsPath;

        private void RefreshTreeSOsArray()
        {
            string[] guids = AssetDatabase.FindAssets("t: plantSO", new[] { _treeSOsPath });
            List<TreeSO> treeSOList = new List<TreeSO>();
            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                treeSOList.Add(AssetDatabase.LoadAssetAtPath<TreeSO>(assetPath));
            }

            treeSOs = treeSOList.ToArray();
        }

        [InlineEditor] [TableList(AlwaysExpanded = true, ShowIndexLabels = true)]
        public TreeSO[] treeSOs;

        [Space(30f)]
        [SerializeField] [FolderPath] [InlineButton("RefreshCropSOs", Label = "Refresh")]
        private string _cropSOsPath;

        private void RefreshCropSOs()
        {
            cropSOs = Assistant.GetAssetsByFolderPath<CropSO>(_cropSOsPath).ToArray();
        }

        [InlineEditor] [TableList(AlwaysExpanded = true, ShowIndexLabels = true)]
        public CropSO[] cropSOs;
    }
}