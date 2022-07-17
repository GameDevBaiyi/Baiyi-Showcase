using System;
using System.Collections.Generic;

namespace BaiyiShowcase.SaveLoadSystem.LoadManage
{
    [Serializable]
    public struct LoadManagerSaveData
    {
        public Dictionary<int, bool> newSceneRecord;
        public int sceneIndexWhenSaved;
    }
}