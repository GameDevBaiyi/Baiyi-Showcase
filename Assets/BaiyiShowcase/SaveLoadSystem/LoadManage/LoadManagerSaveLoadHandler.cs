using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.SaveLoadSystem.LoadManage
{
    public class LoadManagerSaveLoadHandler : SaveLoadHandler
    {
        [Required]
        [SerializeField] private LoadManager _loadManager;

        private void Awake()
        {
            LoadManager.OnEndDeserializing += RestoreData;
        }

        public override object CollectSaveData()
        {
            return new LoadManagerSaveData()
            {
                newSceneRecord = _loadManager.newSceneRecord,
                sceneIndexWhenSaved = _loadManager.sceneIndexWhenSaved
            };
        }

        public override void RestoreData()
        {
            if (SaveLoadAgent.Instance.cache.TryGetValue(id, out object data))
            {
                LoadManagerSaveData loadManagerSaveData = (LoadManagerSaveData)data;
                _loadManager.newSceneRecord = loadManagerSaveData.newSceneRecord;
                _loadManager.sceneIndexWhenSaved = loadManagerSaveData.sceneIndexWhenSaved;
            }
            else
            {
                Debug.Log("Cache中未找到Key");
            }
        }

        private void OnDestroy()
        {
            LoadManager.OnEndDeserializing -= RestoreData;
        }
    }
}