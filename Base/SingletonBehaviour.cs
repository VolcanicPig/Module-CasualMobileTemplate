using UnityEngine;

namespace VolcanicPig
{
    public abstract class SingletonBehaviour<T> : MonoBehaviour
    {
        private static T _Instance;

        public static T Instance
        {
            get { return _Instance; }
        }

        [SerializeField] private bool dontDestroyOnLoad = true;

        public virtual void Awake()
        {
            if (Instance == null)
            {
                _Instance = gameObject.GetComponent<T>();
            }
            else
            {
                Destroy(gameObject);
            }

            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}