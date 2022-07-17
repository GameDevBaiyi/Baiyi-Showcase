using System;
using System.Linq;
using BaiyiShowcase.GameDesign.Terrains;
using BaiyiShowcase.Ores;
using BaiyiUtilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.GameDesign
{
    [Serializable]
    public class Ores_Design
    {
        public OreInitializer orePrefab;

        public TileSO oreGroundTile;

        [SerializeField] [FolderPath] [InlineButton("RefreshOreSOs", "Refresh")]
        private string oreSOPath;

        private void RefreshOreSOs()
        {
            oreSOs = Assistant.GetAssetsByFolderPath<OreSO>(oreSOPath).ToArray();
        }

        [Space(30f)]
        [InlineEditor]
        [ListDrawerSettings(ShowIndexLabels = true,Expanded = true, NumberOfItemsPerPage = 10)]
        public OreSO[] oreSOs;
    }
}