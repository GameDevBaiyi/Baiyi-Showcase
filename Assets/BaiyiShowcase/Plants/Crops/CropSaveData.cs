using System;
using UnityEngine;

namespace BaiyiShowcase.Plants.Crops
{
    [Serializable]
    public struct CropSaveData
    {
        public Vector2 position;
        public int cropSOIndex;
        public int growthDays;
    }
}