using System;
using UnityEngine;

namespace BaiyiShowcase.Plants.Trees
{
    [Serializable]
    public struct TreeSaveData
    {
        public Vector2 position;
        public int treeSOIndex;
        public int growthDays;
    }
}