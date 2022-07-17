using System;
using System.Collections.Generic;
using BaiyiUtilities.Singleton;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.GameStaticSettings
{
    [LabelWidth(120f)] [TypeInfoBox("此处的Inspector仅予Debugging使用, 要设置相关的默认值, 在GameDesignSO中修改")]
    public class GameStaticSettings : Singleton<GameStaticSettings>
    {
        [DisableInEditorMode] [VerticalGroup("1/1")]
        public string playerKey = "Whatever";
        [ShowInInspector] [VerticalGroup("1/1")]
        public bool HasSaved => PlayerPrefs.HasKey(playerKey);

        [Header("玩家设定值(仅作Debugger)")]
        private float _gameVolume;
        [HorizontalGroup("1", 0.5f)]
        [ShowInInspector] [VerticalGroup("1/1", Order = -1)]
        public float GameVolume { get; set; }

        private float _musicVolume;
        [ShowInInspector] [VerticalGroup("1/1")]
        public float MusicVolume { get; set; }

        [DisableInEditorMode] [VerticalGroup("1/2")]
        [ListDrawerSettings(ShowIndexLabels = true, Expanded = true, NumberOfItemsPerPage = 10)] [LabelWidth(50f)]
        public List<Vector2Int> resolutionChoices;

        private Vector2Int _resolution;
        [ShowInInspector] [VerticalGroup("1/1")]
        public Vector2Int Resolution
        {
            get => _resolution;

            set
            {
                _resolution = value;
                OnEndChangingScreenSettings?.Invoke(_resolution, _fullScreenMode);
            }
        }
        public static event Action<Vector2Int, bool> OnEndChangingScreenSettings;

        private bool _fullScreenMode;
        [ShowInInspector] [VerticalGroup("1/1")]
        public bool FullScreenMode
        {
            get => _fullScreenMode;

            set
            {
                _fullScreenMode = value;
                OnEndChangingScreenSettings?.Invoke(_resolution, _fullScreenMode);
            }
        }

        private int _autoSaveInterval;
        [ShowInInspector] [VerticalGroup("1/1")]
        public int AutoSaveInterval { get; set; }

        private int _autoSaveFilesCount;
        [ShowInInspector] [VerticalGroup("1/1")]
        public int AutoSaveFilesCount { get; set; }

        private int _quickSaveFilesCount;
        [ShowInInspector] [VerticalGroup("1/1")]
        public int QuickSaveFilesCount { get; set; }

        [Button]
        private void Print()
        {
            Debug.Log(AutoSaveInterval);
        }
    }
}