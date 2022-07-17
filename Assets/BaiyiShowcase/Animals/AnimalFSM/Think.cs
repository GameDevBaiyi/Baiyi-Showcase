using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Animals.AnimalFSM
{
    //根据当前状态考虑做何事. 此处只是简单的随机选一件事.
    public class Think : State_Animal
    {
        [Required]
        [SerializeField] private FSM_Animal _fsm;
        [Required]
        [SerializeField] private Idle _idle;
        [Required]
        [SerializeField] private Walk _walk;
        [Required]
        [SerializeField] private Eat _eat;
        [Required]
        [SerializeField] private Sleep _sleep;

        public override void OnEnterState()
        {
        }

        public override void OnUpdateState()
        {
            int randomState = Random.Range(0, 4);
            switch (randomState)
            {
                case 0:
                    _fsm.TransitionTo(_idle);
                    break;
                case 1:
                    _fsm.TransitionTo(_walk);
                    break;
                case 2:
                    _fsm.TransitionTo(_eat);
                    break;
                case 3:
                    _fsm.TransitionTo(_sleep);
                    break;
            }
        }

        public override void OnExitState()
        {
        }


        [Button]
        private void Reset()
        {
            _fsm = this.GetComponent<FSM_Animal>();
            _idle = this.GetComponent<Idle>();
            _walk = this.GetComponent<Walk>();
            _eat = this.GetComponent<Eat>();
            _sleep = this.GetComponent<Sleep>();
        }
    }
}