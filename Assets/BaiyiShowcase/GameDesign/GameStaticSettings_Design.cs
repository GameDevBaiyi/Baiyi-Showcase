using System;
using BaiyiUtilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.GameDesign
{
    [Serializable]
    public class GameStaticSettings_Design
    {
        public float gameVolumeDefault;

        public float musicVolumeDefault;

        public bool fullScreenModeDefault;

        [ValidateInput("ValidateAutoSaveIntervalDefault", "必须在AutoSaveIntervalRange区间内")]
        public int autoSaveIntervalDefault;

        bool ValidateAutoSaveIntervalDefault(int interval)
        {
            return interval.IsInRange(autoSaveIntervalRange.x, autoSaveIntervalRange.y);
        }

        [ValidateInput("ValidateAutoSaveIntervalRange", "最小值必须大于等于1")]
        public Vector2Int autoSaveIntervalRange;

        bool ValidateAutoSaveIntervalRange(Vector2Int range)
        {
            return range.x > 0;
        }

        [Tooltip("玩家可选择的最大自动存档个数")]
        [ValidateInput("ValidateMaxAutoSaveFiles", "存档个数必须大于等于1")]
        public int maxAutoSaveFiles;

        bool ValidateMaxAutoSaveFiles(int max)
        {
            return max >= 1;
        }

        [ValidateInput("ValidateAutoSaveFilesCountDefault", "默认存档个数必须大于等于1,并且小于等于最大个数")]
        public int autoSaveFilesCountDefault;
        bool ValidateAutoSaveFilesCountDefault(int count)
        {
            return count >= 1 && count <= maxAutoSaveFiles;
        }

        [Tooltip("玩家可选择的最大快速存档个数")]
        [ValidateInput("ValidateQuickSaveFiles", "存档个数必须大于等于1")]
        public int maxQuickSaveFiles;

        bool ValidateQuickSaveFiles(int max)
        {
            return max >= 1;
        }

        [ValidateInput("ValidateQuickSaveFilesCountDefault", "默认存档个数必须大于等于1,并且小于等于最大个数")]
        public int quickSaveFilesCountDefault;

        bool ValidateQuickSaveFilesCountDefault(int count)
        {
            return count >= 1 && count <= maxQuickSaveFiles;
        }
    }
}