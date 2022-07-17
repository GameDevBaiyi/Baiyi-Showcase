using TMPro;
using UnityEngine;

namespace BaiyiShowcase.UI.TooltipSystem
{
    public class Tooltip : MonoBehaviour
    {
        public ReplacementData[] replacementData;
        public TooltipType tooltipType;

        private void Start()
        {
            foreach (ReplacementData data in replacementData)
            {
                data._initialText = data.textMeshProUGUI.text;
            }
            
            //When finished initialization, hide itself.
            this.gameObject.SetActive(false);
        }
    }

    [System.Serializable]
    public class ReplacementData
    {
        public string name;
        public TextMeshProUGUI textMeshProUGUI;
        public string[] textToBeReplaced;

        [HideInInspector] public string _initialText;
    }
}