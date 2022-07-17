using BaiyiShowcase.BehaviourTree;
using UnityEngine;

namespace BaiyiShowcase.Colonists.ColonistFSM
{
    public class IfTargetExists : Node
    {
        private IIfTargetExists _current;

        public IfTargetExists(IIfTargetExists current)
        {
            _current = current;
        }

        public override NodeState Evaluate()
        {
            if (_current.Target == true)
            {
#if UNITY_EDITOR
                if (_current.DebugMode)
                    Debug.Log("目标存在");
#endif

                return NodeState.Success;
            }
            else
            {
#if UNITY_EDITOR
                if (_current.DebugMode)
                    Debug.Log("正要去砍的树不见了");
#endif
               
                _current.BackToThink();
                return NodeState.Failure;
            }
        }
    }

    public interface IIfTargetExists
    {
        Transform Target { get; }
        bool DebugMode { get; set; }
        void BackToThink();
    }
}