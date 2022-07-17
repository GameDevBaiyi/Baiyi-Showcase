using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BaiyiShowcase.NewGame
{
    public class SelectButtonInitializer : MonoBehaviour
    {
        [Required]
        [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
        [Required]
        [SerializeField] private Button _selectUp;
        [Required]
        [SerializeField] private Button _selectDown;

        private NewGameSettings2UI NewGameSettings2UIProperty => NewGameSettings2UI.Instance;
        private NewGameSettings NewGameSettingsProperty => NewGameSettings.Instance;

        private SpriteRenderer _appearanceRenderer;

        [ShowInInspector] [DisableInEditorMode]
        private int _bodyPartIndex;
        [ShowInInspector] [DisableInEditorMode]
        private Sprite[] _sprites;
        [ShowInInspector] [DisableInEditorMode]
        private int _spriteIndex;

        private void Awake()
        {
            _selectUp.onClick.AddListener(SelectUp);
            _selectDown.onClick.AddListener(SelectDown);
        }

        public void Initialize(string bodyPartName, Sprite[] sprites, int bodyPartIndex,
            SpriteRenderer appearanceRenderer)
        {
            _textMeshProUGUI.text = bodyPartName;
            _sprites = sprites;
            _bodyPartIndex = bodyPartIndex;
            _appearanceRenderer = appearanceRenderer;
        }

        private void SelectUp()
        {
            if (_spriteIndex == _sprites.Length - 1)
            {
                _spriteIndex = 0;
            }
            else
            {
                _spriteIndex++;
            }

            NewGameSettingsProperty.colonists[NewGameSettings2UIProperty.currentColonistIndex].sprites[_bodyPartIndex] =
                _sprites[_spriteIndex];
            _appearanceRenderer.sprite = _sprites[_spriteIndex];
        }

        private void SelectDown()
        {
            if (_spriteIndex == 0)
            {
                _spriteIndex = _sprites.Length - 1;
            }
            else
            {
                _spriteIndex--;
            }

            NewGameSettingsProperty.colonists[NewGameSettings2UIProperty.currentColonistIndex].sprites[_bodyPartIndex] =
                _sprites[_spriteIndex];
            _appearanceRenderer.sprite = _sprites[_spriteIndex];
        }
    }
}