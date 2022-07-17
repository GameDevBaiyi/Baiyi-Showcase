using System.IO;
using BaiyiShowcase.SaveLoadSystem.LoadManage;
using BaiyiShowcase.SaveLoadSystem.SaveFileManage;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BaiyiShowcase.SaveLoadSystem
{
    public class LoadButtonInitializer : MonoBehaviour
    {
        [Required]
        [SerializeField] private TextMeshProUGUI _buttonName;
        [Required]
        [SerializeField] private Button _loadButton;
        private FilesManager FilesManagerProperty => FilesManager.Instance;
        private LoadManager LoadManagerProperty => LoadManager.Instance;
        private string _saveFilePath;

        private void Awake()
        {
            _loadButton.onClick.AddListener(() => LoadManagerProperty.Load(_saveFilePath));
        }

        public void Initialize(string saveFilePath)
        {
            _saveFilePath = saveFilePath;
            _buttonName.text = Path.GetFileNameWithoutExtension(_saveFilePath);
        }

        public void DeleteFile()
        {
            FilesManagerProperty.DeleteFile(_saveFilePath);
            Destroy(this.gameObject);
        }
    }
}