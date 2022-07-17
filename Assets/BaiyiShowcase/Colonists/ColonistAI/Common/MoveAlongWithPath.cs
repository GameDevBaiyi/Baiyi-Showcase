using System.Collections.Generic;
using BaiyiShowcase.BehaviourTree;
using BaiyiShowcase.Managers.ActionsManager;
using BaiyiShowcase.Managers.GridManager;
using UnityEngine;

namespace BaiyiShowcase.Colonists.ColonistFSM
{
    public class MoveAlongWithPath : Node
    {
        private IMoveAlongWithPath _current;

        public MoveAlongWithPath(IMoveAlongWithPath current)
        {
            _current = current;
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

        public override NodeState Evaluate()
        {
            if (_current.Waypoint == default ||
                Vector3.Distance(_current.Agent.position, _current.Waypoint) < 0.1f)
            {
                //当WaypointIndex==Path.Count时, 代表着已经到了导航点
                if (_current.WaypointIndex == _current.Path.Count)
                {
#if UNITY_EDITOR
                    if (_current.DebugMode)
                        Debug.Log("到达了导航点");
#endif

                    //检测目标物体是否在附近
                    if (Vector3.Distance(_current.Agent.position, _current.Target.position) <
                        GridSystemProperty.nodeSize + 0.2f)
                    {
#if UNITY_EDITOR
                        if (_current.DebugMode)
                            Debug.Log("到达了导航点, 且目标在附近");
#endif

                        return NodeState.Success;
                    }
                    else
                    {
#if UNITY_EDITOR
                        if (_current.DebugMode)
                            Debug.Log("到达了导航点, 但未找到目标");
#endif

                        _current.BackToThink();
                    }
                }

                _current.Waypoint = _current.Path[_current.WaypointIndex];
                _current.WaypointIndex++;
            }

            Vector3 direction = CalculateDirection();
            _current.Agent.localScale = CalculateFaceDirection();
            AnimationControllerProperty.Play(_current.AgentAnimator, AnimationType.Walk,
                ref _current.AgentFSM.currentAnimation);
            _current.Agent.Translate(direction * (2f * Time.deltaTime));

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

    public interface IMoveAlongWithPath
    {
        List<Vector2> Path { get; set; }
        Vector2 Waypoint { get; set; }
        Transform Agent { get; }
        Transform Target { get; }
        int WaypointIndex { get; set; }
        Animator AgentAnimator { get; }
        FSM AgentFSM { get; }
        bool DebugMode { get; set; }

        void BackToThink();
    }
}