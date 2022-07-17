using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.GameStaticSettings
{
    public class SettingsSaver : MonoBehaviour
    {
        [SerializeField] private GameStaticSettings _settings;

        [Button]
        private void Reset()
        {
            _settings = this.GetComponent<GameStaticSettings>();
        }

        [Button]
        private void SaveSetting()
        {
            PlayerPrefs.SetFloat("GameVolume", _settings.GameVolume);
            PlayerPrefs.SetFloat("MusicVolume", _settings.MusicVolume);
            SaveResolutionChoices();
            PlayerPrefs.SetInt("ResolutionX", _settings.Resolution.x);
            PlayerPrefs.SetInt("ResolutionY", _settings.Resolution.y);
            PlayerPrefs.SetInt("FullScreenMode", _settings.FullScreenMode ? 1 : 0);
            PlayerPrefs.SetInt("AutoSaveInterval", _settings.AutoSaveInterval);
            PlayerPrefs.SetInt("AutoSaveFilesCount", _settings.AutoSaveFilesCount);
            PlayerPrefs.SetInt("QuickSaveFilesCount", _settings.QuickSaveFilesCount);
            PlayerPrefs.SetString(_settings.playerKey, _settings.playerKey);
            PlayerPrefs.Save();

            void SaveResolutionChoices()
            {
                PlayerPrefs.SetInt("resolutionChoicesCount", _settings.resolutionChoices.Count);
                int index = 0;
                foreach (Vector2Int settingsResolutionChoice in _settings.resolutionChoices)
                {
                    PlayerPrefs.SetInt("resolutionChoicesX" + index, settingsResolutionChoice.x);
                    PlayerPrefs.SetInt("resolutionChoicesY" + index, settingsResolutionChoice.y);
                    index++;
                }
            }
        }

        [Button]
        public void RestoreSettings()
        {
            _settings.GameVolume = PlayerPrefs.GetFloat("GameVolume");
            _settings.MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
            RestoreResolutionChoices();
            _settings.Resolution = new Vector2Int(PlayerPrefs.GetInt("ResolutionX"), PlayerPrefs.GetInt("ResolutionY"));
            _settings.FullScreenMode = PlayerPrefs.GetInt("FullScreenMode") == 1;
            _settings.AutoSaveInterval = PlayerPrefs.GetInt("AutoSaveInterval");
            _settings.AutoSaveFilesCount = PlayerPrefs.GetInt("AutoSaveFilesCount");
            _settings.QuickSaveFilesCount = PlayerPrefs.GetInt("QuickSaveFilesCount");

            void RestoreResolutionChoices()
            {
                _settings.resolutionChoices = new List<Vector2Int>();
                for (int i = 0; i < PlayerPrefs.GetInt("resolutionChoicesCount"); i++)
                {
                    _settings.resolutionChoices.Add(new Vector2Int(PlayerPrefs.GetInt("resolutionChoicesX" + i),
                        PlayerPrefs.GetInt("resolutionChoicesY" + i)));
                }
            }
        }

        [Button]
        public void DeleteSavedPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        private void OnDestroy()
        {
            SaveSetting();
        }
    }
}