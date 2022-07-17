using BaiyiShowcase.Managers.ActionsManager;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Animals.AnimalFSM
{
    public class Idle : State_Animal
    {
        [Required]
        [SerializeField] private FSM_Animal _fsm;
        [Required]
        [SerializeField] private Think _think;

        private AnimationController _animationController;
        public AnimationController AnimationController
        {
            get
            {
                if (_animationController == true)
                {
                    return _animationController;
                }
                else
                {
                    return AnimationController.Instance;
                }
            }
        }

        private Animator _animator;
        private AnimalSO _animalSO;
        private int _idleCode;

        private float _maxIdleTime;
        private float _timer;

        public override void OnEnterState()
        {
            _maxIdleTime = Random.Range(_animalSO.maxIdleTimeRange.x, _animalSO.maxIdleTimeRange.y);
            _timer = 0f;
            AnimationController.Play(_animator, AnimationType.Idle, ref _fsm.currentAnimation);
        }

        public override void OnUpdateState()
        {
            TryTransitionToThink();

            void TryTransitionToThink()
            {
                _timer += Time.deltaTime;
                if (_timer > _maxIdleTime)
                {
                    _fsm.TransitionTo(_think);
                }
            }
        }

        public override void OnExitState()
        {
        }

        [Button]
        private void Reset()
        {
            _fsm = this.GetComponent<FSM_Animal>();
            _think = this.GetComponent<Think>();
        }

        public void Initialize(Animator animator, AnimalSO animalSO)
        {
            _animator = animator;
            _animalSO = animalSO;
        }
    }
}