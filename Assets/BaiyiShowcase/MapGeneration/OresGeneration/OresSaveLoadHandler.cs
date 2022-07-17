using System.Collections.Generic;
using BaiyiShowcase.Ores;
using BaiyiShowcase.SaveLoadSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.MapGeneration.OresGeneration
{
    public class OresSaveLoadHandler : SaveLoadHandler
    {
        [Required]
        [SerializeField] private Ores _ores;
        [Required]
        [SerializeField] private Transform _oresParent;

        public override object CollectSaveData()
        {
            List<OreSaveData> oresSaveData = new List<OreSaveData>();

            for (int i = 0; i < _oresParent.childCount; i++)
            {
                Transform currentTransform = _oresParent.GetChild(i);
                if (currentTransform.TryGetComponent(out OreSaveDataHandler oreSaveDataHandler))
                {
                    oresSaveData.Add(oreSaveDataHandler.CollectOreData());
                }
                else
                {
                    Debug.Log("TryGetComponent失败了, 未找到OreSaveDataHandler");
                }
            }

            return oresSaveData;
        }

        public override void RestoreData()
        {
            if (SaveLoadAgent.Instance.cache.TryGetValue(id, out object data))
            {
                _ores.oreSaveDataList = (List<OreSaveData>)data;
            }
            else
            {
                Debug.Log("Cache中未找到Key");
            }
        }
    }
}