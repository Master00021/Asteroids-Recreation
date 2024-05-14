using UnityEngine;
using System;

namespace Game.Asteroids {

    internal sealed class PlayerData : MonoBehaviour {

        public static Action<int> OnScoreModified;
        public static Action<int> OnLivesModified;
        public static Action OnLivesIncreased;
        public static Action OnLivesDecreased;
        public static Action OnGameOver;

        [SerializeField] private int _startLifes;
        
        private int _lives;
        private int _score;

        private void OnEnable() {
            GameLifeCycle.OnGameStart += OnGameStart;
            PlayerController.OnPlayerDeath += DecreaseLives;

            NormalEnemy.OnScoreSent += ModifyScore;

            LiveEnemy.OnScoreSent += ModifyScore;
            LiveEnemy.OnLiveSent += IncreaseLives;

            Asteroid.OnScoreSent += ModifyScore;

            _lives = _startLifes;
            _score = 0;
        }   

        private void OnDisable() {
            GameLifeCycle.OnGameStart -= OnGameStart;
            PlayerController.OnPlayerDeath -= DecreaseLives;

            NormalEnemy.OnScoreSent -= ModifyScore;

            LiveEnemy.OnScoreSent -= ModifyScore;
            LiveEnemy.OnLiveSent -= IncreaseLives;

            Asteroid.OnScoreSent -= ModifyScore;
        }

        private void OnGameStart() {
            _score = 0;
            _lives = 0;

            ModifyScore(_score);

            for (int i = 0; i < _startLifes; i++) {
                IncreaseLives();
            }
        }

        internal void IncreaseLives() {
            _lives++;

            OnLivesIncreased?.Invoke();
            OnLivesModified?.Invoke(_lives);
        }

        internal void DecreaseLives() {
            _lives--;

            if (_lives <= 0) {
                OnGameOver?.Invoke();
                return;
            }

            OnLivesDecreased?.Invoke();
            OnLivesModified?.Invoke(_lives);
        }

        internal void ModifyScore(int score) {
            _score += score;

            OnScoreModified?.Invoke(_score);
        }

    }
}

