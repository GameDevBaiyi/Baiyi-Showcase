using System;
using BaiyiUtilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Managers.DragSelectionManager
{
    public class SelectionBoxUI : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameObject _selectionBox;
        [Required]
        [SerializeField] private RectTransform _boxTransform;

        private bool _isSelecting = false;
        private Vector2 _startPoint;

        private Vector2 _startScreenPoint;
        private Vector2 _currentScreenPoint;

        private Vector2 _leftBottomPoint;
        private Vector2 _rightTopPoint;

        private void Awake()
        {
            DragToSelect_State.OnBeginDragging += BeginDrawSelectionBox;
        }

        private void OnDestroy()
        {
            DragToSelect_State.OnBeginDragging -= BeginDrawSelectionBox;
        }

        private void Update()
        {
            if (!_isSelecting) return;

            _currentScreenPoint = Input.mousePosition;
            _leftBottomPoint = new Vector2(Mathf.Min(_startScreenPoint.x, _currentScreenPoint.x),
                Mathf.Min(_startScreenPoint.y, _currentScreenPoint.y));
            _rightTopPoint = new Vector2(Mathf.Max(_startScreenPoint.x, _currentScreenPoint.x),
                Mathf.Max(_startScreenPoint.y, _currentScreenPoint.y));

            _boxTransform.position = _leftBottomPoint;
            _boxTransform.sizeDelta =
                new Vector2(_rightTopPoint.x - _leftBottomPoint.x, _rightTopPoint.y - _leftBottomPoint.y);

            if (Input.GetMouseButtonUp(0))
            {
                _isSelecting = false;
                _selectionBox.SetActive(false);
            }

            if (Input.GetMouseButtonDown(1))
            {
                _isSelecting = false;
                _selectionBox.SetActive(false);
            }
        }

        private void BeginDrawSelectionBox(Vector2 startPoint)
        {
            _isSelecting = true;
            _startPoint = startPoint;
            _startScreenPoint = Assistant.Camera.WorldToScreenPoint(_startPoint);
            _currentScreenPoint = _startScreenPoint;
            _boxTransform.sizeDelta = Vector2.zero;
            _selectionBox.SetActive(true);
        }
    }
}