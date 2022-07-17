using BaiyiShowcase.Managers.ActionsManager;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Animals.AnimalFSM
{
    public class Sleep : State_Animal
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

        private float _maxSleepTime;
        private float _timer;

        public override void OnEnterState()
        {
            _maxSleepTime = Random.Range(_animalSO.maxEatTimeRange.x, _animalSO.maxEatTimeRange.y);
            _timer = 0f;
            AnimationController.Play(_animator, AnimationType.Death, ref _fsm.currentAnimation);
        }

        public override void OnUpdateState()
        {
            TryTransitionToThink();

            void TryTransitionToThink()
            {
                _timer += Time.deltaTime;
                if (_timer > _maxSleepTime)
                {
                    _fsm.TransitionTo(_think);
                    _timer = 0f;
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