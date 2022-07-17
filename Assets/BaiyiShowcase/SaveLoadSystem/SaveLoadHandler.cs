using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.SaveLoadSystem
{
    public abstract class SaveLoadHandler : MonoBehaviour
    {
        [Tooltip("右键产生Id")]
        [DisableInEditorMode][CustomContextMenu("Generate Id","GenerateId")]
        public string id;
        public void GenerateId() => id = Guid.NewGuid().ToString();

        public abstract object CollectSaveData();
        public abstract void RestoreData();

        protected virtual void Reset()
        {
            GenerateId();
        }
    }
}