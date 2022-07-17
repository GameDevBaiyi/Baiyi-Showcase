using System.Collections.Generic;

namespace BaiyiShowcase.BehaviourTree
{
    public class Sequence : Node
    {
        private readonly List<Node> _children;

        public Sequence(List<Node> children)
        {
            this._children = children;
        }

        public override NodeState Evaluate()
        {
            bool isAnyChildRunning = false;
            foreach (Node child in _children)
            {
                switch (child.Evaluate())
                {
                    case NodeState.Running:
                        isAnyChildRunning = true;
                        break;
                    case NodeState.Success:
                        break;
                    case NodeState.Failure:
                        nodeState = NodeState.Failure;
                        return nodeState;
                }
            }

            nodeState = isAnyChildRunning ? NodeState.Running : NodeState.Success;
            return nodeState;
        }
    }
}