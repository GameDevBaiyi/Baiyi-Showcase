using System;
using BaiyiShowcase.GameDesign;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.GameStaticSettings
{
    public class SettingsInitializer : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private GameStaticSettings _settings;
        [Required]
        [SerializeField] private SettingsSaver _settingsSaver;

        public static event Action OnEndInitializingSettings; 

        [Button]
        private void Reset()
        {
            _settings = this.GetComponent<GameStaticSettings>();
            _settingsSaver = this.GetComponent<SettingsSaver>();
        }

        private void Start()
        {
            if (!_settings.HasSaved)
            {
                InitializeSettings();
            }
            else
            {
                _settingsSaver.RestoreSettings();
            }
            
            OnEndInitializingSettings?.Invoke();
        }

        [Button]
        private void InitializeSettings()
        {
            _settings.GameVolume = _gameDesignSO.gameStaticSettingsDesign.gameVolumeDefault;
            _settings.MusicVolume = _gameDesignSO.gameStaticSettingsDesign.musicVolumeDefault;
            InitializeResolutionChoices();
            _settings.Resolution = _settings.resolutionChoices[^1];
            _settings.FullScreenMode = _gameDesignSO.gameStaticSettingsDesign.fullScreenModeDefault;
            _settings.AutoSaveInterval = _gameDesignSO.gameStaticSettingsDesign.autoSaveIntervalDefault;
            _settings.AutoSaveFilesCount = _gameDesignSO.gameStaticSettingsDesign.autoSaveFilesCountDefault;
            _settings.QuickSaveFilesCount = _gameDesignSO.gameStaticSettingsDesign.quickSaveFilesCountDefault;

            void InitializeResolutionChoices()
            {
                _settings.resolutionChoices.Clear();
                foreach (Resolution resolution in Screen.resolutions)
                {
                    _settings.resolutionChoices.Add(new Vector2Int(resolution.width, resolution.height));
                }
            }
        }
    }
}