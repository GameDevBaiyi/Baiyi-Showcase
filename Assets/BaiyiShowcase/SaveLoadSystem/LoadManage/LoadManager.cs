using System;
using System.Collections.Generic;
using System.IO;
using BaiyiShowcase.GameDesign;
using BaiyiShowcase.SaveLoadSystem.SaveFileManage;
using BaiyiUtilities.Singleton;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace BaiyiShowcase.SaveLoadSystem.LoadManage
{
    public class LoadManager : Singleton<LoadManager>
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private SaveLoadAgent _saveLoadAgent;
        [FormerlySerializedAs("_saveFilesManager")]
        [Required]
        [SerializeField] private FilesManager _filesManager;
        [Required]
        [SerializeField] private SaveManager _saveManager;

        public Dictionary<int, bool> newSceneRecord = new Dictionary<int, bool>();
        public int sceneIndexWhenSaved;

        public bool IsNewScene => newSceneRecord[SceneManager.GetActiveScene().buildIndex];

        public static event Action OnEndDeserializing;

        protected override void Awake()
        {
            base.Awake();
            if (Instance != this) return;

            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                newSceneRecord[i] = true;
            }

            SceneManager.sceneLoaded += Initialize; //每次载入Main Menu Scene时,初始化newSceneRecord.
            SaveLoadAgent.OnEndCollectingSceneData += RecordActiveScene; //OnEndCaching. Cache数据时即标记为此Scene不再是NewScene.
            SaveLoadAgent.OnBeginCaching += RecordSceneIndexWhenSaved;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= Initialize;
            SaveLoadAgent.OnEndCollectingSceneData -= RecordActiveScene;
            SaveLoadAgent.OnBeginCaching -= RecordSceneIndexWhenSaved;
        }

        private void Initialize(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.buildIndex != _gameDesignSO.commonDesign.mainMenuSceneIndex) return;

            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                newSceneRecord[i] = true;
            }
        }

        [Button]
        private void RecordActiveScene()
        {
            newSceneRecord[SceneManager.GetActiveScene().buildIndex] = false;
        }

        [Button]
        private void RecordSceneIndexWhenSaved()
        {
            sceneIndexWhenSaved = SceneManager.GetActiveScene().buildIndex;
        }

        [Button(ButtonStyle.Box, Expanded = true)] //LoadGame的实现: 先恢复Cache,再恢复LoadManager,此时加载TargetScene的数据已经足够.
        public void Load(string filePath)
        {
            if (_saveLoadAgent.DeserializeFromFile(filePath))
            {
                OnEndDeserializing?.Invoke();
                SceneManager.LoadScene(sceneIndexWhenSaved);
            }
        }

        [Button(ButtonStyle.Box, Expanded = true)]
        public void Load(int saveIndex)
        {
            Load(_filesManager._saveFilePaths[saveIndex]);
        }

        [Button]
        public void TestLoadAutoSaveFile()
        {
            string previousAutoSavePath = Path.Combine(_gameDesignSO.commonDesign.saveFileDirectory,
                "AutoSave" + _saveManager.PreviousAutoSaveIndex + _gameDesignSO.commonDesign.saveFileExtension);
            Load(previousAutoSavePath);
        }

        [Button]
        public void QuickLoad()
        {
            string previousQuickSavePath = Path.Combine(_gameDesignSO.commonDesign.saveFileDirectory,
                "QuickSave" + _saveManager.PreviousQuickSaveIndex + _gameDesignSO.commonDesign.saveFileExtension);
            Load(previousQuickSavePath);
        }
    }
}