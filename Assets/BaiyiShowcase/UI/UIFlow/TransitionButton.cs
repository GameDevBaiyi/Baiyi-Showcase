using UnityEngine;
using UnityEngine.UI;

namespace BaiyiShowcase.UI.UIFlow
{
    public class TransitionButton : MonoBehaviour
    {
        [SerializeField] private GameObject _windowToShow;
        [SerializeField] private GameObject _windowToHide;
        [SerializeField] private bool _canTriggerByEsc;

        private Button _button;

        private void Awake()
        {
            _button = this.GetComponent<Button>();

            _button.onClick.AddListener(HandleClick);
        }

        private void Update()
        {
            if (_canTriggerByEsc && Input.GetKeyDown(KeyCode.Escape))
            {
                HandleClick();
            }
        }

        private void HandleClick()
        {
            _windowToShow.SetActive(true);
            _windowToHide.SetActive(false);
        }
    }
}