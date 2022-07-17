using UnityEngine;
using UnityEngine.UI;

namespace BaiyiShowcase.UI.UIFlow
{
    public class SwitchButton : MonoBehaviour
    {
        [SerializeField] private GameObject _windowsToShow;

        private Button _button;

        private void Awake()
        {
            _button = this.GetComponent<Button>();

            _button.onClick.AddListener(HandleClick);
        }

        private void HandleClick()
        {
            _windowsToShow.SetActive(!_windowsToShow.activeInHierarchy);
        }
    }
}