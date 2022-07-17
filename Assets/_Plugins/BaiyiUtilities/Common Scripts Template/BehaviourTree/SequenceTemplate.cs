using System.Collections.Generic;

namespace BaiyiUtilities.BehaviourTree
{
    public class SequenceTemplate : NodeTemplate
    {
        private readonly List<NodeTemplate> _children;

        public SequenceTemplate(List<NodeTemplate> children)
        {
            this._children = children;
        }

        public override NodeStateTemplate Evaluate()
        {
            bool isAnyChildRunning = false;
            foreach (NodeTemplate child in _children)
            {
                switch (child.Evaluate())
                {
                    case NodeStateTemplate.Running:
                        isAnyChildRunning = true;
                        break;
                    case NodeStateTemplate.Success:
                        break;
                    case NodeStateTemplate.Failure:
                        nodeState = NodeStateTemplate.Failure;
                        return nodeState;
                }
            }

            nodeState = isAnyChildRunning ? NodeStateTemplate.Running : NodeStateTemplate.Success;
            return nodeState;
        }
    }
}