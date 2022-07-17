using System.Collections.Generic;
using BaiyiShowcase.BehaviourTree;
using BaiyiShowcase.Managers.ActionsManager;
using BaiyiShowcase.PathfindingSystem;
using UnityEngine;

namespace BaiyiShowcase.Colonists.ColonistFSM
{
    public class RandomWalk : Node
    {
        private readonly IRandomWalk _current;


        public RandomWalk(IRandomWalk current)
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

        public override NodeState Evaluate()
        {
            if (_current.Path.Count == 0)
            {
                _current.Path = Pathfinding.GetRandomPath(_current.Agent.position, 10);
            }

            if (_current.Waypoint == default ||
                Vector3.Distance(_current.Agent.position, _current.Waypoint) < 0.1f)
            {
                //当WaypointIndex==Path.Count时, 代表着已经到了导航点
                if (_current.WaypointIndex == _current.Path.Count)
                {
#if UNITY_EDITOR
                    if (_current.DebugMode)
                        Debug.Log("到达了随机路线的终点");
#endif

                    //重置Idle和RandomWalk的状态.
                    _current.IdleTimer = 0f;
                    _current.Path = new List<Vector2>();
                    _current.Waypoint = default;
                    _current.WaypointIndex = 0;

                    return NodeState.Running;
                }

                _current.Waypoint = _current.Path[_current.WaypointIndex];
                _current.WaypointIndex++;
            }

            Vector3 direction = CalculateDirection();
            _current.Agent.localScale = CalculateFaceDirection();
            AnimationControllerProperty.Play(_current.AgentAnimator, AnimationType.Walk,
                ref _current.AgentFSM.currentAnimation);
            _current.Agent.Translate(direction * (1.5f * Time.deltaTime));

#if UNITY_EDITOR
            if (_current.DebugMode)
                Debug.Log("未到达, 移动中");
#endif

            return NodeState.Failure;

            Vector3 CalculateDirection()
            {
                return ((Vector3)_current.Waypoint - _current.Agent.position).normalized;
            }

            Vector3 CalculateFaceDirection()
            {
                float angle = Vector3.Angle(Vector3.right, direction);
                return angle is > 90 and < 270 ? Vector3.one : new Vector3(-1, 1, 1);
            }
        }
    }

    public interface IRandomWalk
    {
        List<Vector2> Path { get; set; }
        Vector2 Waypoint { get; set; }
        Transform Agent { get; }
        int WaypointIndex { get; set; }
        Animator AgentAnimator { get; }
        FSM AgentFSM { get; }
        bool DebugMode { get; set; }
        float IdleTimer { get; set; }
    }
}