using System.Collections.Generic;
using BaiyiShowcase.GameDesign;
using BaiyiUtilities.Singleton;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Managers.ActionsManager
{
    public class AnimationController : Singleton<AnimationController>
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;

        private Dictionary<AnimationType, int> _animationDictionary = new Dictionary<AnimationType, int>();

        protected override void Awake()
        {
            base.Awake();

            if (Instance != this) return;

            _animationDictionary[AnimationType.Idle] = _gameDesignSO.animalsDesign.IdleCode;
            _animationDictionary[AnimationType.Walk] = _gameDesignSO.animalsDesign.WalkCode;
            _animationDictionary[AnimationType.Attack] = _gameDesignSO.animalsDesign.AttackCode;
            _animationDictionary[AnimationType.Death] = _gameDesignSO.animalsDesign.DeathCode;
        }

        public void Play(Animator animator, AnimationType animationType, ref AnimationType currentType)
        {
            switch (animationType)
            {
                case AnimationType.Idle:
                    if (currentType == AnimationType.Idle) return;
                    animator.CrossFade(_animationDictionary[AnimationType.Idle], 0.5f);
                    currentType = AnimationType.Idle;
                    break;
                case AnimationType.Walk:
                    if (currentType == AnimationType.Walk) return;
                    animator.CrossFade(_animationDictionary[AnimationType.Walk], 0.5f);
                    currentType = AnimationType.Walk;
                    break;
                case AnimationType.Attack:
                    animator.Play(_animationDictionary[AnimationType.Attack]);
                    break;
                case AnimationType.Death:
                    if (currentType == AnimationType.Death) return;
                    animator.CrossFade(_animationDictionary[AnimationType.Death], 0.5f);
                    currentType = AnimationType.Death;
                    break;
            }
        }
    }

    public enum AnimationType
    {
        Idle,
        Walk,
        Attack,
        Death
    }
}