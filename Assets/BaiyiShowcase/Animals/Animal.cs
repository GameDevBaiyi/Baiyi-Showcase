using System;
using UnityEngine;

namespace BaiyiShowcase.Animals
{
    public class Animal : MonoBehaviour
    {
        public AnimalSO animalSO;

        public void Initialize(AnimalSO animalSOParameter)
        {
            this.animalSO = animalSOParameter;
        }
    }

    [Serializable]
    public struct AnimalSaveData
    {
        public Vector2 position;
        public int animalSOIndex;
    }
}