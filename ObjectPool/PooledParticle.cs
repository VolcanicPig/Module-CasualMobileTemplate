using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolcanicPig.Mobile;

namespace Game
{
    public class PooledParticle : PooledObjectBase
    {
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>(); 
        }

        private void OnEnable()
        {
            float time = _particleSystem.main.duration;
            StartCoroutine(CoRecycleAfterTime(time)); 
        }
    }
}
