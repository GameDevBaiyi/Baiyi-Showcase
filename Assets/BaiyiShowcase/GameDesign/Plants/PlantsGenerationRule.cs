using System;
using BaiyiShowcase.GameDesign.Terrains;
using BaiyiShowcase.Plants;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace BaiyiShowcase.GameDesign.Plants
{
    [Serializable]
    public class PlantsGenerationRule
    {
        [VerticalGroup("Settings")]
        public TileSO tileSO;
        [VerticalGroup("Settings")]
        public float density;
        [TableList(AlwaysExpanded = true, ShowIndexLabels = true)]
        public PlantSOAndRatio[] plantSOAndRatios;
    }

    [Serializable]
    public class PlantSOAndRatio
    {
        [Range(1, 10)]
        public int plantRatio = 1;
        [FormerlySerializedAs("plantSOs")]
        public PlantSO plantSO;
    }
}