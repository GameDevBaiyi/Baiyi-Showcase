using System.Collections.Generic;
using BaiyiShowcase.BehaviourTree;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Colonists.ColonistFSM
{
    public class ChopWoodBT : State_Colonist, IIfTargetExists, IIfHasPath, IMoveAlongWithPath, IChopWood
    {
        [Required]
        [SerializeField] private Animator _animator;
        [Required]
        [SerializeField] private FSM_Colonist _fsm;
        [Required]
        [SerializeField] private Think _think;
        [Required]
        [SerializeField] private Transform _agent;

        private Node _topNode;

        public List<Vector2> Path { get; set; }
        public Vector2 Waypoint { get; set; }
        public Transform Agent => _agent;
        public Transform Target { get; private set; }

        public int WaypointIndex { get; set; }

        public Animator AgentAnimator => _animator;
        public FSM AgentFSM => _fsm;

        public float ChopTimer { get; set; }
        public float ChopInterval { get; set; }

        [ShowInInspector]
        public bool DebugMode { get; set; }


        private void Start()
        {
            BuildTree();
        }

        private void BuildTree()
        {
            IfTargetExists ifTargetExists = new IfTargetExists(this);

            IfHasPath ifHasPath = new IfHasPath(this);
            MoveAlongWithPath moveAlongWithPath = new MoveAlongWithPath(this);
            Sequence getPathAndMove = new Sequence(new List<Node>() { ifHasPath, moveAlongWithPath });

            ChopWood chopWood = new ChopWood(this);
            Sequence moveToChop = new Sequence(new List<Node>() { getPathAndMove, chopWood });

            _topNode = new Sequence(new List<Node>()
                { ifTargetExists, moveToChop });
        }

        public override void OnEnterState()
        {
            Path = new List<Vector2>();
            Waypoint = default;
            WaypointIndex = 0;
            ChopTimer = 0f;
            ChopInterval = 1f;
        }

        public override void OnUpdateState()
        {
            _topNode.Evaluate();
        }

        public override void OnExitState()
        {
        }

        public void InitializeChopWoodBT(Transform target)
        {
            Target = target;
        }

        public void BackToThink()
        {
            _fsm.TransitionTo(_think);
#if UNITY_EDITOR
            if (DebugMode)
                Debug.Log("返回到Think状态");
#endif
        }
    }
}