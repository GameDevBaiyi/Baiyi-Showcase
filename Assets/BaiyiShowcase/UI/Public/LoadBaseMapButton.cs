using BaiyiShowcase.GameDesign;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BaiyiShowcase.UI.Public
{
    public class LoadBaseMapButton : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;

        private void Awake()
        {
            this.GetComponent<Button>().onClick.AddListener(HandleClick);
        }

        private void HandleClick()
        {
            SceneManager.LoadScene(_gameDesignSO.commonDesign.baseMapSceneIndex);
        }
    }
}