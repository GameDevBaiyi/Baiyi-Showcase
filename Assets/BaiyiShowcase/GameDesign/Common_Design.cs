using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.GameDesign
{
    [Serializable]
    public class Common_Design
    {
        public int mainMenuSceneIndex;

        public int baseMapSceneIndex;

        public int wildMapSize;

        [Tooltip("地图最基本的格子,每单位的大小,一般就设为1,即每个格子1*1.")]
        public float nodeSize;

        [InlineButton("RestoreSaveFileDirectory", "Restore")]
        public string saveFileDirectory;

        void RestoreSaveFileDirectory()
        {
            saveFileDirectory = Application.persistentDataPath;
        }

        public string saveFileExtension = ".baiyiShowCase";

        public Vector2 cameraOrthographicSizeRange;
        public float cameraSpeed;
    }
}