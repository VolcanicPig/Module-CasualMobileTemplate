using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class AlertObservers : MonoBehaviour
    {
        private readonly List<Action> _callbacks = new List<Action>();

        public void AddObserver(Action callback)
        {
            _callbacks.Add(callback);
        }
        
        public void Alert()
        {
            foreach (Action action in _callbacks)
            {
                action?.Invoke();
            }
            
            _callbacks.Clear();
        }

        public void ClearObservers()
        {
            _callbacks.Clear();
        }
    }
}