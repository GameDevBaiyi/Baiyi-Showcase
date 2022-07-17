using UnityEngine;

namespace BaiyiUtilities.Singleton
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; protected set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
                return;
            }

            Destroy(this.gameObject);
        }
    }
}