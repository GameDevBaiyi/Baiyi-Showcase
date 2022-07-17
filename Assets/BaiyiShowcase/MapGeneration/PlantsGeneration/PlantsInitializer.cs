using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BaiyiShowcase.GameDesign;
using BaiyiShowcase.GameDesign.Plants;
using BaiyiShowcase.GameDesign.Terrains;
using BaiyiShowcase.Managers.GridManager;
using BaiyiShowcase.MapGeneration.GroundGeneration;
using BaiyiShowcase.NewGame;
using BaiyiShowcase.Plants;
using BaiyiShowcase.SaveLoadSystem.LoadManage;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BaiyiShowcase.MapGeneration.PlantsGeneration
{
    public class PlantsInitializer : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private Plants _plants;
        [Required]
        [SerializeField] private PlantsSaveLoadHandler _saveLoadHandler;
        [Required]
        [SerializeField] private Ground _ground;
        [Required]
        [SerializeField] private GridSystem _gridSystem;

        public static event Action OnEndInitializingPlants;

        [BoxGroup("Debugger", CenterLabel = true)]
        [SerializeField]
        private TerrainSO _debuggerTerrainSO;

        private TerrainSO CurrentTerrain
        {
            get
            {
                if (NewGameSettings.Instance == true)
                {
                    return NewGameSettings.Instance.terrainSO;
                }
                else
                {
                    Debug.Log("未找到 NewGameSettingsDesign.Instance, 使用Debugger");
                    return _debuggerTerrainSO;
                }
            }
        }
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
            GroundGenerator.OnEndGeneratingGround += InitializePlants;
        }

        private void OnDestroy()
        {
            GroundGenerator.OnEndGeneratingGround -= InitializePlants;
        }

        private void InitializePlants()
        {
            if (IsNewScene)
            {
                FirstlyInitializePlants();
            }
            else
            {
                _saveLoadHandler.RestoreData();
            }

            OnEndInitializingPlants?.Invoke();
        }


        [Button]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        private void FirstlyInitializePlants()
        {
            Random.InitState(Seed);
            _plants.ClearData();
            IEnumerable<TileSO> allTileSOHavingPlants =
                _gameDesignSO.plantsDesign.plantsGenerationRules.Select(t => t.tileSO);
            IEnumerable<TileSO> currentTileSOsHavingPlants = CurrentTerrain.tileRatios.Select(t => t.tileSO)
                .Where(t => allTileSOHavingPlants.Contains(t));

            foreach (TileSO tileSO in currentTileSOsHavingPlants)
            {
                if (!TryGetFirstRule(out PlantsGenerationRule correspondingRule)) continue;

                //获得所有的 符合当前tileSO的,随机排列的坐标.
                IEnumerable<Vector2Int> allRandomlyOrderedTiles = _ground.groundDataDictionary
                    .Where(t => t.Value == tileSO)
                    .Select(t => t.Key)
                    .OrderBy(_ => Random.Range(Int32.MinValue, Int32.MaxValue));

                //计算该tile上总共有多少植物.
                int plantsCount = (int)(allRandomlyOrderedTiles.Count() * correspondingRule.density);

                //计算植物比例的分母.
                float ratioDenominator = correspondingRule.plantSOAndRatios.Select(t => t.plantRatio).Sum();

                //计算具体各种植物分别有几颗.
                List<int> plantCounts =
                    correspondingRule.plantSOAndRatios.Select(t =>
                        (int)(t.plantRatio / ratioDenominator * plantsCount)).ToList();

                List<IEnumerable<Vector2Int>> sortedCoords = new List<IEnumerable<Vector2Int>>();
                for (int i = 0; i < plantCounts.Count; i++)
                {
                    int skipCount = plantCounts.Take(i).Sum();
                    IEnumerable<Vector2Int> coords = allRandomlyOrderedTiles.Skip(skipCount).Take(plantCounts[i]);
                    sortedCoords.Add(coords);
                }

                int index = 0;
                foreach (IEnumerable<Vector2Int> vector2Ints in sortedCoords)
                {
                    PlantSO correspondingPlantSO = correspondingRule.plantSOAndRatios[index].plantSO;
                    foreach (Vector2Int vector2Int in vector2Ints)
                    {
                        _plants.initialPlantDataList.Add(new InitialPlantData()
                        {
                            position = _gridSystem.CoordToWorldPosition(vector2Int),
                            plantSO = correspondingPlantSO,
                        });
                    }

                    index++;
                }
                //继续

                bool TryGetFirstRule(out PlantsGenerationRule rule)
                {
                    try
                    {
                        rule =
                            _gameDesignSO.plantsDesign.plantsGenerationRules.First(t => t.tileSO == tileSO);
                        return true;
                    }
                    catch
                    {
                        Debug.Log("没找到匹配tileSO:" + tileSO + "的植物生成规则");
                        rule = new PlantsGenerationRule();
                        return false;
                    }
                }
            }
        }
    }
}