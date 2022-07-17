using BaiyiUtilities.Singleton;
using UnityEngine;

namespace BaiyiShowcase.Managers.GridManager
{
    public class GridSystem : Singleton<GridSystem>
    {
        public int currentMapSize;
        public float nodeSize;
        private float HalfNodeSize => nodeSize / 2f;


        public Vector2 CoordToWorldPosition(Vector2Int coord)
        {
            return new Vector2(coord.x + HalfNodeSize, coord.y + HalfNodeSize);
        }

        public Vector2Int WorldPositionToCoord(Vector3 worldPosition)
        {
            float mapBound = (currentMapSize - 1) * nodeSize;
            Vector3 worldPositionInMap =
                new Vector3(Mathf.Clamp(worldPosition.x, 0f, mapBound),
                    Mathf.Clamp(worldPosition.y, 0f, mapBound));

            return new Vector2Int(Mathf.FloorToInt(worldPositionInMap.x / nodeSize),
                Mathf.FloorToInt(worldPositionInMap.y / nodeSize));
        }

        public Vector2 ClampWorldPositionToMap(Vector2 worldPosition)
        {
            return new Vector2(Mathf.Clamp(worldPosition.x, 0f, currentMapSize * nodeSize),
                Mathf.Clamp(worldPosition.y, 0f, currentMapSize * nodeSize));
        }
    }
}