using System.Collections.Generic;

namespace BaiyiShowcase.BehaviourTree
{
    public class Selector : Node
    {
        private readonly List<Node> _children;

        public Selector(List<Node> children)
        {
            this._children = children;
        }

        public override NodeState Evaluate()
        {
            foreach (Node child in _children)
            {
                switch (child.Evaluate())
                {
                    case NodeState.Running:
                        nodeState = NodeState.Running;
                        return nodeState;
                    case NodeState.Success:
                        nodeState = NodeState.Success;
                        return nodeState;
                    case NodeState.Failure:
                        break;
                }
            }

            nodeState = NodeState.Failure;
            return nodeState;
        }
    }
}