using BaiyiShowcase.BehaviourTree;
using BaiyiShowcase.Managers.ActionsManager;
using UnityEngine;

namespace BaiyiShowcase.Colonists.ColonistFSM
{
    public class Idle : Node
    {
        private readonly IIdle _current;

        public Idle(IIdle current)
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

        public override NodeState Evaluate()
        {
            _current.IdleTimer += Time.deltaTime;
            if (_current.IdleTimer > _current.MaxIdleTime)
            {
                return NodeState.Failure;
            }

            AnimationControllerProperty.Play(_current.AgentAnimator, AnimationType.Idle,
                ref _current.AgentFSM.currentAnimation);
            return NodeState.Running;
        }
    }

    public interface IIdle
    {
        float MaxIdleTime { get; set; }
        float IdleTimer { get; set; }
        Animator AgentAnimator { get; }
        FSM AgentFSM { get; }
    }
}