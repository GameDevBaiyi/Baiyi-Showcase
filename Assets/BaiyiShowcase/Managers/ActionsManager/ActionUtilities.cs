using System.Collections.Generic;
using System.Linq;
using BaiyiShowcase.Colonists.ColonistFSM;
using BaiyiUtilities;
using BaiyiUtilities.Singleton;
using UnityEngine;
using BaiyiShowcase.Managers.GridManager;
using BaiyiShowcase.PathfindingSystem;

namespace BaiyiShowcase.Managers.ActionsManager
{
    public class ActionUtilities : Singleton<ActionUtilities>
    {
        private AnimationController _animationController;
        public AnimationController AnimationControllerProperty
        {
            get
            {
                if (_animationController == true)
                {
                    return _animationController;
                }
                else
                {
                    return AnimationController.Instance;
                }
            }
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

        public bool TryMoveTo(ref bool hasPath, ref bool hasWaypoint, List<Vector2> path, ref Vector2 wayPoint,
            Transform mover, Vector2 destination, Animator animator, FSM_Colonist fsm, out bool canFindAPath)
        {
            if (!hasPath)
            {
                if (TryGetPath(ref hasPath))
                {
                    hasPath = true;
                    canFindAPath = true;
                }
                else
                {
                    canFindAPath = false;
                }

                return false;
            }

            canFindAPath = true;

            if (!hasWaypoint)
            {
                if (!TryGetWaypoint(ref hasWaypoint, ref wayPoint)) return true;
                return false;
            }

            //Move.
            MoveToWaypoint(ref hasWaypoint, ref wayPoint);
            return false;

            bool TryGetPath(ref bool hasPath)
            {
                Vector2 currentPosition = mover.position;
                foreach (Vector2 nearbyPosition in GetDestination4D())
                {
                    path = Pathfinding.GetPath(currentPosition, nearbyPosition);
                    if (path.Count > 0)
                    {
                        hasPath = true;
                        return true;
                    }
                }

                return false;

                IEnumerable<Vector2> GetDestination4D()
                {
                    return GridSystemProperty.WorldPositionToCoord(destination).Get4D()
                        .Select(t => GridSystemProperty.CoordToWorldPosition(t))
                        .OrderBy(t => Vector3.Distance(t, mover.position));
                }
            }

            bool TryGetWaypoint(ref bool hasWaypoint, ref Vector2 wayPoint)
            {
                if (path.Count > 0)
                {
                    // wayPoint = path.Dequeue();
                    hasWaypoint = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            void MoveToWaypoint(ref bool hasWaypoint, ref Vector2 wayPoint)
            {
                Vector3 direction = CalculateDirection(ref wayPoint);
                mover.localScale = CalculateFaceDirection();
                AnimationControllerProperty.Play(animator, AnimationType.Walk, ref fsm.currentAnimation);
                mover.Translate(direction * (2f * Time.deltaTime));

                if (Vector3.Distance(mover.position, wayPoint) < 0.1f)
                {
                    hasWaypoint = false;
                }

                Vector3 CalculateDirection(ref Vector2 wayPoint)
                {
                    return ((Vector3)wayPoint - mover.position).normalized;
                }

                Vector3 CalculateFaceDirection()
                {
                    float angle = Vector3.Angle(Vector3.right, direction);
                    return angle is > 90 and < 270 ? Vector3.one : new Vector3(-1, 1, 1);
                }
            }
        }
    }
}