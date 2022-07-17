using UnityEngine;

namespace BaiyiUtilities.FSM
{
    public class FSMTemplate : MonoBehaviour
    {
        private StateTemplate _currentStateTemplate;

        private void Update()
        {
            if (!_currentStateTemplate) return;

            _currentStateTemplate.OnUpdateState();
        }

        public void TransitionTo(StateTemplate targetStateTemplate)
        {
            _currentStateTemplate.OnExitState();
            targetStateTemplate.OnEnterState();
            _currentStateTemplate = targetStateTemplate;
        }
    }
}