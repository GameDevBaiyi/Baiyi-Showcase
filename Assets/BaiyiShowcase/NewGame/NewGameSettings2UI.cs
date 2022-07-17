using System.Collections.Generic;
using System.Linq;
using BaiyiShowcase.GameDesign;
using BaiyiUtilities;
using BaiyiUtilities.Singleton;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace BaiyiShowcase.NewGame
{
    public class NewGameSettings2UI : Singleton<NewGameSettings2UI>
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        private NewGameSettings NewGameSettingsProperty => NewGameSettings.Instance;
        [Required]
        [SerializeField] private Transform _colonistButtonsParent;
        [Required]
        [SerializeField] private ColonistButtonInitializer _colonistButtonPrefab;
        [Required]
        [SerializeField] private TMP_InputField _nameInput;
        [Required]
        [SerializeField] private Transform _selectButtonsParent;
        [Required]
        [SerializeField] private SelectButtonInitializer _selectButtonPrefab;
        [Required]
        [SerializeField] private Transform _skillsParent;
        [Required]
        [SerializeField] private SkillUIInitializer _skillUIPrefab;
        [Required]
        [SerializeField] private Transform _colonistAppearance;

        private bool _dontOnEnableOnFirstTime = true;

        [DisableInEditorMode]
        public int currentColonistIndex = 0;

        private void OnEnable()
        {
            if (_dontOnEnableOnFirstTime) return;

            NewGameSettingsProperty.InitializeColonistsData();
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
            int bodyPartIndex = 0;
            foreach (ColonistsCustomization colonistsDesignCustomization in
                     _gameDesignSO.colonistsDesign.customizations)
            {
                SelectButtonInitializer selectButtonInitializer =
                    Instantiate(_selectButtonPrefab, _selectButtonsParent);
                selectButtonInitializer.Initialize(colonistsDesignCustomization.name,
                    colonistsDesignCustomization.sprites, bodyPartIndex,
                    _colonistAppearance.FindInAllChildren(colonistsDesignCustomization.name)
                        .GetComponent<SpriteRenderer>());
                bodyPartIndex++;
            }

            foreach (string skill in _gameDesignSO.newGameSettingsDesign.skills)
            {
                SkillUIInitializer skillUIInitializer = Instantiate(_skillUIPrefab, _skillsParent);
                skillUIInitializer.Initialize(skill, 20);
            }
        }

        public void UpdateUI()
        {
            //更新对应角色个数的Buttons.
            _colonistButtonsParent.DestroyAllChildren();
            int correspondingColonistIndex = 0;
            foreach (NewGameColonist newGameColonist in NewGameSettingsProperty.colonists)
            {
                ColonistButtonInitializer colonistButtonInitializer =
                    Instantiate(_colonistButtonPrefab, _colonistButtonsParent);
                colonistButtonInitializer.Initialize(newGameColonist.colonistName, correspondingColonistIndex);
                correspondingColonistIndex++;
            }

            //更新外观.
            int index = 0;
            foreach (Sprite sprite in NewGameSettingsProperty.colonists[currentColonistIndex].sprites)
            {
                if (_colonistAppearance.FindInAllChildren(_gameDesignSO.colonistsDesign.customizations[index].name)
                    .TryGetComponent(out SpriteRenderer spriteRenderer))
                {
                    spriteRenderer.sprite = sprite;
                }
                else
                {
                    Debug.Log("未找到对应名称 " + _gameDesignSO.colonistsDesign.customizations[index].name + " 的身体部位");
                }

                index++;
            }

            //更新Name Input.
            _nameInput.text = NewGameSettingsProperty.colonists[currentColonistIndex].colonistName;

            //更新Skill UI.
            int index1 = 0;
            Dictionary<string, int>.ValueCollection skillsValues =
                NewGameSettingsProperty.colonists[currentColonistIndex].skills.Values;
            foreach (Transform child in _skillsParent)
            {
                if (child.TryGetComponent(out SkillUIInitializer skillUIInitializer))
                {
                    skillUIInitializer.ChangeSkillValue(skillsValues.ElementAt(index1));
                }
                else
                {
                    Debug.Log("未找到skillUIInitializer");
                }

                index1++;
            }
        }
    }
}