using System;
using System.Collections.Generic;
using System.Linq;
using BaiyiShowcase.GameDesign;
using BaiyiUtilities;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BaiyiShowcase.NewGame
{
    public class NewGameSettings1UI : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        private NewGameSettings NewGameSettingsProperty => NewGameSettings.Instance;
        [Required]
        [SerializeField] private TMP_InputField _seed;
        [Required]
        [SerializeField] private Button _randomize;
        [Required]
        [SerializeField] private TMP_Dropdown _mapSize;
        [Required]
        [SerializeField] private TMP_Dropdown _terrain;
        [Required]
        [SerializeField] private TMP_Dropdown _colonistCount;

        private bool _dontOnEnableOnFirstTime = true;

        private void Awake()
        {
            _seed.onValueChanged.AddListener(HandlerChangingSeed);
            _randomize.onClick.AddListener(HandleClickRandomizeButton);
            _mapSize.onValueChanged.AddListener(HandlerChangingMapSize);
            _colonistCount.onValueChanged.AddListener(HandlerChangingColonistCount);
        }

        private void OnEnable()
        {
            if (_dontOnEnableOnFirstTime) return;

            UpdateUI();
        }

        private void Start()
        {
            InitializeUI();

            //Hide when initialized.
            this.gameObject.SetActive(false);
            _dontOnEnableOnFirstTime = false;
        }

        private void InitializeUI()
        {
            _mapSize.ClearOptions();
            _mapSize.AddOptions(_gameDesignSO.groundGenerationDesign.mapSizeChoices.Select(t => t.ToString()).ToList());

            _terrain.ClearOptions();
            _terrain.AddOptions(new List<string>() { "Normal Terrain" });

            _colonistCount.ClearOptions();
            _colonistCount.AddOptions(_gameDesignSO.newGameSettingsDesign.characterCountChoices
                .Select(t => t.ToString()).ToList());

            UpdateUI();
        }

        private void UpdateUI()
        {
            _seed.text = NewGameSettingsProperty.seed;
            _mapSize.value = Array.IndexOf(_gameDesignSO.groundGenerationDesign.mapSizeChoices,
                NewGameSettingsProperty.mapSize);
            _colonistCount.value = Array.IndexOf(_gameDesignSO.newGameSettingsDesign.characterCountChoices,
                NewGameSettingsProperty.colonistsCount);
        }


        private void HandlerChangingSeed(string seed)
        {
            NewGameSettingsProperty.seed = seed;
        }

        private void HandleClickRandomizeButton()
        {
            _seed.text = Assistant.GetRandomString(_gameDesignSO.groundGenerationDesign.seedLength);
        }

        private void HandlerChangingMapSize(int choice)
        {
            NewGameSettingsProperty.mapSize = _gameDesignSO.groundGenerationDesign.mapSizeChoices[choice];
        }

        private void HandlerChangingColonistCount(int choice)
        {
            NewGameSettingsProperty.colonistsCount = _gameDesignSO.newGameSettingsDesign.characterCountChoices[choice];
        }
    }
}