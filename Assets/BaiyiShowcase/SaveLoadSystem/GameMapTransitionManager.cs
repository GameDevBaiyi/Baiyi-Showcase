using System;
using UnityEngine.SceneManagement;

namespace BaiyiShowcase.SaveLoadSystem
{
    /// <summary>
    /// 游戏流程中的地图切换均调用此接口, Main Menu的进出不使用此接口.
    /// </summary>
    public static class GameMapTransitionManager
    {
        public static event Action OnBeginMapTransition;

        public static void Transition(int targetSceneIndex)
        {
            OnBeginMapTransition?.Invoke();
            SceneManager.LoadScene(targetSceneIndex);
        }
    }
}