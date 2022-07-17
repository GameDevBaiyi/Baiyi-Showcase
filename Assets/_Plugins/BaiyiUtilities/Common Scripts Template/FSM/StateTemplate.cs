using UnityEngine;

namespace BaiyiUtilities.FSM
{
    public abstract class StateTemplate : MonoBehaviour
    {
        public abstract void OnEnterState();
        public abstract void OnUpdateState();
        public abstract void OnExitState();
    }
}