using System;
using System.Linq;
using BaiyiUtilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.GameDesign.Terrains
{
    [Serializable]
    public class GroundGeneration_Design
    {
        [Tooltip("地图种子长度")]
        [ValidateInput("ValidateSeedLength", "种子长度必须大于等于1")]
        public int seedLength;

        bool ValidateSeedLength(int length)
        {
            return length > 0;
        }

        [ValueDropdown("mapSizeChoices", HideChildProperties = true)]
        public int mapSizeDefault;

        [ListDrawerSettings(ShowIndexLabels = true, NumberOfItemsPerPage = 10, Expanded = true)]
        [ValidateInput("ValidateMapSizeChoices", "地图可选大小必须大于等于1")]
        public int[] mapSizeChoices;

        bool ValidateMapSizeChoices()
        {
            return mapSizeChoices.All(t => t > 0);
        }

        [Space(30f)]
        [Range(1, 10)]
        public float frequency;

        [Range(1, 8)]
        public int octaves;

        [ListDrawerSettings(Expanded = true)]
        public TileSO[] notWalkableTiles;

        [ValidateInput("ValidatePatchSize", "patch生成规则必须大于等于1")]
        public int patchSize;

        bool ValidatePatchSize(int size)
        {
            return size > 0;
        }

        [Space(30f)]
        [ListDrawerSettings(ShowIndexLabels = true, NumberOfItemsPerPage = 10, Expanded = true)]
        [MinMaxSlider(0f, 1f, true)]
        public Vector2[] noiseRanges;

        [Space(30f)]
        [TableList(AlwaysExpanded = true, NumberOfItemsPerPage = 10)]
        [InlineEditor]
        public TerrainSO[] terrainSOs;

        [Space(30f)]
        [SerializeField] [FolderPath] [InlineButton("RefreshTileSOs", Label = "Refresh")]
        private string _tileSOsPath;

        private void RefreshTileSOs()
        {
            tileSOs = Assistant.GetAssetsByFolderPath<TileSO>(_tileSOsPath).ToArray();
        }

        [InlineEditor]
        [ListDrawerSettings(ShowIndexLabels = true, NumberOfItemsPerPage = 10, Expanded = true)]
        public TileSO[] tileSOs;
    }
}