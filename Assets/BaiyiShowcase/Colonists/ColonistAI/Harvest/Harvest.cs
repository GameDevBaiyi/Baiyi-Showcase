using BaiyiShowcase.BehaviourTree;
using BaiyiShowcase.Colonists.ColonistFSM;
using BaiyiShowcase.Managers.ActionsManager;
using BaiyiShowcase.Plants.Crops;
using UnityEngine;

namespace BaiyiShowcase.Colonists.ColonistAI.Harvest
{
    public class Harvest : Node
    {
        private readonly IHarvest _current;

        public Harvest(IHarvest current)
        {
            _current = current;
        }


        private CropApplier _applier;
        private CropApplier CropApplierProperty
        {
            get
            {
                if (_applier == true && _current.Target == _applier.transform)
                {
                    return _applier;
                }
                else
                {
                    _applier = _current.Target.GetComponent<CropApplier>();
                    return _applier;
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
#if UNITY_EDITOR
            if (_current.DebugMode)
                Debug.Log("收获中");
#endif


            _current.HarvestTimer += Time.deltaTime;
            if (_current.HarvestTimer > _current.HarvestInterval)
            {
                //可由动画中的Event来激活, 斧头落下时再添加进度.
                //20f是砍伐进度, 可以在GameDesignSO中设计基础工作速度和自身属性的算法. 
                CropApplierProperty.Harvested(20f);
                AnimationControllerProperty.Play(_current.AgentAnimator, AnimationType.Attack,
                    ref _current.AgentFSM.currentAnimation);
                _current.HarvestTimer = 0f;
            }

            return NodeState.Success;
        }
    }

    public interface IHarvest
    {
        Transform Target { get; }
        bool DebugMode { get; set; }
        float HarvestInterval { get; set; }
        float HarvestTimer { get; set; }
        Animator AgentAnimator { get; }
        FSM AgentFSM { get; }
        void BackToThink();
    }
}