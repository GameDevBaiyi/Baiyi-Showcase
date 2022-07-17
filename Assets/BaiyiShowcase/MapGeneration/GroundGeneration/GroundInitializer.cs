using System;
using System.Collections.Generic;
using BaiyiShowcase.GameDesign;
using BaiyiShowcase.Managers.GridManager;
using BaiyiShowcase.NewGame;
using BaiyiShowcase.SaveLoadSystem.LoadManage;
using BaiyiUtilities;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Linq;
using BaiyiShowcase.GameDesign.Terrains;
using BaiyiShowcase.PathfindingSystem;
using Random = UnityEngine.Random;

namespace BaiyiShowcase.MapGeneration.GroundGeneration
{
    public class GroundInitializer : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private Ground _ground;
        [Required]
        [SerializeField] private GroundSaveLoadHandler _saveLoadHandler;
        [Required]
        [SerializeField] private GridSystem _gridSystem;

        [BoxGroup("Debugger", CenterLabel = true)]
        [SerializeField]
        private TerrainSO _terrainSO;
        private bool IsNewScene => LoadManager.Instance.IsNewScene;
        private TerrainSO CurrentTerrainSO
        {
            get
            {
                if (NewGameSettings.Instance == true)
                {
                    return NewGameSettings.Instance.terrainSO;
                }
                else
                {
                    Debug.Log("未找到 NewGameSettings.Instance.terrainSO, 使用Debugger");
                    return _terrainSO;
                }
            }
        }

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

        public static event Action OnEndInitializingGround;

        private void Awake()
        {
            PathfindingGridInitializer.OnEndInitializingPathfinding += InitializeGround;
        }

        private void OnDestroy()
        {
            PathfindingGridInitializer.OnEndInitializingPathfinding -= InitializeGround;
        }

        //OnEndInitializingPathfinding. 即PathfindingGrid数据恢复后,再开始恢复Ground.
        private void InitializeGround()
        {
            if (IsNewScene)
            {
                FirstlyInitializeGround();
            }
            else
            {
                _saveLoadHandler.RestoreData();
            }

            OnEndInitializingGround?.Invoke();
        }

        [Button]
        private void FirstlyInitializeGround()
        {
            _ground.ClearData();
            Random.InitState(Seed);
            List<HashSet<Vector2Int>> patches = GetAllPatches();
            AllocatePatchesRandomly();


            List<HashSet<Vector2Int>> GetAllPatches()
            {
                HashSet<Vector2Int> patch;
                int mapSize = _gridSystem.currentMapSize;
                float fixedFrequency = _gameDesignSO.groundGenerationDesign.frequency * (mapSize / 100f);
                float[,] perlinNoise =
                    Assistant.GetPerlinNoise(mapSize, fixedFrequency, _gameDesignSO.groundGenerationDesign.octaves);
                HashSet<Vector2Int> allLeftPoints = new HashSet<Vector2Int>();
                for (int x = 0; x < mapSize; x++)
                {
                    for (int y = 0; y < mapSize; y++)
                    {
                        allLeftPoints.Add(new Vector2Int(x, y));
                    }
                }

                List<HashSet<Vector2Int>> allPatches = new List<HashSet<Vector2Int>>();

                while (true)
                {
                    Vector2Int startCoord = new Vector2Int();
                    if (TryGetStartCoord())
                    {
                        Vector2 range = GetRangeOfCurrentCoord();
                        patch = Assistant.FloodFill8D(startCoord,
                                t => allLeftPoints.Contains(t) &&
                                     perlinNoise[t.x, t.y].IsInRange(range.x, range.y),
                                _gameDesignSO.groundGenerationDesign.patchSize)
                            .ToHashSet();

                        allPatches.Add(patch);
                        allLeftPoints.ExceptWith(patch);
                    }
                    else
                    {
                        return allPatches;
                    }

                    bool TryGetStartCoord()
                    {
                        try
                        {
                            startCoord = allLeftPoints.First();
                            return true;
                        }
                        catch
                        {
                            return false;
                        }
                    }

                    Vector2 GetRangeOfCurrentCoord()
                    {
                        float currentNoise = perlinNoise[startCoord.x, startCoord.y];
                        foreach (Vector2 range in _gameDesignSO.groundGenerationDesign.noiseRanges)
                        {
                            if (currentNoise >= range.x &&
                                currentNoise < range.y)
                            {
                                return range;
                            }
                        }

                        Debug.Log("此点的Noise" + currentNoise + "无合适的范围");
                        return Vector2.zero;
                    }
                }
            }

            void AllocatePatchesRandomly()
            {
                Dictionary<Vector2, TileSO> rangeAndTileSO = new Dictionary<Vector2, TileSO>();
                float rangeBottom = 0f;
                float rangeTop = 0f;
                foreach (TileRatio tileRatio in CurrentTerrainSO.tileRatios)
                {
                    rangeTop = rangeBottom + tileRatio.ratio;
                    rangeAndTileSO[new Vector2(rangeBottom, rangeTop)] = tileRatio.tileSO;
                    rangeBottom = rangeTop;
                }

                foreach (HashSet<Vector2Int> vector2Ints in patches)
                {
                    float randomRange = Random.Range(0f, rangeTop);
                    Vector2 targetKey = GetTargetKey();
                    TileSO targetTileSO = rangeAndTileSO[targetKey];
                    foreach (Vector2Int point in vector2Ints)
                    {
                        _ground.groundDataDictionary[point] = targetTileSO;
                    }

                    Vector2 GetTargetKey()
                    {
                        foreach (Vector2 key in rangeAndTileSO.Keys)
                        {
                            if (randomRange.IsInRange(key.x, key.y))
                            {
                                return key;
                            }
                        }

                        Debug.Log("没根据随机的范围在Dictionary中找到对应的key");
                        return Vector2.zero;
                    }
                }
            }
        }


        //另一种生成规则
        // private void InitializeGroundRule2()
        // {
        //     int mapSize = _gridSystem.currentMapSize;
        //     _ground.tileDataList = new List<TileData>();
        //
        //     float[,] perlinNoise = Assistant.GetPerlinNoise(mapSize, _gameDesignSO.frequency, _gameDesignSO.octaves);
        //     HashSet<Vector2Int> leftNonPatchCoords = new HashSet<Vector2Int>();
        //     GenerateBottom();
        //     GeneratePatch();
        //
        //     void GenerateBottom()
        //     {
        //         for (int x = 0; x < mapSize; x++)
        //         {
        //             for (int y = 0; y < mapSize; y++)
        //             {
        //                 leftNonPatchCoords.Add(new Vector2Int(x, y));
        //                 CompareNoiseWithDesign();
        //
        //                 void CompareNoiseWithDesign()
        //                 {
        //                     foreach (BottomGroundSetting baseGroundTileSetting in NewGameSettingsDesign.Instance.terrainSO
        //                                  .bottomGroundSettings)
        //                     {
        //                         if (perlinNoise[x, y] >= baseGroundTileSetting.perlinNoiseRange.x &&
        //                             perlinNoise[x, y] <= baseGroundTileSetting.perlinNoiseRange.y)
        //                         {
        //                             _ground.tileDataList.Add(new TileData()
        //                             {
        //                                 coord = new Vector2Int(x, y),
        //                                 tileSO = baseGroundTileSetting.tileSO
        //                             });
        //
        //                             break;
        //                         }
        //                     }
        //                 }
        //             }
        //         }
        //     }
        //
        //     void GeneratePatch()
        //     {
        //         foreach (PatchSetting patchSetting in CurrentTerrainSO.patchSettings)
        //         {
        //             //根据设置生成Patch.
        //             HashSet<Vector2Int> patch = new HashSet<Vector2Int>();
        //             int targetPatchSize = patchSetting.patchSizeRange.GetRandomValueInRange();
        //
        //             //openSet含有的点:满足Predicate,但待添加进patch并且评估其周围情况.
        //             Queue<Vector2Int> openSet = new Queue<Vector2Int>();
        //             //closedSet含有的点:评估过Predicate,失败了.
        //             List<Vector2Int> closedSet = new List<Vector2Int>();
        //             //closedSet含有的点:如果这一轮产生的Patch太小,需要提前存储下一轮能用的点.
        //             Queue<Vector2Int> openSetForNextPredicate = new Queue<Vector2Int>();
        //
        //             Vector2Int startCoord = leftNonPatchCoords.GetRandomElement();
        //             float startNoise = perlinNoise[startCoord.x, startCoord.y];
        //             int noiseRangeMultiplier = 1;
        //
        //             openSet.Enqueue(startCoord);
        //
        //             while (true)
        //             {
        //                 if (patch.Count >= targetPatchSize) break;
        //                 if (leftNonPatchCoords.Count == 0) break;
        //
        //                 Vector2 noiseRange = new Vector2(Mathf.Clamp(startNoise - 0.1f * noiseRangeMultiplier, 0, 1),
        //                     Mathf.Clamp(startNoise + 0.1f * noiseRangeMultiplier, 0, 1));
        //
        //                 while (true)
        //                 {
        //                     if (patch.Count >= targetPatchSize) break;
        //                     if (openSet.Count == 0) break;
        //
        //                     Vector2Int current = openSet.Dequeue();
        //                     patch.Add(current);
        //
        //                     foreach (Vector2Int surroundingCoord in current.GetCardinalDirections())
        //                     {
        //                         if (closedSet.Contains(surroundingCoord)) continue;
        //                         if (openSet.Contains(surroundingCoord)) continue;
        //                         if (patch.Contains(surroundingCoord)) continue;
        //
        //                         if (Predicate(surroundingCoord))
        //                         {
        //                             openSet.Enqueue(surroundingCoord);
        //                         }
        //                         else
        //                         {
        //                             closedSet.Add(surroundingCoord);
        //                         }
        //
        //                         if (NextPredicate(surroundingCoord))
        //                         {
        //                             openSetForNextPredicate.Enqueue(surroundingCoord);
        //                         }
        //                     }
        //                 }
        //
        //                 noiseRangeMultiplier++;
        //                 openSet = openSetForNextPredicate;
        //                 closedSet.Clear();
        //
        //                 bool Predicate(Vector2Int coord)
        //                 {
        //                     return coord.IsInRange(0, mapSize - 1, 0, mapSize - 1) && perlinNoise[coord.x, coord.y]
        //                         .IsInRange(noiseRange.x, noiseRange.y);
        //                 }
        //
        //                 bool NextPredicate(Vector2Int coord)
        //                 {
        //                     int nextMultiplier = noiseRangeMultiplier + 1;
        //                     Vector2 nextNoiseRange = new Vector2(Mathf.Clamp(startNoise - 0.2f * nextMultiplier, 0, 1),
        //                         Mathf.Clamp(startNoise + 0.2f * nextMultiplier, 0, 1));
        //                     return coord.IsInRange(0, mapSize - 1, 0, mapSize - 1) && perlinNoise[coord.x, coord.y]
        //                         .IsInRange(nextNoiseRange.x, nextNoiseRange.y);
        //                 }
        //             }
        //
        //             leftNonPatchCoords.ExceptWith(patch);
        //
        //             //添加Patch的数据到Ground中.
        //             foreach (Vector2Int coord in patch)
        //             {
        //                 _ground.tileDataList[coord.x * mapSize + coord.y].tileSO = patchSetting.tileSO;
        //             }
        //         }
        //     }
        // }
    }
}