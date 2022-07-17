using System.Collections.Generic;
using System.IO;
using System.Linq;
using BaiyiShowcase.GameDesign;
using BaiyiUtilities.Singleton;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.SaveLoadSystem.SaveFileManage
{
    public class FilesManager : Singleton<FilesManager>
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;

        [DisableInEditorMode]
        public List<string> _saveFilePaths;

        protected override void Awake()
        {
            base.Awake();
            if (Instance != this) return;

            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = _gameDesignSO.commonDesign.saveFileDirectory;
            watcher.Created += GetFiles;
            watcher.Deleted += GetFiles;
        }

        public void GetFiles(object source, FileSystemEventArgs e)
        {
            GetFiles();
        }

        [Button]
        public void GetFiles()
        {
            _saveFilePaths = Directory.GetFiles(_gameDesignSO.commonDesign.saveFileDirectory,
                    "*" + _gameDesignSO.commonDesign.saveFileExtension)
                .ToList();
        }

        public void DeleteFile(int fileIndex)
        {
            File.Delete(_saveFilePaths[fileIndex]);
            _saveFilePaths.RemoveAt(fileIndex);
        }

        public void DeleteFile(string saveFilePath)
        {
            File.Delete(saveFilePath);
            _saveFilePaths.Remove(saveFilePath);
        }
    }
}