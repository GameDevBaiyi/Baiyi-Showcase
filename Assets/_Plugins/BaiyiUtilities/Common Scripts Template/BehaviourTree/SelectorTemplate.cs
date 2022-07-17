using System.Collections.Generic;

namespace BaiyiUtilities.BehaviourTree
{
    public class SelectorTemplate : NodeTemplate
    {
        private readonly List<NodeTemplate> _children;

        public SelectorTemplate(List<NodeTemplate> children)
        {
            this._children = children;
        }

        public override NodeStateTemplate Evaluate()
        {
            foreach (NodeTemplate child in _children)
            {
                switch (child.Evaluate())
                {
                    case NodeStateTemplate.Running:
                        nodeState = NodeStateTemplate.Running;
                        return nodeState;
                    case NodeStateTemplate.Success:
                        nodeState = NodeStateTemplate.Success;
                        return nodeState;
                    case NodeStateTemplate.Failure:
                        break;
                }
            }

            nodeState = NodeStateTemplate.Failure;
            return nodeState;
        }
    }
}