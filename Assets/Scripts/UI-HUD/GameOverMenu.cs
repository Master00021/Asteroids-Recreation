using System.Collections;
using UnityEngine;
using System;

namespace Game.Asteroids {

    internal sealed class GameOverMenu : MonoBehaviour {

        public static Action OnTimeOver;

        [SerializeField] private GameObject _gameOver;
        [SerializeField] private float _timeForNextUI;

        private Coroutine _waitForNextUI;

        private void OnEnable() {
            PlayerData.OnGameOver += OnMenuEnter;

            OnMenuExit();
        }

        private void OnDisable() {
            PlayerData.OnGameOver -= OnMenuEnter;
        }

        private void OnMenuEnter() {
            _gameOver.SetActive(true);

            _waitForNextUI = StartCoroutine(CO_WaitForNextUI());
        }

        private void OnMenuExit() {
            _gameOver.SetActive(false);

            if (_waitForNextUI != null) {
                StopCoroutine(_waitForNextUI);
            }
        }

        private IEnumerator CO_WaitForNextUI() {
            while (true) {
                yield return new WaitForSeconds(_timeForNextUI);

                OnMenuExit();
                OnTimeOver?.Invoke();
            }
        }

    }
}