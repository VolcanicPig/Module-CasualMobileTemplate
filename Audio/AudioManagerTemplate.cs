using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VolcanicPig.Mobile
{
    public class AudioManagerTemplate<T> : SingletonBehaviourSerialized<T>
    {
        public Dictionary<string, AudioClip> soundEffects = new Dictionary<string, AudioClip>();

        protected AudioSource source;

        private bool _soundEnabled = true; 

        public override void Awake()
        {
            base.Awake();
            source = GetComponent<AudioSource>(); 
        }

        public void SetSoundEnabled(bool isEnabled)
        {
            _soundEnabled = isEnabled; 
        }

        public void GetClipFromKey(string key, out AudioClip clip)
        {
            clip = soundEffects.ContainsKey(key) ? soundEffects[key] : null;
        }

        public void PlayClip(AudioClip clip)
        {
            if (!_soundEnabled) return;
            source.PlayOneShot(clip);
        }

        public void PlayClip(string clipKey)
        {
            if (!_soundEnabled) return; 
            GetClipFromKey(clipKey, out AudioClip clip);

            if (clip != null)
            {
                PlayClip(clip); 
            }
        }
    }
}
