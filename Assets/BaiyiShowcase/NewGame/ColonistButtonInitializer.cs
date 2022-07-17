using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BaiyiShowcase.NewGame
{
    public class ColonistButtonInitializer : MonoBehaviour
    {
        [Required]
        [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
        private NewGameSettings2UI NewGameSettings2UIProperty => NewGameSettings2UI.Instance;

        public int correspondingColonistIndex;

        private void Awake()
        {
            this.GetComponent<Button>().onClick.AddListener(HandleOnclick);
        }

        public void Initialize(string colonistName, int index)
        {
            _textMeshProUGUI.text = colonistName;
            this.correspondingColonistIndex = index;
        }

        private void HandleOnclick()
        {
            NewGameSettings2UIProperty.currentColonistIndex = correspondingColonistIndex;
            NewGameSettings2UIProperty.UpdateUI();
        }
    }
}