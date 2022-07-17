using System;
using BaiyiShowcase.Colonists;
using BaiyiShowcase.GameDesign.Terrains;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.GameDesign
{
    [Serializable]
    public class Colonists_Design
    {
        public ColonistInitializer colonistPrefab;

        [BoxGroup("角色行为设计")]
        public Vector2 idleTimeRange;
        public Vector2Int pathLengthRange;
        

        [Space(30f)]
        [ListDrawerSettings(ShowIndexLabels = true, NumberOfItemsPerPage = 10, Expanded = true)]
        public TileSO[] tilesCantGeneratingColonistFirstly;

        [Space(30f)]
        [ListDrawerSettings(ShowIndexLabels = true, NumberOfItemsPerPage = 10, Expanded = true,
            ListElementLabelName = "name")]
        public ColonistsCustomization[] customizations;
    }
}