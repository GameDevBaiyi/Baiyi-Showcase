using System.Collections.Generic;
using BaiyiShowcase.BehaviourTree;
using BaiyiShowcase.Colonists.ColonistFSM;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Colonists.ColonistAI.Harvest
{
    public class HarvestBT : State_Colonist, IIfTargetExists, IIfHasPath, IMoveAlongWithPath, IHarvest
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

        public float HarvestTimer { get; set; }
        public Animator AgentAnimator => _animator;
        public FSM AgentFSM => _fsm;

        [ShowInInspector]
        public bool DebugMode { get; set; }
        public float HarvestInterval { get; set; }


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

            Harvest harvest = new Harvest(this);
            Sequence moveToHarvest = new Sequence(new List<Node>() { getPathAndMove, harvest });

            _topNode = new Sequence(new List<Node>()
                { ifTargetExists, moveToHarvest });
        }

        public override void OnEnterState()
        {
            Path = new List<Vector2>();
            Waypoint = default;
            WaypointIndex = 0;
            HarvestTimer = 0f;
            HarvestInterval = 1f;
        }

        public override void OnUpdateState()
        {
            _topNode.Evaluate();
        }

        public override void OnExitState()
        {
        }

        public void InitializeHarvestBT(Transform target)
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