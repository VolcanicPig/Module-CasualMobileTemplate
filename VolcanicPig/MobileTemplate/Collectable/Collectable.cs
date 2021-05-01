using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VolcanicPig.Collectable
{
    public interface ICollectable<T>
    {
        void Collect(T player);
        void Collect();
    }

    public abstract class Collectable : MonoBehaviour, ICollectable<GameObject>
    {
        [SerializeField] private GameObject pickupEffect;

        public virtual void Collect(GameObject player)
        {
            SpawnPickupEffect();
            Destroy(gameObject);
        }

        public virtual void Collect()
        {
            SpawnPickupEffect();
            Destroy(gameObject);
        }

        public virtual void SpawnPickupEffect()
        {
            if (!pickupEffect) return;
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
        }
    }
}
