using UnityEngine;

namespace BaiyiShowcase.Animals.AnimalFSM
{
    public abstract class State_Animal : MonoBehaviour
    {
        public abstract void OnEnterState();
        public abstract void OnUpdateState();
        public abstract void OnExitState();
    }
}