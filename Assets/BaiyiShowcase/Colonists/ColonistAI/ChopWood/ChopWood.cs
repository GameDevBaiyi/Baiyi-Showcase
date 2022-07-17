using BaiyiShowcase.BehaviourTree;
using BaiyiShowcase.Managers.ActionsManager;
using BaiyiShowcase.OrderSystem;
using BaiyiShowcase.Plants.Trees;
using UnityEngine;

namespace BaiyiShowcase.Colonists.ColonistFSM
{
    public class ChopWood : Node
    {
        private readonly IChopWood _current;

        public ChopWood(IChopWood current)
        {
            _current = current;
        }

     

        private TreeApplier _applier;
        private TreeApplier TreeApplier
        {
            get
            {
                if (_applier == true && _current.Target == _applier.transform)
                {
                    return _applier;
                }
                else
                {
                    _applier = _current.Target.GetComponent<TreeApplier>();
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
                Debug.Log("砍木头中");
#endif


            _current.ChopTimer += Time.deltaTime;
            if (_current.ChopTimer > _current.ChopInterval)
            {
                //GetChopped可由动画中的Event来激活, 斧头落下时再GetChopped.
                //20f是砍伐进度, 可以在GameDesignSO中设计基础工作速度和自身属性的算法. 
                TreeApplier.Chopped(20f);
                AnimationControllerProperty.Play(_current.AgentAnimator, AnimationType.Attack,
                    ref _current.AgentFSM.currentAnimation);
                _current.ChopTimer = 0f;
            }

            return NodeState.Success;
        }
    }

    public interface IChopWood
    {
        Transform Target { get; }
        bool DebugMode { get; set; }
        float ChopInterval { get; set; }
        float ChopTimer { get; set; }
        Animator AgentAnimator { get; }
        FSM AgentFSM { get; }
        void BackToThink();
    }
}