using System.Collections.Generic;
using BaiyiShowcase.Ores;
using UnityEngine;

namespace BaiyiShowcase.MapGeneration.OresGeneration
{
    public class Ores : MonoBehaviour
    {
        public List<InitialOreData> initialOreDataList = new List<InitialOreData>();
        public List<OreSaveData> oreSaveDataList = new List<OreSaveData>();

        public void ClearData()
        {
            initialOreDataList.Clear();
            oreSaveDataList.Clear();
        }
    }

    public class InitialOreData
    {
        public Vector2 position;
        public OreSO oreSO;
    }
}