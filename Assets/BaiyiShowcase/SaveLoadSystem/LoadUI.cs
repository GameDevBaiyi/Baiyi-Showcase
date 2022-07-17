using BaiyiShowcase.SaveLoadSystem.SaveFileManage;
using BaiyiUtilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.SaveLoadSystem
{
    public class LoadUI : MonoBehaviour
    {
        [Required]
        [SerializeField] private Transform _content;
        [Required]
        [SerializeField] private LoadButtonInitializer _loadButtonPrefab;

        private FilesManager FilesManagerProperty => FilesManager.Instance;

        private void OnEnable()
        {
            UpdateUI();

            void UpdateUI()
            {
                FilesManagerProperty.GetFiles();
                _content.DestroyAllChildren();
                foreach (string saveFilePath in FilesManagerProperty._saveFilePaths)
                {
                    LoadButtonInitializer buttonInitializer = Instantiate(_loadButtonPrefab, _content);
                    buttonInitializer.Initialize(saveFilePath);
                }
            }
        }
    }
}