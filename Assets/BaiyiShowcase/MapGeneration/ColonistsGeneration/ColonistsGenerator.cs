using System;
using BaiyiShowcase.Colonists;
using BaiyiShowcase.GameDesign;
using BaiyiShowcase.MapGeneration.GroundGeneration;
using BaiyiShowcase.NewGame;
using BaiyiShowcase.SaveLoadSystem.LoadManage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.MapGeneration.ColonistsGeneration
{
    public class ColonistsGenerator : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private Colonists _colonists;
        [Required]
        [SerializeField] private Transform _colonistsParent;

        private NewGameSettings NewGameSettingsProperty => NewGameSettings.Instance;

        private bool IsNewScene => LoadManager.Instance.IsNewScene;

        public static event Action OnEndGeneratingColonists;

        private void Awake()
        {
            ColonistsInitializer.OnEndInitializingColonists += GenerateColonists;
        }

        private void OnDestroy()
        {
            ColonistsInitializer.OnEndInitializingColonists -= GenerateColonists;
        }

        private void GenerateColonists()
        {
            if (IsNewScene)
            {
                FirstlyGenerateColonists();
            }
            else
            {
                RestoreEverySingleColonistData();
            }

            OnEndGeneratingColonists?.Invoke();
        }

        [Button]
        private void FirstlyGenerateColonists()
        {
            int index = 0;
            foreach (Vector2 colonistPosition in _colonists.positions)
            {
                ColonistInitializer colonistInitializer = Instantiate(_gameDesignSO.colonistsDesign.colonistPrefab,
                    colonistPosition, Quaternion.identity, _colonistsParent);
                colonistInitializer.InitializeColonist(NewGameSettingsProperty.colonists[index]);
                index++;
            }
        }

        private void RestoreEverySingleColonistData()
        {
            foreach (ColonistSaveData colonistSaveData in _colonists.colonistSaveDataList)
            {
                ColonistInitializer colonistInitializer = Instantiate(_gameDesignSO.colonistsDesign.colonistPrefab,
                    colonistSaveData.position, Quaternion.identity, _colonistsParent);
                colonistInitializer.saveDataHandler.RestoreColonistData(colonistSaveData);
            }
        }
    }
}