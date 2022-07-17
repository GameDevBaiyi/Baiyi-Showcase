using System.Collections.Generic;
using BaiyiShowcase.Colonists;
using UnityEngine;

namespace BaiyiShowcase.MapGeneration.ColonistsGeneration
{
    public class Colonists : MonoBehaviour
    {
        public IEnumerable<Vector2> positions;

        public List<ColonistSaveData> colonistSaveDataList;
    }
}