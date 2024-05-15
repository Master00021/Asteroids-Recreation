using System.Collections;
using UnityEngine;
using System;

namespace Asteroids {

    internal sealed class GameOverMenu : MonoBehaviour {

        public static Action OnTimeOver;

        [SerializeField] private GameObject _gameOver;
        [SerializeField] private float _timeForNextUI;

        private Coroutine _coroutine;

        private void OnEnable() {
            PlayerData.OnGameOver += OnMenuEnter;

            OnMenuExit();
        }

        private void OnDisable() {
            PlayerData.OnGameOver -= OnMenuEnter;
        }

        private void OnMenuEnter() {
            _gameOver.SetActive(true);

            _coroutine = StartCoroutine(CO_WaitForNextUI());
        }

        private void OnMenuExit() {
            _gameOver.SetActive(false);

            if (_coroutine != null) {
                StopCoroutine(_coroutine);
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