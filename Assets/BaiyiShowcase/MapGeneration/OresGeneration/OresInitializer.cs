using System;
using System.Collections.Generic;
using System.Linq;
using BaiyiShowcase.GameDesign;
using BaiyiShowcase.Managers.GridManager;
using BaiyiShowcase.MapGeneration.GroundGeneration;
using BaiyiShowcase.NewGame;
using BaiyiShowcase.Ores;
using BaiyiShowcase.SaveLoadSystem.LoadManage;
using BaiyiUtilities;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BaiyiShowcase.MapGeneration.OresGeneration
{
    public class OresInitializer : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private Ores _ores;
        [Required]
        [SerializeField] private OresSaveLoadHandler _saveLoadHandler;
        [Required]
        [SerializeField] private Ground _ground;
        [Required]
        [SerializeField] private GridSystem _gridSystem;

        public static event Action OnEndingInitializingOres;
        private bool IsNewScene => LoadManager.Instance.IsNewScene;

        private int Seed
        {
            get
            {
                if (NewGameSettings.Instance == true)
                {
                    return NewGameSettings.Instance.seed.GetHashCode();
                }
                else
                {
                    Debug.Log("未找到NewGameSettings.Instance.seed , 使用随机Seed");
                    return Random.Range(-1000, 1000);
                }
            }
        }

        private void Awake()
        {
            GroundInitializer.OnEndInitializingGround += InitializeOres;
        }

        private void OnDestroy()
        {
            GroundInitializer.OnEndInitializingGround -= InitializeOres;
        }

        private void InitializeOres()
        {
            if (IsNewScene)
            {
                FirstlyInitializeOres();
            }
            else
            {
                _saveLoadHandler.RestoreData();
            }

            OnEndingInitializingOres?.Invoke();
        }

        [Button]
        private void FirstlyInitializeOres()
        {
            Random.InitState(Seed);
            _ores.ClearData();
            IEnumerable<Vector2Int> allRockyLand = GetAllRockyLand();
            OreSO[] allOreSOs = _gameDesignSO.oresDesign.oreSOs;
            foreach (Vector2Int vector2Int in allRockyLand)
            {
                _ores.initialOreDataList.Add(new InitialOreData()
                {
                    position = _gridSystem.CoordToWorldPosition(vector2Int),
                    oreSO = allOreSOs.GetRandomElement()
                });
            }

            IEnumerable<Vector2Int> GetAllRockyLand()
            {
                return _ground.groundDataDictionary.Where(t => t.Value == _gameDesignSO.oresDesign.oreGroundTile)
                    .Select(t => t.Key);
            }
        }
    }
}