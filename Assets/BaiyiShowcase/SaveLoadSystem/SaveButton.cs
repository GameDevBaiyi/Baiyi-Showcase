using BaiyiShowcase.SaveLoadSystem.SaveFileManage;
using UnityEngine;
using UnityEngine.UI;

namespace BaiyiShowcase.SaveLoadSystem
{
    public class SaveButton : MonoBehaviour
    {
        private SaveManager SaveManagerProperty => SaveManager.Instance;

        private void Awake()
        {
            this.GetComponent<Button>().onClick.AddListener(SaveManagerProperty.ManualSave);
        }
    }
}