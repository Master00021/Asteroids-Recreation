using UnityEngine;
using System;

namespace Game.Asteroids {

    internal sealed class GameLifeCycle : MonoBehaviour {

        public static Action OnGameLoaded;
        public static Action OnGameStart;
        public static Action OnGameStop;
        public static Action OnGameEnd;

        private void OnEnable() {
            GameInput.OnGameStart += StartGame;
            GameInput.OnGameStop += StopGame;
            GameInput.OnGameEnded += EndGame;
        }

        private void OnDisable() {
            GameInput.OnGameStart -= StartGame;
            GameInput.OnGameStop -= StopGame;
            GameInput.OnGameEnded -= EndGame;
        }

        private void Start() {
            OnGameLoaded?.Invoke();
        }

        private void StartGame() {
            OnGameStart?.Invoke();
        }

        private void StopGame() {
            OnGameStop?.Invoke();
        }

        private void EndGame() {
            OnGameEnd?.Invoke();
            Start();
        }

    }
}

