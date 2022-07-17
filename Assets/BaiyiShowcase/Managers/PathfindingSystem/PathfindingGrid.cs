using System.Collections.Generic;
using BaiyiShowcase.Managers.GridManager;
using BaiyiUtilities;
using BaiyiUtilities.Singleton;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.PathfindingSystem
{
    public class PathfindingGrid : Singleton<PathfindingGrid>
    {
        [Required]
        [SerializeField] private GridSystem _gridSystem;

        public PathfindingNode[,] grid;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.black;
            if (grid == null) return;

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (!grid[x, y].walkable)
                    {
                        Gizmos.DrawCube(_gridSystem.CoordToWorldPosition(new Vector2Int(x, y)), Vector3.one);
                    }
                }
            }
        }

        public PathfindingNode WorldPositionToNode(Vector2 worldPosition)
        {
            Vector2Int coord = _gridSystem.WorldPositionToCoord(worldPosition);
            return grid[coord.x, coord.y];
        }

        public IEnumerable<PathfindingNode> Get8DNeighbors(Vector2Int startCoord)
        {
            List<PathfindingNode> neighbors = new List<PathfindingNode>();
            int mapSize = _gridSystem.currentMapSize;
            foreach (Vector2Int neighborCoord in startCoord.Get8D())
            {
                if (!neighborCoord.IsInRange(0, mapSize - 1, 0, mapSize - 1)) continue;

                neighbors.Add(grid[neighborCoord.x, neighborCoord.y]);
            }

            return neighbors;
        }

        public int CalculateDistance(PathfindingNode node1, PathfindingNode node2)
        {
            int absXDiff = Mathf.Abs(node1.coord.x - node2.coord.x);
            int absYDiff = Mathf.Abs(node1.coord.y - node2.coord.y);
            int smallerAbs = Mathf.Min(absXDiff, absYDiff);
            int absOfDiffs = Mathf.Abs(absXDiff - absYDiff);
            return absOfDiffs * 10 + smallerAbs * 14;
        }
    }
}