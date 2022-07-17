using UnityEngine;

namespace BaiyiShowcase.UI.TooltipSystem
{
    public abstract class TooltipTrigger : MonoBehaviour
    {
        private const float Delay = 0.5f;
        private float _timer;
        private bool _hasShowed;

        protected void OnMouseOver()
        {
            if (_hasShowed) return;

            _timer += Time.deltaTime;
            if (_timer > Delay)
            {
                TooltipType tooltipType = GetTooltipType();
                string[] info = GetInfo();

                TooltipSystem.Show(tooltipType, info);
                _hasShowed = true;
                _timer = 0f;
            }
        }

        protected void OnMouseExit()
        {
            if (!_hasShowed) return;
            TooltipSystem.Hide();
            _hasShowed = false;
        }

        protected abstract TooltipType GetTooltipType();
        protected abstract string[] GetInfo();
    }
}