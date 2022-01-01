using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector; 
using UnityEngine;

namespace VolcanicPig
{
    public abstract class SingletonBehaviourSerialized<T> : SerializedMonoBehaviour
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
