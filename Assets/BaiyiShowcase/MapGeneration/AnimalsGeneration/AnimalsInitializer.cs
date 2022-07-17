using System;
using System.Collections.Generic;
using System.Linq;
using BaiyiShowcase.Animals;
using BaiyiShowcase.GameDesign;
using BaiyiShowcase.GameDesign.Terrains;
using BaiyiShowcase.Managers.GridManager;
using BaiyiShowcase.MapGeneration.GroundGeneration;
using BaiyiShowcase.NewGame;
using BaiyiShowcase.SaveLoadSystem.LoadManage;
using BaiyiUtilities;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

// ReSharper disable PossibleMultipleEnumeration

namespace BaiyiShowcase.MapGeneration.AnimalsGeneration
{
    public class AnimalsInitializer : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private Animals _animals;
        [Required]
        [SerializeField] private AnimalsSaveLoadHandler _saveLoadHandler;
        [Required]
        [SerializeField] private Ground _ground;
        [Required]
        [SerializeField] private GridSystem _gridSystem;

        public static event Action OnEndInitializingAnimals;

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
            GroundInitializer.OnEndInitializingGround += InitializeAnimals;
        }

        private void OnDestroy()
        {
            GroundInitializer.OnEndInitializingGround -= InitializeAnimals;
        }

        private void InitializeAnimals()
        {
            if (IsNewScene)
            {
                FirstlyInitializeAnimals();
            }
            else
            {
                _saveLoadHandler.RestoreData();
            }

            OnEndInitializingAnimals?.Invoke();
        }

        [Button]
        private void FirstlyInitializeAnimals()
        {
            Random.InitState(Seed);
            _animals.ClearData();
            IEnumerable<Vector2> animalPositions = GetAnimalPositions();

            AnimalSO[] animalSOs = _gameDesignSO.animalsDesign.animalSOs;
            foreach (Vector2 animalPosition in animalPositions)
            {
                _animals.initialAnimalDataList.Add(new InitialAnimalData()
                {
                    position = animalPosition,
                    animalSO = animalSOs.GetRandomElement()
                });
            }


            IEnumerable<Vector2> GetAnimalPositions()
            {
                TileSO[] tilesCantGenerateAnimals = _gameDesignSO.animalsDesign.tilesCantGenerateAnimals;
                IEnumerable<Vector2Int> allCoordsCanGenerate = _ground.groundDataDictionary
                    .Where(t => !tilesCantGenerateAnimals.Contains(t.Value)).Select(t => t.Key)
                    .OrderBy(_ => Random.Range(Int32.MinValue, Int32.MaxValue));
                int animalsCount = (int)(allCoordsCanGenerate.Count() * _gameDesignSO.animalsDesign.density);

                return allCoordsCanGenerate.Take(animalsCount).Select(t => _gridSystem.CoordToWorldPosition(t));
            }
        }
    }
}