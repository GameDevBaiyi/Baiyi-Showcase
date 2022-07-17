using System.Collections.Generic;
using BaiyiShowcase.Animals;
using BaiyiShowcase.GameDesign;
using UnityEngine;

namespace BaiyiShowcase.MapGeneration.AnimalsGeneration
{
    public class Animals : MonoBehaviour
    {
        public List<InitialAnimalData> initialAnimalDataList = new List<InitialAnimalData>();
        public List<AnimalSaveData> animalSaveDataList = new List<AnimalSaveData>();

        public void ClearData()
        {
            initialAnimalDataList.Clear();
        }
    }

    public class InitialAnimalData
    {
        public Vector2 position;
        public AnimalSO animalSO;
    }
}