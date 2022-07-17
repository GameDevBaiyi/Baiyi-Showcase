using BaiyiShowcase.Colonists.ColonistAI.Harvest;
using BaiyiShowcase.OrderSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Colonists.ColonistFSM
{
    public class Think : State_Colonist
    {
        [Required]
        [SerializeField] private FSM_Colonist _fsm;
        [Required]
        [SerializeField] private IdleBT _idleBT;
        [Required]
        [SerializeField] private ChopWoodBT _chopWoodBT;
        [Required]
        [SerializeField] private HarvestBT _harvestBT;

        [ShowInInspector]
        public bool DebugMode { get; set; }

        private OrderCollections _orderCollections;
        private OrderCollections OrderCollectionsProperty
        {
            get
            {
                if (_orderCollections == true)
                {
                    return _orderCollections;
                }
                else
                {
                    _orderCollections = OrderCollections.Instance;
                    return _orderCollections;
                }
            }
        }

        public override void OnEnterState()
        {
        }

        public override void OnUpdateState()
        {
            //OrderCollections是各种指令的合集. 有专门的查找工作之类的实现.
            if (OrderCollectionsProperty.TryLookUpOrder(OrderType.ChopWood, out Transform chopTarget))
            {
#if UNITY_EDITOR
                if (DebugMode)
                    Debug.Log("进入ChopWood状态");
#endif
                _chopWoodBT.InitializeChopWoodBT(chopTarget);
                _fsm.TransitionTo(_chopWoodBT);

                return;
            }

            if (OrderCollectionsProperty.TryLookUpOrder(OrderType.Harvest, out Transform harvestTarget))
            {
#if UNITY_EDITOR
                if (DebugMode)
                    Debug.Log("进入Harvest状态");
#endif
                _harvestBT.InitializeHarvestBT(harvestTarget);
                _fsm.TransitionTo(_harvestBT);

                return;
            }

            _fsm.TransitionTo(_idleBT);
#if UNITY_EDITOR
            if (DebugMode)
                Debug.Log("无事可做");
#endif
        }

        public override void OnExitState()
        {
        }
    }
}