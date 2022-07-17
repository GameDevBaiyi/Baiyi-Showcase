using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaiyiUtilities.Singleton
{
    public class PersistentSingletonParent : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += DestroyEmptyParent;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= DestroyEmptyParent;
        }

        private void DestroyEmptyParent(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (this.transform.childCount == 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}