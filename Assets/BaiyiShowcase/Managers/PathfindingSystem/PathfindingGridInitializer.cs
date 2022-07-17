using System;
using BaiyiShowcase.Managers.GridManager;
using BaiyiShowcase.SaveLoadSystem.LoadManage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.PathfindingSystem
{
    public class PathfindingGridInitializer : MonoBehaviour
    {
        [Required]
        [SerializeField] private PathfindingGrid _grid;
        [Required]
        [SerializeField] private GridSystem _gridSystem;


        public static Action<Vector2Int, bool> endChangingWalkable;

        public static event Action OnEndInitializingPathfinding;

        private bool IsNewScene => LoadManager.Instance.IsNewScene;

        private void Awake()
        {
            GridSystemInitializer.OnEndInitializingGridSystem += InitializeGrid;
            endChangingWalkable += UpdateGrid;
        }

        private void OnDestroy()
        {
            GridSystemInitializer.OnEndInitializingGridSystem -= InitializeGrid;
            endChangingWalkable -= UpdateGrid;
        }

        private void InitializeGrid()
        {
            int mapSize = _gridSystem.currentMapSize;
            _grid.grid = new PathfindingNode[mapSize, mapSize];
            for (int x = 0; x < mapSize; x++)
            {
                for (int y = 0; y < mapSize; y++)
                {
                    _grid.grid[x, y] = new PathfindingNode()
                    {
                        coord = new Vector2Int(x, y),
                        walkable = true
                    };
                }
            }

            OnEndInitializingPathfinding?.Invoke();
        }

        private void UpdateGrid(Vector2Int coord, bool walkable)
        {
            _grid.grid[coord.x, coord.y].walkable = walkable;
        }
    }
}