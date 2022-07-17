using System;
using BaiyiUtilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.GameDesign.Terrains
{
    [CreateAssetMenu(fileName = "TerrainSO", menuName = "SO/Terrain/TerrainSO")]
    public class TerrainSO : ScriptableObject
    {
        [ListDrawerSettings(ShowIndexLabels = true,ListElementLabelName = "Name",Expanded = true)]
        [HideLabel]
        public TileRatio[] tileRatios;
    }

    [Serializable]
    public class TileRatio
    {
        public TileSO tileSO;
        [ProgressBar(0, 5)]
        public int ratio;

        private string Name => tileSO.GetNameWithSubtractingFromSpace();
    }
}