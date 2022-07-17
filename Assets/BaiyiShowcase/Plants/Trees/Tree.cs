using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace BaiyiShowcase.Plants.Trees
{
    public class Tree : Plant
    {
        public TreeSO treeSO;

        [Required]
        public SpriteRenderer spriteRenderer;
        [Required]
        public Animator animator;

        [FormerlySerializedAs("treeSaveDataHandler")]
        [Required]
        public TreeSaveDataHandler saveDataHandler;

        //设置一个工作量, 此处只是一个Demo, 不做更多的植物生长和产量设计了.
        public float chopWorkLoad = 100f;

        private float _chopProgress;
        [ShowInInspector]
        public float ChopProgress
        {
            get => _chopProgress;
            set
            {
                _chopProgress = value;
                OnEndChopping?.Invoke(_chopProgress);
            }
        }

        public event Action<float> OnEndChopping;
    }
}