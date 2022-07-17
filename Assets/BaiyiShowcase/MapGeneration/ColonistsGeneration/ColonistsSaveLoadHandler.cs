using System.Collections.Generic;
using BaiyiShowcase.Colonists;
using BaiyiShowcase.SaveLoadSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.MapGeneration.ColonistsGeneration
{
    public class ColonistsSaveLoadHandler : SaveLoadHandler
    {
        [Required]
        [SerializeField] private Transform _colonistsParent;
        [Required]
        [SerializeField] private Colonists _colonists;

        public override object CollectSaveData()
        {
            List<ColonistSaveData> colonistSaveDataList = new List<ColonistSaveData>();
            foreach (Transform child in _colonistsParent)
            {
                if (child.TryGetComponent(out ColonistSaveDataHandler saveDataHandler))
                {
                    colonistSaveDataList.Add(saveDataHandler.CollectColonistData());
                }
            }

            return colonistSaveDataList;
        }

        public override void RestoreData()
        {
            if (SaveLoadAgent.Instance.cache.TryGetValue(id, out object data))
            {
                _colonists.colonistSaveDataList = (List<ColonistSaveData>)data;
            }
            else
            {
                Debug.Log("Cache中未找到Key");
            }
        }
    }
}