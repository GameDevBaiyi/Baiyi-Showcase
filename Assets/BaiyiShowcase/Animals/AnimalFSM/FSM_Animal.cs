using BaiyiShowcase.Managers.ActionsManager;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Animals.AnimalFSM
{
    public class FSM_Animal : MonoBehaviour
    {
        [Required]
        [SerializeField] private Think _think;
        
        private State_Animal _current;
        public AnimationType currentAnimation;

        private void Start()
        {
            _current = _think;
        }

        private void Update()
        {
            _current.OnUpdateState();
        }

        public void TransitionTo(State_Animal target)
        {
            _current.OnExitState();
            target.OnEnterState();
            _current = target;
        }
    }
}