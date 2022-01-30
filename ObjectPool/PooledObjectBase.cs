using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace VolcanicPig.Mobile
{
    public abstract class PooledObjectBase : MonoBehaviour
    {
        public string poolKey; 

        public virtual void Recycle()
        {
            ObjectPool.Instance.RecycleObject(this); 
        }

        protected IEnumerator CoRecycleAfterTime(float time)
        {
            yield return Helpers.GetWait(time); 
            Recycle();
        }
    }
}
