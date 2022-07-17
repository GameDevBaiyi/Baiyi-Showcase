using System.Collections.Generic;
using BaiyiShowcase.BehaviourTree;
using BaiyiShowcase.GameDesign;
using BaiyiUtilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Colonists.ColonistFSM
{
    public class IdleBT : State_Colonist, ITimer, IIdle, IRandomWalk
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private Animator _animator;
        [Required]
        [SerializeField] private FSM_Colonist _fsm;
        [Required]
        [SerializeField] private Think _think;
        [Required]
        [SerializeField] private Transform _agent;

        private Node _topNode;

        public float MaxBTTime { get; set; }
        public float BTTimer { get; set; }
        public float MaxIdleTime { get; set; }
        public float IdleTimer { get; set; }
        public List<Vector2> Path { get; set; }
        public Vector2 Waypoint { get; set; }
        public Transform Agent => _agent;
        public int WaypointIndex { get; set; }
        public Animator AgentAnimator => _animator;
        public FSM AgentFSM => _fsm;
        public bool DebugMode { get; set; }

        private void Start()
        {
            BuildTree();
        }

        private void BuildTree()
        {
            Timer timer = new Timer(this);
            Idle idle = new Idle(this);
            RandomWalk randomWalk = new RandomWalk(this);

            Selector idleAndMove = new Selector(new List<Node>() { idle, randomWalk });

            _topNode = new Sequence(new List<Node>() { timer, idleAndMove });
        }

        public override void OnEnterState()
        {
            MaxBTTime = _gameDesignSO.colonistsDesign.idleTimeRange.GetRandomValueInRange();
            BTTimer = 0f;
            MaxIdleTime = 2f;
            IdleTimer = 0f;
            Path = new List<Vector2>();
            Waypoint = default;
            WaypointIndex = 0;
        }

        public override void OnUpdateState()
        {
            _topNode.Evaluate();
        }

        public override void OnExitState()
        {
        }


        public void BackToThink()
        {
            _fsm.TransitionTo(_think);
        }
    }
}