using System.Collections.Generic;
using System.Linq;
using BaiyiShowcase.BehaviourTree;
using BaiyiShowcase.Managers.GridManager;
using BaiyiShowcase.OrderSystem;
using BaiyiShowcase.PathfindingSystem;
using BaiyiUtilities;
using UnityEngine;

namespace BaiyiShowcase.Colonists.ColonistFSM
{
    public class IfHasPath : Node
    {
        private readonly IIfHasPath _current;

        public IfHasPath(IIfHasPath current)
        {
            _current = current;
        }

        private AStarPathfinding _pathfinding;
        private AStarPathfinding Pathfinding
        {
            get
            {
                if (_pathfinding == true)
                {
                    return _pathfinding;
                }
                else
                {
                    _pathfinding = AStarPathfinding.Instance;
                    return _pathfinding;
                }
            }
        }

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

        private OrderCollections OrderCollectionsProperty => OrderCollections.Instance;


        public override NodeState Evaluate()
        {
            if (_current.Path.Count > 0)
            {
                return NodeState.Success;
            }
            else
            {
                if (TryGetPath())
                {
#if UNITY_EDITOR
                    if (_current.DebugMode)
                        Debug.Log("成功获得路径");
#endif

                    return NodeState.Success;
                }
                else
                {
#if UNITY_EDITOR
                    if (_current.DebugMode)
                        Debug.Log("无法到达目标点 " + _current.Target.name + "  " + _current.Target.position);
#endif

                    OrderCollectionsProperty.RemoveOrder(OrderType.ChopWood, _current.Target);
                    _current.BackToThink();
                    return NodeState.Failure;
                }
            }
        }

        private bool TryGetPath()
        {
            Vector2 currentPosition = _current.Agent.position;
            foreach (Vector2 nearbyPosition in GetDestination4D())
            {
                _current.Path = Pathfinding.GetPath(currentPosition, nearbyPosition);
                if (_current.Path.Count > 0)
                {
                    return true;
                }
            }

            return false;

            IEnumerable<Vector2> GetDestination4D()
            {
                return GridSystemProperty.WorldPositionToCoord(_current.Target.position).Get4D()
                    .Select(t => GridSystemProperty.CoordToWorldPosition(t))
                    .OrderBy(t => Vector3.Distance(t, _current.Agent.position));
            }
        }
    }

    public interface IIfHasPath
    {
        List<Vector2> Path { get; set; }
        Transform Agent { get; }
        Transform Target { get; }
        bool DebugMode { get; set; }

        void BackToThink();
    }
}