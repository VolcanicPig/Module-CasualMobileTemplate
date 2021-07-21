using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VolcanicPig.Utilities
{
    public class SceneLoader : SingletonBehaviour<SceneLoader>
    {
        private Dictionary<string, List<Action>> _sceneLoadedCallbacks = new Dictionary<string, List<Action>>();

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (_sceneLoadedCallbacks.Count < 1) return;
            if (_sceneLoadedCallbacks.ContainsKey(scene.name))
            {
                if (_sceneLoadedCallbacks[scene.name].Count < 1) return;

                for (int i = _sceneLoadedCallbacks[scene.name].Count - 1; i >= 0; i--)
                {
                    _sceneLoadedCallbacks[scene.name][i].Invoke();
                    _sceneLoadedCallbacks[scene.name].RemoveAt(i);
                }
            }
        }

        private void AddSceneLoadedCallback(string sceneName, Action callback)
        {
            if (!_sceneLoadedCallbacks.ContainsKey(sceneName))
            {
                _sceneLoadedCallbacks.Add(sceneName, new List<Action>());
            }

            _sceneLoadedCallbacks[sceneName].Add(callback);
        }

        public void LoadScene(string scene, bool async = false, bool additive = false, Action callback = null)
        {
            if (async)
            {
                SceneManager.LoadSceneAsync(scene, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene(scene, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
            }

            if (callback != null)
            {
                AddSceneLoadedCallback(scene, callback);
            }
        }

        public void LoadScene(Scene scene, bool async = false, bool additive = false, Action callback = null)
        {
            if (async)
            {
                SceneManager.LoadSceneAsync(scene.buildIndex, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene(scene.buildIndex, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
            }

            if (callback != null)
            {
                AddSceneLoadedCallback(scene.name, callback);
            }
        }

        public void LoadScene(int scene, bool async = false, bool additive = false, Action callback = null)
        {
            if (async)
            {
                SceneManager.LoadSceneAsync(scene, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene(scene, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
            }

            if (callback != null)
            {
                AddSceneLoadedCallback(SceneManager.GetSceneByBuildIndex(scene).name, callback);
            }
        }
    }
}
