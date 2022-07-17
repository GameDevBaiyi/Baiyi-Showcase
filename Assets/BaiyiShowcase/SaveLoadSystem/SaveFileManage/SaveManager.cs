using System;
using System.IO;
using BaiyiShowcase.GameDesign;
using BaiyiUtilities.Singleton;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.SaveLoadSystem.SaveFileManage
{
    public class SaveManager : Singleton<SaveManager>
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private GameStaticSettings.GameStaticSettings _gameStaticSettings;
        [Required]
        [SerializeField] private SaveLoadAgent _saveLoadAgent;

        //这两个数据使用PlayerPrefs保存, 与GameStaticSettings相同.
        private int _currentAutoSaveIndex = 1;
        private int _currentQuickSaveIndex = 1;
        public int PreviousAutoSaveIndex => _currentAutoSaveIndex == 1
            ? _gameStaticSettings.AutoSaveFilesCount
            : _currentAutoSaveIndex - 1;
        public int PreviousQuickSaveIndex => _currentQuickSaveIndex == 1
            ? _gameStaticSettings.QuickSaveFilesCount
            : _currentQuickSaveIndex - 1;


        private string GetAutoSavePath()
        {
            string path = Path.Combine(_gameDesignSO.commonDesign.saveFileDirectory,
                "AutoSave" + _currentAutoSaveIndex + _gameDesignSO.commonDesign.saveFileExtension);
            if (_currentAutoSaveIndex == _gameStaticSettings.AutoSaveFilesCount)
            {
                _currentAutoSaveIndex = 1;
            }
            else
            {
                _currentAutoSaveIndex++;
            }

            return path;
        }

        private string GetQuickSavePath()
        {
            string path = Path.Combine(_gameDesignSO.commonDesign.saveFileDirectory,
                "QuickSave" + _currentQuickSaveIndex + _gameDesignSO.commonDesign.saveFileExtension);
            if (_currentQuickSaveIndex == _gameStaticSettings.QuickSaveFilesCount)
            {
                _currentQuickSaveIndex = 1;
            }
            else
            {
                _currentQuickSaveIndex++;
            }

            return path;
        }

        private string GetManualSavePath()
        {
            return Path.Combine(_gameDesignSO.commonDesign.saveFileDirectory,
                DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + _gameDesignSO.commonDesign.saveFileExtension);
        }

        [Button]
        public void AutoSave()
        {
            _saveLoadAgent.CollectActiveSceneDataToCache();
            _saveLoadAgent.SerializeToFile(GetAutoSavePath());
        }

        [Button]
        public void QuickSave()
        {
            _saveLoadAgent.CollectActiveSceneDataToCache();
            _saveLoadAgent.SerializeToFile(GetQuickSavePath());
        }

        [Button]
        public void ManualSave()
        {
            _saveLoadAgent.CollectActiveSceneDataToCache();
            _saveLoadAgent.SerializeToFile(GetManualSavePath());
        }

        protected override void Awake()
        {
            base.Awake();

            if (Instance != this) return;

            RestoreIndexes();
        }

        private void OnDestroy()
        {
            SaveIndexes();
        }

        [Button]
        private void SaveIndexes()
        {
            PlayerPrefs.SetInt("_currentAutoSaveIndex", _currentAutoSaveIndex);
            PlayerPrefs.SetInt("_currentQuickSaveIndex", _currentQuickSaveIndex);
            PlayerPrefs.Save();
        }


        [Button]
        private void RestoreIndexes()
        {
            _currentAutoSaveIndex = PlayerPrefs.GetInt("_currentAutoSaveIndex");
            _currentQuickSaveIndex = PlayerPrefs.GetInt("_currentQuickSaveIndex");
        }
    }
}