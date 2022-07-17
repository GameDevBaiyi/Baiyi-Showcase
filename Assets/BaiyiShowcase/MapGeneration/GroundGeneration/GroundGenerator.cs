using System;
using System.Collections.Generic;
using System.Linq;
using BaiyiShowcase.GameDesign;
using BaiyiShowcase.GameDesign.Terrains;
using BaiyiShowcase.Managers.GridManager;
using BaiyiShowcase.NewGame;
using BaiyiShowcase.PathfindingSystem;
using BaiyiUtilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace BaiyiShowcase.MapGeneration.GroundGeneration
{
    public class GroundGenerator : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private Ground _ground;
        [Required]
        [SerializeField] private Transform _animatedTilesParent;
        [Required]
        [SerializeField] private GridSystem _gridSystem;
        [Required]
        [SerializeField] private Tilemap _nonAnimatedTilemap;

        public static event Action OnEndGeneratingGround;


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
            GroundInitializer.OnEndInitializingGround += GenerateGround;
        }

        private void OnDestroy()
        {
            GroundInitializer.OnEndInitializingGround -= GenerateGround;
        }

        [Button] //OnEndRestoringGround 在Ground数据恢复完, 调用此方法开始生成Ground.
        public void GenerateGround()
        {
            Random.InitState(Seed);
            TileSO[] notWalkableTiles = _gameDesignSO.groundGenerationDesign.notWalkableTiles;
            foreach (KeyValuePair<Vector2Int, TileSO> keyValuePair in _ground.groundDataDictionary)
            {
                if (keyValuePair.Value is AnimatedTileSO animatedTileSO)
                {
                    Instantiate(animatedTileSO.tiles.GetRandomElement(),
                        _gridSystem.CoordToWorldPosition(keyValuePair.Key), Quaternion.identity, _animatedTilesParent);
                }
                else if (keyValuePair.Value is NonAnimatedTileSO nonAnimatedTileSO)
                {
                    _nonAnimatedTilemap.SetTile((Vector3Int)keyValuePair.Key,
                        nonAnimatedTileSO.tiles.GetRandomElement());
                }

                //不能在上面的行走的Tile,比如水, 更新信息给Pathfinding.
                if (notWalkableTiles.Any(t => t == keyValuePair.Value))
                {
                    PathfindingGridInitializer.endChangingWalkable?.Invoke(keyValuePair.Key, false);
                }
            }

            OnEndGeneratingGround?.Invoke();
        }

        [Button]
        public void ClearGround()
        {
            _nonAnimatedTilemap.ClearAllTiles();

            while (true)
            {
                if (_animatedTilesParent.childCount == 0) break;

                DestroyImmediate(_animatedTilesParent.GetChild(0).gameObject);
            }
        }
    }
}