using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BaiyiShowcase.GameDesign;
using BaiyiShowcase.SaveLoadSystem.SerializationSurrogates;
using BaiyiUtilities.Singleton;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaiyiShowcase.SaveLoadSystem
{
    public class SaveLoadAgent : Singleton<SaveLoadAgent>
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;

        public Dictionary<string, object> cache = new Dictionary<string, object>();

        public static event Action OnEndCollectingSceneData;
        public static event Action OnBeginCaching;

        protected override void Awake()
        {
            base.Awake();

            if (Instance != this) return;

            SceneManager.sceneLoaded += ClearCache; //每次载入Main Menu Scene时,清理cache.
            GameMapTransitionManager.OnBeginMapTransition += CollectActiveSceneDataToCache; //在游戏流程中的地图切换之前Cache.
        }

        [Button]
        public void CollectActiveSceneDataToCache()
        {
            OnBeginCaching?.Invoke();
            
            foreach (SaveLoadHandler saveLoadHandler in FindObjectsOfType<SaveLoadHandler>())
            {
                cache[saveLoadHandler.id] = saveLoadHandler.CollectSaveData();
            }

            OnEndCollectingSceneData?.Invoke();
        }

        public void SerializeToFile(string path)
        {
            FileStream fileStream = File.Create(path);
            BinaryFormatter binaryFormatter = BinaryFormatterForUnity.GetBinaryFormatter();
            binaryFormatter.Serialize(fileStream, cache);
            fileStream.Close();
        }

        public bool DeserializeFromFile(string path)
        {
            if (!File.Exists(path))
            {
                Debug.Log("存档路径不存在.");
                return false;
            }

            FileStream fileStream = File.Open(path, FileMode.Open);
            BinaryFormatter binaryFormatter = BinaryFormatterForUnity.GetBinaryFormatter();
            cache = (Dictionary<string, object>)binaryFormatter
                .Deserialize(fileStream); //Replace the whole dictionary, so of its data.
            fileStream.Close();

            return true;
        }

        private void ClearCache(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.buildIndex != _gameDesignSO.commonDesign.mainMenuSceneIndex) return;

            cache.Clear();
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= ClearCache;
            GameMapTransitionManager.OnBeginMapTransition -= CollectActiveSceneDataToCache;
        }
    }
}