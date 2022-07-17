namespace BaiyiUtilities.BehaviourTree
{
    public abstract class NodeTemplate
    {
        protected NodeStateTemplate nodeState;

        public abstract NodeStateTemplate Evaluate();
    }
}