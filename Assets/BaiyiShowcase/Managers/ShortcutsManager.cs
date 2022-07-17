using BaiyiShowcase.SaveLoadSystem.LoadManage;
using BaiyiShowcase.SaveLoadSystem.SaveFileManage;
using BaiyiUtilities.Singleton;
using UnityEngine;

namespace BaiyiShowcase.Managers
{
    public class ShortcutsManager : Singleton<ShortcutsManager>
    {
        [SerializeField] private KeyCode _quickSave;
        [SerializeField] private KeyCode _quickLoad;
        
        private void Update()
        {
            if (Input.GetKeyDown(_quickSave))
            {
                SaveManager.Instance.QuickSave();
            }
            
            if (Input.GetKeyDown(_quickLoad))
            {
                LoadManager.Instance.QuickLoad();
            }
        }
    }
}