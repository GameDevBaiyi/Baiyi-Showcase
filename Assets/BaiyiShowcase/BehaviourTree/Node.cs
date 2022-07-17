namespace BaiyiShowcase.BehaviourTree
{
    public abstract class Node
    {
        protected NodeState nodeState;

        public abstract NodeState Evaluate();
    }
}