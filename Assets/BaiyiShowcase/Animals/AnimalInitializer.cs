using BaiyiShowcase.Animals.AnimalFSM;
using BaiyiShowcase.GameDesign;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Animals
{
    public class AnimalInitializer : MonoBehaviour
    {
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
        [Required]
        public AnimalSaveDataHandler _saveDataHandler;


        public void InitializeAnimal(AnimalSO animalSO)
        {
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