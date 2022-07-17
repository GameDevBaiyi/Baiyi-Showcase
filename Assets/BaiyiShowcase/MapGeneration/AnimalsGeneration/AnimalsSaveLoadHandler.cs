using System.Collections.Generic;
using BaiyiShowcase.Animals;
using BaiyiShowcase.SaveLoadSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.MapGeneration.AnimalsGeneration
{
    public class AnimalsSaveLoadHandler : SaveLoadHandler
    {
        [Required]
        [SerializeField] private Animals _animals;
        [Required]
        [SerializeField] private Transform _animalsParent;

        public override object CollectSaveData()
        {
            List<AnimalSaveData> animalSaveDataList = new List<AnimalSaveData>();

            for (int i = 0; i < _animalsParent.childCount; i++)
            {
                Transform currentTransform = _animalsParent.GetChild(i);
                if (currentTransform.TryGetComponent(out AnimalSaveDataHandler animalSaveDataHandler))
                {
                    animalSaveDataList.Add(animalSaveDataHandler.CollectAnimalData());
                }
                else
                {
                    Debug.Log("TryGetComponent失败了, 未找到AnimalSaveDataHandler");
                }
            }

            return animalSaveDataList;
        }

        public override void RestoreData()
        {
            if (SaveLoadAgent.Instance.cache.TryGetValue(id, out object data))
            {
                _animals.animalSaveDataList = (List<AnimalSaveData>)data;
            }
            else
            {
                Debug.Log("Cache中未找到Key");
            }
        }
    }
}