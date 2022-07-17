using BaiyiShowcase.Managers.ActionsManager;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Animals.AnimalFSM
{
    public class Eat : State_Animal
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
        private int _eatCode;
        private float _eatInterval = 3f;

        private float _maxEatTime;
        private float _timer;
        private float _animationTimer;

        public override void OnEnterState()
        {
            _maxEatTime = Random.Range(_animalSO.maxEatTimeRange.x, _animalSO.maxEatTimeRange.y);
            _timer = 0f;
            _animationTimer = 0f;
        }

        public override void OnUpdateState()
        {
            if (TryTransitionToThink()) return;

            DoEat();

            bool TryTransitionToThink()
            {
                _timer += Time.deltaTime;
                if (_timer > _maxEatTime)
                {
                    _fsm.TransitionTo(_think);
                    _timer = 0f;
                    return true;
                }

                return false;
            }

            void DoEat()
            {
                _animationTimer += Time.deltaTime;
                if (_animationTimer > _eatInterval)
                {
                    AnimationController.Play(_animator, AnimationType.Attack, ref _fsm.currentAnimation);
                    _animationTimer = 0f;
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