using System;
using BaiyiUtilities;
using Sirenix.OdinInspector;
using UnityEditorInternal;
using UnityEngine;

namespace BaiyiShowcase.Managers.DragSelectionManager
{
    public class DragToSelect_State : MonoBehaviour
    {
        private Vector2 _startPoint;
        private Vector2 _currentPoint;

        [Required]
        [SerializeField] private DragSelection_FSM _fsm;
        [Required]
        [SerializeField] private Sprite _cursorSprite;

        [SerializeField] protected LayerMask _layerMask;

        public static event Action<Vector2> OnBeginDragging;
        public static event Action<Sprite> OnEnterDragState;


        public virtual void OnEnterState()
        {
            OnEnterDragState?.Invoke(_cursorSprite);
        }

        public virtual void OnUpdateState()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startPoint = Assistant.Camera.ScreenToWorldPoint(Input.mousePosition);
                _currentPoint = _startPoint;
                OnBeginDragging?.Invoke(_startPoint);
            }

            if (Input.GetMouseButton(0))
            {
                _currentPoint = Assistant.Camera.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                Collider2D[] collider2Ds = Physics2D.OverlapAreaAll(_startPoint, _currentPoint, _layerMask);
                DoWithRaycastHit2DArray(collider2Ds);
            }

            if (Input.GetMouseButtonDown(1))
            {
                _fsm.TransitionTo(_fsm._defaultDrag);
            }
        }

        public virtual void OnExitState()
        {
        }

        protected virtual void DoWithRaycastHit2DArray(Collider2D[] collider2Ds)
        {
            //比如要将选中的Tree加入到伐木指令中, 设置好_layerMask, 然后Foreach查找Tree Component.
        }
    }
}