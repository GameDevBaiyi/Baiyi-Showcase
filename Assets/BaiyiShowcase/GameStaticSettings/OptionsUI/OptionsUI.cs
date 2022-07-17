using System;
using System.Collections.Generic;
using System.Text;
using BaiyiShowcase.GameDesign;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BaiyiShowcase.GameStaticSettings.OptionsUI
{
    [LabelWidth(200)]
    public class OptionsUI : MonoBehaviour
    {
        [SerializeField] private GameDesignSO _gameDesignSO;
        private GameStaticSettings GameStaticSettingsProperty => GameStaticSettings.Instance;

        [Header("Corresponding UI")]
        [SerializeField] private Slider _gameVolume;
        [SerializeField] private Slider _musicVolume;
        [SerializeField] private TMP_Dropdown _resolution;
        [SerializeField] private Toggle _fullScreen;
        [SerializeField] private Slider _autoSaveInterval;
        [SerializeField] private TextMeshProUGUI _intervalNumber;
        [SerializeField] private Slider _autoSaveFiles;
        [SerializeField] private TextMeshProUGUI _autoSaveFilesNumber;
        [SerializeField] private Slider _quickSaveFiles;
        [SerializeField] private TextMeshProUGUI _quickSaveFilesNumber;

        private bool _dontOnEnableOnFirstTime = true;

        private void Awake()
        {
            SettingsInitializer.OnEndInitializingSettings += InitializeUI;
        }

        private void OnDestroy()
        {
            SettingsInitializer.OnEndInitializingSettings -= InitializeUI;
        }

        private void OnEnable()
        {
            if (_dontOnEnableOnFirstTime) return;

            UpdateUI();

            void UpdateUI()
            {
                _gameVolume.value = GameStaticSettingsProperty.GameVolume;
                _musicVolume.value = GameStaticSettingsProperty.MusicVolume;
                _resolution.value =
                    GameStaticSettingsProperty.resolutionChoices.IndexOf(GameStaticSettingsProperty.Resolution);
                _fullScreen.isOn = GameStaticSettingsProperty.FullScreenMode;
                _autoSaveInterval.value = GameStaticSettingsProperty.AutoSaveInterval;
                _autoSaveFiles.value = GameStaticSettingsProperty.AutoSaveFilesCount;
                _quickSaveFiles.value = GameStaticSettingsProperty.QuickSaveFilesCount;
            }
        }

        private void Start()
        {
            //Hide When Initialized.
            this.gameObject.SetActive(false);

            _dontOnEnableOnFirstTime = false;
            InitializeUI();
        }

        private void InitializeUI()
        {
            //添加分辨率的DropDown选项.
            _resolution.ClearOptions();
            List<string> options = new List<string>();
            StringBuilder builder = new StringBuilder();
            foreach (Vector2Int resolutionChoice in GameStaticSettingsProperty.resolutionChoices)
            {
                builder.Clear();
                builder.Append(resolutionChoice.x + " * " + resolutionChoice.y);
                options.Add(builder.ToString());
            }

            _resolution.AddOptions(options);

            //初始化Slider的最大最小值.
            _autoSaveInterval.minValue = _gameDesignSO.gameStaticSettingsDesign.autoSaveIntervalRange.x;
            _autoSaveInterval.maxValue = _gameDesignSO.gameStaticSettingsDesign.autoSaveIntervalRange.y;

            //初始化Slider的最大最小值.
            _autoSaveFiles.minValue = 1;
            _autoSaveFiles.maxValue = _gameDesignSO.gameStaticSettingsDesign.maxAutoSaveFiles;

            //初始化Slider的最大最小值.
            _quickSaveFiles.minValue = 1;
            _quickSaveFiles.maxValue = _gameDesignSO.gameStaticSettingsDesign.maxQuickSaveFiles;

            SubscribeEvents();

            void SubscribeEvents()
            {
                _gameVolume.onValueChanged.AddListener(UpdateGameVolume);
                _musicVolume.onValueChanged.AddListener(UpdateMusicVolume);
                _resolution.onValueChanged.AddListener(UpdateResolution);
                _fullScreen.onValueChanged.AddListener(UpdateFullScreen);
                _autoSaveInterval.onValueChanged.AddListener(UpdateAutoSaveInterval);
                _autoSaveFiles.onValueChanged.AddListener(UpdateAutoSaveFiles);
                _quickSaveFiles.onValueChanged.AddListener(UpdateQuickSaveFiles);
            }
        }

        private void UpdateGameVolume(float value)
        {
            GameStaticSettingsProperty.GameVolume = value;
        }

        private void UpdateMusicVolume(float value)
        {
            GameStaticSettingsProperty.MusicVolume = value;
        }

        private void UpdateResolution(int value)
        {
            GameStaticSettingsProperty.Resolution = GameStaticSettingsProperty.resolutionChoices[value];
        }

        private void UpdateFullScreen(bool value)
        {
            GameStaticSettingsProperty.FullScreenMode = value;
        }

        private void UpdateAutoSaveInterval(float value)
        {
            GameStaticSettingsProperty.AutoSaveInterval = (int)value;
            _intervalNumber.text = value.ToString();
        }

        private void UpdateAutoSaveFiles(float value)
        {
            GameStaticSettingsProperty.AutoSaveFilesCount = (int)value;
            _autoSaveFilesNumber.text = value.ToString();
        }

        private void UpdateQuickSaveFiles(float value)
        {
            GameStaticSettingsProperty.QuickSaveFilesCount = (int)value;
            _quickSaveFilesNumber.text = value.ToString();
        }
    }
}