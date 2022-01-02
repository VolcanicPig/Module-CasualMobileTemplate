using System;
using System.Collections;
using UnityEngine;
using VolcanicPig.Utilities;

namespace VolcanicPig.Mobile
{
    public enum GameState
    {
        Start,
        InGame,
        End
    }

    public enum WinState
    {
        Win, Lose
    }

    public class MobileGameManagerTemplate<T> : SingletonBehaviour<T>
    {
        public static Action<int, int> OnCurrencyChanged;
        public static Action<GameState> OnGameStateChanged;

        [Header("Scenes")]
        [SerializeField] string gameScene = "GameScene";
        [SerializeField] string uiScene = "UiScene";
        private int _initScenesToLoad = 0;
        private int _initScenesLoaded = 0;

        [Header("Player")]
        [SerializeField] private GameObject playerPrefab;

        private const string KCurrency = "Currency";
        private const string KLevel = "Level"; 

        private GameObject _currentPlayerRef;
        public GameObject GetCurrentPlayer => _currentPlayerRef;

        private GameState _gameState;
        public GameState GetGameState => _gameState;

        private WinState _winState;
        public WinState GetWinState => _winState;

        private int _currency = 0;
        public int Currency
        {
            get { return _currency; }
            set
            {
                OnCurrencyChanged?.Invoke(_currency, value);
                _currency = value;
                PlayerPrefs.SetInt(KCurrency, value); 
            }
        }

        private int _level; 
        public int Level
        {
            get { return _level; }
            set
            {
                _level = value; 
                PlayerPrefs.SetInt(KLevel, value); 
            }
        }

        public Settings Settings; 

        private void Start()
        {
            LoadData(); 
            LoadScenes();
        }

        protected virtual void LoadData()
        {
            Settings = new Settings(); 
            Settings.Load();
            
            _level = PlayerPrefs.GetInt(KLevel);
            _currency = PlayerPrefs.GetInt(KCurrency); 
        }

        public void LoadScenes()
        {
            if (!SceneLoader.Instance)
            {
                Debug.LogError($"{this} : There is no scene loader in the scene");
                return;
            }

            SceneLoader sceneLoader = SceneLoader.Instance;

            _initScenesLoaded = 0;

            if (gameScene != null)
            {
                sceneLoader.LoadScene(gameScene, true, true, SceneLoaded);
                _initScenesToLoad++;
            }

            if (uiScene != null)
            {
                sceneLoader.LoadScene(uiScene, true, true, SceneLoaded);
                _initScenesToLoad++;
            }
        }

        private void SceneLoaded()
        {
            _initScenesLoaded++;
            if (_initScenesLoaded >= _initScenesToLoad)
            {
                ScenesFullyLoaded();
            }
        }

        private void ScenesFullyLoaded()
        {
            SpawnPlayer();
            OnGameStarted(); 
            ChangeState(GameState.Start);
        }

        protected virtual void SpawnPlayer()
        {
            if (playerPrefab)
            {
                _currentPlayerRef = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            }
        }

        public void StartGame()
        {
            ChangeState(GameState.InGame);
        }

        public void EndGame(bool won)
        {
            if(_gameState != GameState.InGame) 
            {
                Debug.LogError($"Cannot end the game whilst in State : {_gameState}"); 
                return; 
            }

            _winState = won ? WinState.Win : WinState.Lose;
            ChangeState(GameState.End);
            OnGameEnded(); 

            if(won) Level++; 
        }

        public void RestartGame()
        {
            if(_gameState != GameState.End) 
            {
                Debug.LogError($"Cannot restart the game whilst in State : {_gameState}"); 
                return; 
            }

            if (_currentPlayerRef) Destroy(_currentPlayerRef);

            SpawnPlayer();
            ChangeState(GameState.Start);
            OnGameRestarted();
        }

        private void ChangeState(GameState state)
        {
            Debug.Log($"Game State Changed :: {state}");
            
            _gameState = state;

            OnGameStateChanged?.Invoke(state);
        }

        protected virtual void OnGameStarted()
        { }

        protected virtual void OnGameEnded()
        { }

        protected virtual void OnGameRestarted()
        { }

        private void OnApplicationQuit()
        {
            Settings.Save();   
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            Settings.Save();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            Settings.Save();
        }
    }
}
