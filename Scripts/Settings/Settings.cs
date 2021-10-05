using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VolcanicPig
{
    public class Settings
    {
        private bool _hapticsEnabled = true;
        public bool HapticsEnabled => _hapticsEnabled;

        private bool _soundEnabled = true;
        public bool SoundEnabled => _soundEnabled;

        private const string _kHapticsEnabled = "hapticsEnabled";
        private const string _kSoundEnabled = "soundEnabled";

        public void Load()
        {
            _hapticsEnabled = PlayerPrefs.GetInt(_kHapticsEnabled, 1) == 1;
            _soundEnabled = PlayerPrefs.GetInt(_kSoundEnabled, 1) == 1; 
        }

        public void Save()
        {
            PlayerPrefs.SetInt(_kHapticsEnabled, _hapticsEnabled ? 1 : 0);
            PlayerPrefs.SetInt(_kSoundEnabled, _soundEnabled ? 1 : 0);
        }

        public void ToggleSoundEnabled()
        {
            _soundEnabled = !_soundEnabled;
        }

        public void ToggleHapticsEnabled()
        {
            _hapticsEnabled = !_hapticsEnabled; 
        }
    }
}
