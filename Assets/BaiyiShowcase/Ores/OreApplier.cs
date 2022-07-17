using BaiyiShowcase.Managers.GridManager;
using BaiyiShowcase.PathfindingSystem;
using UnityEngine;

namespace BaiyiShowcase.Ores
{
    public class OreApplier : MonoBehaviour
    {
        private GridSystem _gridSystem;
        private GridSystem GridSystemProperty
        {
            get
            {
                if (_gridSystem == true)
                {
                    return _gridSystem;
                }
                else
                {
                    _gridSystem = GridSystem.Instance;
                    return _gridSystem;
                }
            }
        }

        private void OnEnable()
        {
            PathfindingGridInitializer.endChangingWalkable?.Invoke(
                GridSystemProperty.WorldPositionToCoord(this.transform.position), false);
        }

        private void OnDisable()
        {
            PathfindingGridInitializer.endChangingWalkable?.Invoke(
                GridSystemProperty.WorldPositionToCoord(this.transform.position), true);
        }
    }
}