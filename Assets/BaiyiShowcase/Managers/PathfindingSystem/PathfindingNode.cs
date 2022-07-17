using UnityEngine;

namespace BaiyiShowcase.PathfindingSystem
{
    public class PathfindingNode
    {
        public Vector2Int coord;
        public bool walkable;
        public int gCost;
        public int hCost;
        public int FCost => gCost + hCost;
        public PathfindingNode parent;
    }
}