using UnityEngine;

namespace BaiyiShowcase.GameStaticSettings
{
    public class SettingsApplier : MonoBehaviour
    {
        private void Awake()
        {
            GameStaticSettings.OnEndChangingScreenSettings += ApplyScreenSettings;
        }

        private void OnDestroy()
        {
            GameStaticSettings.OnEndChangingScreenSettings -= ApplyScreenSettings;
        }

        private void ApplyScreenSettings(Vector2Int resolution, bool isFullScreen)
        {
            Screen.SetResolution(resolution.x, resolution.y, isFullScreen);
        }
    }
}