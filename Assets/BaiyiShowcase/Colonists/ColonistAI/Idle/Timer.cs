using BaiyiShowcase.BehaviourTree;
using UnityEngine;

namespace BaiyiShowcase.Colonists.ColonistFSM
{
    public class Timer : Node
    {
        private readonly ITimer _current;

        public Timer(ITimer current)
        {
            _current = current;
        }

    

        public override NodeState Evaluate()
        {
            _current.BTTimer += Time.deltaTime;
            if (_current.BTTimer > _current.MaxBTTime)
            {
                _current.BackToThink();
                return NodeState.Failure;
            }

            return NodeState.Success;
        }
    }

    public interface ITimer
    {
        float MaxBTTime { get; set; }
        float BTTimer { get; set; }
  
        void BackToThink();
    }
}