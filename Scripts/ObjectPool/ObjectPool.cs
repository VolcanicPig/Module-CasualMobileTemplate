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
        private Dictionary<string, List<PooledObjectBase>> _pool = new Dictionary<string, List<PooledObjectBase>>(); 

        private void Start() 
        {
            PreCachePools(); 
        }

        private void PreCachePools()
        {
            for (int i = 0; i < objectPools.Length; i++)
            {
                List<PooledObjectBase> newPool = new List<PooledObjectBase>(); 

                for (int j = 0; j < objectPools[i].poolSize; j++)
                {
                    PooledObjectBase poolObj = Instantiate(objectPools[i].prefab, transform).GetComponent<PooledObjectBase>(); 
                    poolObj.poolKey = objectPools[i].key;
                    newPool.Add(poolObj); 
                    poolObj.gameObject.SetActive(false); 
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

            PooledObjectBase go = pooled; 
            
            go.transform.SetParent(transform); 
            go.transform.localPosition = Vector3.zero; 
            go.gameObject.SetActive(false);

            _pool[pooled.poolKey].Add(go); 
        }

        public PooledObjectBase SpawnFromPool(string key)
        {
            if(!_pool.ContainsKey(key))
            {
                Debug.LogError($"{this} Does not contain key {key} cannot spawn object"); 
                return null; 
            }

            PooledObjectBase go = _pool[key][_pool[key].Count - 1]; 
            if(go)
            {
                go.gameObject.SetActive(true); 

                _pool[key].RemoveAt(_pool[key].Count - 1); 
                return go; 
            }

            return null; 
        }

        public PooledObjectBase SpawnFromPool(string key, Transform parent, Vector3 position)
        {
            if(!_pool.ContainsKey(key))
            {
                Debug.LogError($"{this} Does not contain key {key} cannot spawn object"); 
                return null; 
            }

            if(_pool[key].Count < 0) return null; 

            PooledObjectBase go = _pool[key][_pool[key].Count - 1]; 
            if(go)
            {
                go.gameObject.SetActive(true); 
                go.transform.SetParent(parent); 
                go.transform.position = position; 

                _pool[key].RemoveAt(_pool[key].Count - 1); 
                return go; 
            }

            return null; 
        }
    }
}