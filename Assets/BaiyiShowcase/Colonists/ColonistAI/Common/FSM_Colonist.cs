using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Colonists.ColonistFSM
{
    public class FSM_Colonist : FSM
    {
        [Required]
        [SerializeField] private Think _think;
        
        private State_Colonist _current;
        

        private void Start()
        {
            _current = _think;
        }

        private void Update()
        {
            _current.OnUpdateState();
        }

        public void TransitionTo(State_Colonist target)
        {
            _current.OnExitState();
            target.OnEnterState();
            _current = target;
        }
    }
}