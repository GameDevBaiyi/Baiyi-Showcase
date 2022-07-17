using System;
using BaiyiShowcase.Animals.AnimalFSM;
using BaiyiShowcase.GameDesign;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Animals
{
    public class AnimalSaveDataHandler : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private Animal _animal;
        [Required]
        [SerializeField] private Walk _walk;
        [Required]
        [SerializeField] private Idle _idle;
        [Required]
        [SerializeField] private Eat _eat;
        [Required]
        [SerializeField] private Sleep _sleep;


        public AnimalSaveData CollectAnimalData()
        {
            return new AnimalSaveData()
            {
                position = _animal.transform.position,
                animalSOIndex = Array.IndexOf(_gameDesignSO.animalsDesign.animalSOs, _animal.animalSO),
            };
        }


        public void RestoreAnimalData(AnimalSaveData animalSaveData)
        {
            Animals_Design animalsDesign = _gameDesignSO.animalsDesign;
            AnimalSO animalSO = animalsDesign.animalSOs[animalSaveData.animalSOIndex];
            Animator animator = this.GetComponentInChildren<Animator>();

            _animal.Initialize(animalSO);
            _walk.Initialize(animator, this.transform, animalSO, animalSO.moveRadius,
                animalSO.moveSpeed);
            _idle.Initialize(animator, animalSO);
            _eat.Initialize(animator, animalSO);
            _sleep.Initialize(animator, animalSO);
        }
    }
}