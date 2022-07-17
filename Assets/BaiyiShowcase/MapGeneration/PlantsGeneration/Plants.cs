using System;
using System.Collections.Generic;
using BaiyiShowcase.Plants;
using BaiyiShowcase.Plants.Crops;
using BaiyiShowcase.Plants.Trees;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.MapGeneration.PlantsGeneration
{
    public class Plants : MonoBehaviour
    {
        public List<InitialPlantData> initialPlantDataList;

        public List<TreeSaveData> treeSaveDataList;
        public List<CropSaveData> cropSaveDataList;

        [Button]
        public void ClearData()
        {
            initialPlantDataList.Clear();
            treeSaveDataList.Clear();
            cropSaveDataList.Clear();
        }
    }

    [Serializable]
    public class InitialPlantData
    {
        public Vector2 position;
        public PlantSO plantSO;
    }

    [Serializable]
    public struct PlantsSaveData
    {
        public List<TreeSaveData> treeSaveDataList;
        public List<CropSaveData> cropSaveDataList;
    }
}