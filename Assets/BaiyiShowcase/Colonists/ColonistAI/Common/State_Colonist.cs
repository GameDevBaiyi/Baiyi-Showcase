using UnityEngine;

namespace BaiyiShowcase.Colonists.ColonistFSM
{
    public abstract class State_Colonist : MonoBehaviour
    {
        public abstract void OnEnterState();
        public abstract void OnUpdateState();
        public abstract void OnExitState();
    }
}