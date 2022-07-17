using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

namespace BaiyiShowcase.UI.TooltipSystem
{
    public struct TooltipReferences
    {
        public RectTransform tooltipTransform;
        public GameObject tooltipGameObject;
        public Tooltip tooltip;
    }

    public class TooltipSystem : MonoBehaviour
    {
        private static Transform _tooltipCanvas;
        private static TooltipReferences[] _tooltipReferencesArray;
        private static Dictionary<TooltipType, TooltipReferences> _tooltipTypeAndTooltipReferences;
        private static TooltipReferences _currentTooltip;


        private static TextMeshProUGUI _textMeshProUGUI;
        private static readonly StringBuilder _stringBuilder = new StringBuilder();

        private static float _screenHalfWidth;
        private static float _screenHalfHeight;

        private void Start()
        {
            _tooltipCanvas = this.transform;
            int tooltipCount = _tooltipCanvas.childCount;
            _tooltipReferencesArray = new TooltipReferences[tooltipCount];
            for (int i = 0; i < tooltipCount; i++)
            {
                Transform currentTooltipTransform = _tooltipCanvas.GetChild(i);
                _tooltipReferencesArray[i].tooltipTransform = currentTooltipTransform as RectTransform;
                _tooltipReferencesArray[i].tooltipGameObject = currentTooltipTransform.gameObject;
                _tooltipReferencesArray[i].tooltip = currentTooltipTransform.GetComponent<Tooltip>();
            }

            _tooltipTypeAndTooltipReferences = _tooltipReferencesArray.ToDictionary(t => t.tooltip.tooltipType);

            _screenHalfWidth = Screen.width / 2f;
            _screenHalfHeight = Screen.height / 2f;
        }

        public static void Show(TooltipType tooltipType, string[] info)
        {
            _currentTooltip = _tooltipTypeAndTooltipReferences[tooltipType];

            _currentTooltip.tooltipGameObject.SetActive(true);
            SetTooltipPosition();

            int infoIndex = 0;
            foreach (ReplacementData replacementData in _currentTooltip.tooltip.replacementData)
            {
                _textMeshProUGUI = replacementData.textMeshProUGUI;
                _stringBuilder.Append(replacementData._initialText);
                foreach (string s in replacementData.textToBeReplaced)
                {
                    if (infoIndex == info.Length) break;
                    _stringBuilder.Replace(s, info[infoIndex]);
                    infoIndex++;
                }

                _textMeshProUGUI.text = _stringBuilder.ToString();
                _stringBuilder.Clear();
            }
        }

        public static void Hide()
        {
            _currentTooltip.tooltipGameObject.SetActive(false);
        }

        private static void SetTooltipPosition()
        {
            Vector3 mousePosition = Input.mousePosition;
            _currentTooltip.tooltipTransform.position = mousePosition;
            _currentTooltip.tooltipTransform.pivot = GetModifiedPivot(mousePosition);
        }

        private static Vector2 GetModifiedPivot(Vector2 mousePosition)
        {
            return new Vector2(mousePosition.x < _screenHalfWidth ? 0 : 1, mousePosition.y < _screenHalfHeight ? 0 : 1);
        }
    }
}