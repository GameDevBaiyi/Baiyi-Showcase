using System.Collections.Generic;
using BaiyiShowcase.Managers.ActionsManager;
using BaiyiShowcase.PathfindingSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BaiyiShowcase.Animals.AnimalFSM
{
    //动物没法用一个Prefab, 所以很多引用需要由代码去实现
    public class Walk : State_Animal
    {
        [Required]
        [SerializeField] private FSM_Animal _fsm;
        [Required]
        [SerializeField] private Think _think;

        private Transform _transform;
        private Animator _animator;
        private AnimalSO _animalSO;
        private float _moveRadius;
        private float _moveSpeed;
        private int _walkCode;

        private float _maxWalkTime;
        private float _timer;
        private Vector2 _waypoint;
        private int _waypointIndex;
        private List<Vector2> _path = new List<Vector2>();

        private AnimationController _animationController;
        public AnimationController AnimationControllerProperty
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

        private AStarPathfinding _pathfinding;
        private AStarPathfinding Pathfinding
        {
            get
            {
                if (_pathfinding == true)
                {
                    return _pathfinding;
                }
                else
                {
                    _pathfinding = AStarPathfinding.Instance;
                    return _pathfinding;
                }
            }
        }

        public override void OnEnterState()
        {
            _maxWalkTime = Random.Range(_animalSO.maxWalkTimeRange.x, _animalSO.maxWalkTimeRange.y);
            _timer = 0f;
            _waypoint = default;
            _waypointIndex = 0;
            _path = new List<Vector2>();
        }

        public override void OnUpdateState()
        {
            if (TryTransitionToThink()) return;

            RandomWalk();

            bool TryTransitionToThink()
            {
                _maxWalkTime += Time.deltaTime;
                if (_timer > _maxWalkTime)
                {
                    _fsm.TransitionTo(_think);
                    return true;
                }

                return false;
            }

            void RandomWalk()
            {
                if (_path.Count == 0)
                {
                    _path = Pathfinding.GetRandomPath(_transform.position, (int)_moveRadius);
                }

                if (_waypoint == default ||
                    Vector3.Distance(_transform.position, _waypoint) < 0.1f)
                {
                    //当WaypointIndex==Path.Count时, 代表着已经到了导航点
                    if (_waypointIndex == _path.Count)
                    {
                        //Walk的路线.
                        _waypoint = default;
                        _waypointIndex = 0;
                        _path = new List<Vector2>();
                        
                        return;
                    }

                    _waypoint = _path[_waypointIndex];
                    _waypointIndex++;
                }

                Vector3 direction = CalculateDirection();
                _transform.localScale = CalculateFaceDirection();
                AnimationControllerProperty.Play(_animator, AnimationType.Walk,
                    ref _fsm.currentAnimation);
                _transform.Translate(direction * (_moveSpeed * Time.deltaTime));

                Vector3 CalculateDirection()
                {
                    return ((Vector3)_waypoint - _transform.position).normalized;
                }

                Vector3 CalculateFaceDirection()
                {
                    float angle = Vector3.Angle(Vector3.right, direction);
                    return angle is > 90 and < 270 ? Vector3.one : new Vector3(-1, 1, 1);
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

        public void Initialize(Animator animator, Transform transformParameter, AnimalSO animalSO,
            float moveRadius, float moveSpeed)
        {
            _animator = animator;
            _animalSO = animalSO;
            _moveRadius = moveRadius;
            _moveSpeed = moveSpeed;
            _transform = transformParameter;
        }
    }
}