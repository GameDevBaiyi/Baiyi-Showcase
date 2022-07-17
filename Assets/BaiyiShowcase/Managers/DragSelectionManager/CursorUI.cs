using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace BaiyiShowcase.Managers.DragSelectionManager
{
    public class CursorUI : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameObject _cursor;
        [Required]
        [SerializeField] private RectTransform _cursorTransform;
        [Required]
        [SerializeField] private Image _image;

        private void Awake()
        {
            DragToSelect_State.OnEnterDragState += BeginDrawSelectionBox;
        }

        private void OnDestroy()
        {
            DragToSelect_State.OnEnterDragState -= BeginDrawSelectionBox;
        }

        private void Update()
        {
            _cursorTransform.position = Input.mousePosition;
        }

        private void BeginDrawSelectionBox(Sprite cursor)
        {
            if (cursor == true)
            {
                _image.sprite = cursor;
                _cursor.SetActive(true);
            }
            else
            {
                _image.sprite = cursor;
                _cursor.SetActive(false);
            }
        }
    }
}