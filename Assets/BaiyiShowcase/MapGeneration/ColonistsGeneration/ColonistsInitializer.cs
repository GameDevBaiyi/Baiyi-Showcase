using System;
using System.Collections.Generic;
using System.Linq;
using BaiyiShowcase.GameDesign;
using BaiyiShowcase.GameDesign.Terrains;
using BaiyiShowcase.Managers.GridManager;
using BaiyiShowcase.MapGeneration.GroundGeneration;
using BaiyiShowcase.NewGame;
using BaiyiShowcase.SaveLoadSystem.LoadManage;
using BaiyiUtilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

// ReSharper disable PossibleMultipleEnumeration
// ReSharper disable AccessToModifiedClosure

namespace BaiyiShowcase.MapGeneration.ColonistsGeneration
{
    public class ColonistsInitializer : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private Colonists _colonists;
        [Required]
        [SerializeField] private ColonistsSaveLoadHandler _saveLoadHandler;
        [Required]
        [SerializeField] private Ground _ground;
        [Required]
        [SerializeField] private GridSystem _gridSystem;

        private NewGameSettings NewGameSettingsProperty => NewGameSettings.Instance;
        private bool IsNewScene => LoadManager.Instance.IsNewScene;

        public static event Action OnEndInitializingColonists;

        private void Awake()
        {
            GroundInitializer.OnEndInitializingGround += InitializeColonists;
        }

        private void OnDestroy()
        {
            GroundInitializer.OnEndInitializingGround -= InitializeColonists;
        }

        private void InitializeColonists()
        {
            if (IsNewScene)
            {
                FirstlyInitializeColonists();
            }
            else
            {
                _saveLoadHandler.RestoreData();
            }

            OnEndInitializingColonists?.Invoke();
        }

        [Button] //找到初始空地,至少和角色个数相同的格子.
        private void FirstlyInitializeColonists()
        {
            int halfMapSize = _gridSystem.currentMapSize / 2;
            IEnumerable<TileSO> tilesCantGeneratingColonist =
                _gameDesignSO.colonistsDesign.tilesCantGeneratingColonistFirstly;
            IEnumerable<Vector2Int> allSpareCoords =
                _ground.groundDataDictionary.Where(t => !tilesCantGeneratingColonist.Contains(t.Value))
                    .Select(t => t.Key).OrderBy(t => Mathf.Abs(t.x - halfMapSize) + Mathf.Abs(t.y - halfMapSize));

            int colonistsCount = NewGameSettingsProperty.colonistsCount;
            while (true)
            {
                IEnumerable<Vector2Int> spawnCoords = Assistant.FloodFill8D(allSpareCoords.First(),
                    t => allSpareCoords.Contains(t), colonistsCount);
                if (spawnCoords.Count() >= colonistsCount)
                {
                    _colonists.positions = spawnCoords.Select(t => _gridSystem.CoordToWorldPosition(t));
                    break;
                }
                else
                {
                    allSpareCoords = allSpareCoords.Except(spawnCoords);
                }

                if (allSpareCoords.Count() == 0)
                {
                    Debug.Log("找不到合适的生成位置");
                    SceneManager.LoadScene(_gameDesignSO.commonDesign.mainMenuSceneIndex);
                    break;
                }
            }
        }
    }
}