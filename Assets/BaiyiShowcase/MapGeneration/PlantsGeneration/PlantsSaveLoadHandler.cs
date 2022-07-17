using System.Collections.Generic;
using BaiyiShowcase.Plants;
using BaiyiShowcase.Plants.Crops;
using BaiyiShowcase.Plants.Trees;
using BaiyiShowcase.SaveLoadSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using Tree = BaiyiShowcase.Plants.Trees.Tree;

namespace BaiyiShowcase.MapGeneration.PlantsGeneration
{
    public class PlantsSaveLoadHandler : SaveLoadHandler
    {
        [Required]
        [SerializeField] private Plants _plants;
        [Required]
        [SerializeField] private Transform _plantsParent;

        public override object CollectSaveData()
        {
            PlantsSaveData plantsSaveData = new PlantsSaveData()
            {
                treeSaveDataList = new List<TreeSaveData>(),
                cropSaveDataList = new List<CropSaveData>()
            };

          for (int i = 0; i < _plantsParent.childCount; i++)
            {
                Transform currentTransform = _plantsParent.GetChild(i);
                if (currentTransform.TryGetComponent(out Plant plant))
                {
                    if (plant is Tree tree)
                    {
                        plantsSaveData.treeSaveDataList.Add(tree.saveDataHandler.CollectTreeSaveData());
                    }
                    else if (plant is Crop crop)
                    {
                        plantsSaveData.cropSaveDataList.Add(crop.saveDataHandler.CollectCropSaveData());
                    }
                }
                else
                {
                    Debug.Log("TryGetComponent失败了, 未找到 Plant");
                }
            }

            return plantsSaveData;
        }

        public override void RestoreData()
        {
            if (SaveLoadAgent.Instance.cache.TryGetValue(id, out object data))
            {
                PlantsSaveData plantsSaveData = (PlantsSaveData)data;
                _plants.treeSaveDataList = plantsSaveData.treeSaveDataList;
                _plants.cropSaveDataList = plantsSaveData.cropSaveDataList;
            }
            else
            {
                Debug.Log("Cache中未找到Key");
            }
        }
    }
}