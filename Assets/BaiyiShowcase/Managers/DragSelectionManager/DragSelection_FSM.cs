using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Managers.DragSelectionManager
{
    public class DragSelection_FSM : MonoBehaviour
    {
        private DragToSelect_State _current;

        [Required]
        public DefaultDrag _defaultDrag;
        [Required]
        public ChopWoodSelection _chopWoodSelection;
        [Required]
        public HarvestSelection _harvestSelection;
        [Required]
        public MineSelection _mineSelection;

        private void Start()
        {
            _current = _defaultDrag;
        }

        private void Update()
        {
            _current.OnUpdateState();
        }

        public void TransitionTo(DragToSelect_State targetState)
        {
            _current.OnExitState();
            targetState.OnEnterState();
            _current = targetState;
        }

        [Button]
        public void TransitionToChopWood()
        {
            TransitionTo(_chopWoodSelection);
        }
    }
}