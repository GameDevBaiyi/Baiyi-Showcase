using System;
using System.Collections.Generic;
using BaiyiShowcase.GameDesign.Terrains;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.MapGeneration.GroundGeneration
{
    public class Ground : MonoBehaviour
    {
        [ListDrawerSettings(ShowPaging = true, NumberOfItemsPerPage = 10)]
        public Dictionary<Vector2Int, TileSO> groundDataDictionary = new Dictionary<Vector2Int, TileSO>();

        [Button]
        public void ClearData()
        {
            groundDataDictionary.Clear();
        }

        [BoxGroup("Debugger", CenterLabel = true)]
        [SerializeField]
        private Vector2Int _coord;

        [Button]
        public void PrintPointInfo()
        {
            Debug.Log(groundDataDictionary[_coord]);
        }
    }

    [Serializable]
    public struct GroundSaveData
    {
        public Dictionary<Vector2Int, int> coordTileIndexDictionary;
    }
}