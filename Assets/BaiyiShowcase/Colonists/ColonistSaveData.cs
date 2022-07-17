using System;
using UnityEngine;

namespace BaiyiShowcase.Colonists
{
    [Serializable]
    public struct ColonistSaveData
    {
        public string colonistName;
        public Vector2 position;
        public int[] spriteIndexes;
    }
}