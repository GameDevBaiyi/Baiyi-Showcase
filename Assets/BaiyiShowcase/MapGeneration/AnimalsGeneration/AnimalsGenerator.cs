using BaiyiShowcase.Animals;
using BaiyiShowcase.GameDesign;
using BaiyiShowcase.NewGame;
using BaiyiShowcase.SaveLoadSystem.LoadManage;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BaiyiShowcase.MapGeneration.AnimalsGeneration
{
    public class AnimalsGenerator : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private Animals _animals;
        [Required]
        [SerializeField] private Transform __animalsParent;

        private bool IsNewScene => LoadManager.Instance.IsNewScene;

        private int Seed
        {
            get
            {
                if (NewGameSettings.Instance == true)
                {
                    return NewGameSettings.Instance.seed.GetHashCode();
                }
                else
                {
                    Debug.Log("未找到NewGameSettings.Instance.seed , 使用随机Seed");
                    return Random.Range(-1000, 1000);
                }
            }
        }

        private void Awake()
        {
            AnimalsInitializer.OnEndInitializingAnimals += GenerateAnimals;
        }

        private void OnDestroy()
        {
            AnimalsInitializer.OnEndInitializingAnimals -= GenerateAnimals;
        }

        private void GenerateAnimals()
        {
            if (IsNewScene)
            {
                FirstlyGenerateAnimals();
            }
            else
            {
                RestoreEverySingleAnimalData();
            }
        }

        [Button]
        private void FirstlyGenerateAnimals()
        {
            Random.InitState(Seed);
            foreach (InitialAnimalData initialAnimalData in _animals.initialAnimalDataList)
            {
                AnimalInitializer animalInitializer = Instantiate(initialAnimalData.animalSO.animalPrefab,
                    initialAnimalData.position, Quaternion.identity, __animalsParent);
                animalInitializer.InitializeAnimal(initialAnimalData.animalSO);
            }
        }

        private void RestoreEverySingleAnimalData()
        {
            foreach (AnimalSaveData animalSaveData in _animals.animalSaveDataList)
            {
                AnimalSO animalSO = _gameDesignSO.animalsDesign.animalSOs[animalSaveData.animalSOIndex];
                AnimalInitializer animalInitializer = Instantiate(animalSO.animalPrefab,
                    animalSaveData.position, Quaternion.identity, __animalsParent);
                animalInitializer._saveDataHandler.RestoreAnimalData(animalSaveData);
            }
        }
    }
}