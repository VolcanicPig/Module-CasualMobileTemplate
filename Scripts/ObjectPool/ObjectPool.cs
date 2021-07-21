using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VolcanicPig.Mobile
{
    [System.Serializable]
    public class Pool 
    {
        public string key; 
        public GameObject prefab; 
        public int poolSize; 
    }

    public class ObjectPool : SingletonBehaviour<ObjectPool>
    {
        [SerializeField] Pool[] objectPools; 
        private Dictionary<string, List<GameObject>> _pool = new Dictionary<string, List<GameObject>>(); 

        private void Start() 
        {
            PreCachePools(); 
        }

        private void PreCachePools()
        {
            for (int i = 0; i < objectPools.Length; i++)
            {
                List<GameObject> newPool = new List<GameObject>(); 

                for (int j = 0; j < objectPools[i].poolSize; j++)
                {
                    GameObject poolObj = Instantiate(objectPools[i].prefab, transform); 
                    newPool.Add(poolObj); 
                    poolObj.SetActive(false); 
                }

                _pool.Add(objectPools[i].key, newPool); 
            }
        }    

        public void RecycleObject(PooledObjectBase pooled)
        {
            if(!_pool.ContainsKey(pooled.poolKey))
            {
                Debug.LogError($"{this} Does not contain key {pooled.poolKey} cannot recycle object"); 
                return; 
            }

            GameObject go = pooled.gameObject; 
            
            go.transform.SetParent(transform); 
            go.transform.localPosition = Vector3.zero; 
            go.SetActive(false);

            _pool[pooled.poolKey].Add(go); 
        }

        public GameObject SpawnFromPool(string key)
        {
            if(!_pool.ContainsKey(key))
            {
                Debug.LogError($"{this} Does not contain key {key} cannot spawn object"); 
                return null; 
            }

            GameObject go = _pool[key][_pool[key].Count - 1]; 
            if(go)
            {
                go.SetActive(true); 

                _pool[key].RemoveAt(_pool[key].Count - 1); 
                return go; 
            }

            return null; 
        }
        public GameObject SpawnFromPool(string key, Transform parent, Vector3 position)
        {
            if(!_pool.ContainsKey(key))
            {
                Debug.LogError($"{this} Does not contain key {key} cannot spawn object"); 
                return null; 
            }

            GameObject go = _pool[key][_pool[key].Count - 1]; 
            if(go)
            {
                go.SetActive(true); 
                go.transform.SetParent(parent); 
                go.transform.position = position; 

                _pool[key].RemoveAt(_pool[key].Count - 1); 
                return go; 
            }

            return null; 
        }
    }
}