using System;
using System.Collections.Generic;
using BaiyiShowcase.GameDesign;
using BaiyiShowcase.GameDesign.Terrains;
using BaiyiShowcase.SaveLoadSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.MapGeneration.GroundGeneration
{
    public class GroundSaveLoadHandler : SaveLoadHandler
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private Ground _ground;

        public override object CollectSaveData()
        {
            Dictionary<Vector2Int, int> coordTileIndexDictionary = new Dictionary<Vector2Int, int>();
            foreach (KeyValuePair<Vector2Int, TileSO> keyValuePair in _ground.groundDataDictionary)
            {
                coordTileIndexDictionary[keyValuePair.Key] =
                    Array.IndexOf(_gameDesignSO.groundGenerationDesign.tileSOs, keyValuePair.Value);
            }

            return new GroundSaveData()
            {
                coordTileIndexDictionary = coordTileIndexDictionary
            };
        }

        [Button]
        public override void RestoreData()
        {
            if (SaveLoadAgent.Instance.cache.TryGetValue(id, out object data))
            {
                _ground.ClearData();

                GroundSaveData groundSaveData = (GroundSaveData)data;
                foreach (KeyValuePair<Vector2Int, int> keyValuePair in groundSaveData.coordTileIndexDictionary)
                {
                    _ground.groundDataDictionary[keyValuePair.Key] =
                        _gameDesignSO.groundGenerationDesign.tileSOs[keyValuePair.Value];
                }
            }
            else
            {
                Debug.Log("Cache中未找到Key");
            }
        }
    }
}