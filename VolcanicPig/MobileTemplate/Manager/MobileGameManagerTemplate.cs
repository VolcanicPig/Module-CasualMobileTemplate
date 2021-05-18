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
        public static Action<double> OnCurrencyChanged;
        public static Action<GameState> OnGameStateChanged;


        [Header("Scenes")]
        [SerializeField] string gameScene = "GameScene";
        [SerializeField] string uiScene = "UiScene";
        private int _initScenesToLoad = 0;
        private int _initScenesLoaded = 0;

        [Header("Player")]
        [SerializeField] private GameObject playerPrefab;

        private GameObject _currentPlayerRef;
        public GameObject GetCurrentPlayer => _currentPlayerRef;

        private GameState _gameState;
        public GameState GetGameState => _gameState;

        private WinState _winState;
        public WinState GetWinState => _winState;

        private double _currency = 0;
        public double Currency
        {
            get { return _currency; }
            set
            {
                _currency = value;
                OnCurrencyChanged?.Invoke(_currency);
            }
        }

        private void Start()
        {
            LoadScenes();
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
            if(playerPrefab)
            {
                _currentPlayerRef = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            }

            ChangeState(GameState.Start);
        }

        public void StartGame()
        {
            ChangeState(GameState.InGame);
        }

        public void EndGame(bool won)
        {
            _winState = won ? WinState.Win : WinState.Lose;
            ChangeState(GameState.End);
        }

        public void RestartGame()
        {
            if (_currentPlayerRef) Destroy(_currentPlayerRef);

            if(playerPrefab)
            {
                _currentPlayerRef = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            }
            
            ChangeState(GameState.Start);
        }

        private void ChangeState(GameState state)
        {
            Debug.Log($"Game State Changed :: {state}");

            switch (state)
            {
                case GameState.Start:
                    Ui.UiManager.Instance.OpenMenu("Start");
                    break;
                case GameState.InGame:
                    Ui.UiManager.Instance.OpenMenu("InGame");
                    break;
                case GameState.End:
                    Ui.UiManager.Instance.OpenMenu("End");
                    break;
            }

            OnGameStateChanged?.Invoke(state);
        }
    }
}
