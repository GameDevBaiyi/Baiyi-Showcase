using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace BaiyiShowcase.UI.Public
{
    public class QuitGameButton : MonoBehaviour
    {
        private void Awake()
        {
            this.GetComponent<Button>().onClick.AddListener(HandleClick);
        }

        private void HandleClick()
        {
            Application.Quit();
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
        }
    }
}