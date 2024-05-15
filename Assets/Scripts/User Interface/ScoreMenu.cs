using System.Collections;
using UnityEngine;
using System;
using TMPro;

namespace Asteroids {

    internal sealed class ScoreMenu : MonoBehaviour {

        public static Action OnScoreShowed;

        [SerializeField] private TextMeshProUGUI _yourScore;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _tapAnyKey;
        [SerializeField] private float _timeToFlick;

        private Coroutine _coroutine;
        private int _finalScore;

        private void OnEnable() {
            GameOverMenu.OnTimeOver += OnMenuEnter;
            GameLifeCycle.OnGameEnd += OnMenuExit;
            PlayerData.OnScoreModified += GetPlayerScore;

            OnMenuExit();
        }

        private void OnDisable() {
            GameOverMenu.OnTimeOver -= OnMenuEnter;
            GameLifeCycle.OnGameEnd -= OnMenuExit;
            PlayerData.OnScoreModified -= GetPlayerScore;
        }

        private void GetPlayerScore(int score) {
            _finalScore = score;
        }

        private void OnMenuEnter() {
            _yourScore.gameObject.SetActive(true);
            _score.gameObject.SetActive(true);

            _score.text = _finalScore.ToString();

            _coroutine = StartCoroutine(CO_PushStartFlicker());
            OnScoreShowed?.Invoke();
        }

        private void OnMenuExit() {
            _yourScore.gameObject.SetActive(false);
            _score.gameObject.SetActive(false);
            _tapAnyKey.gameObject.SetActive(false);

            if (_coroutine != null) {
                StopCoroutine(_coroutine);
            }
        }

        private IEnumerator CO_PushStartFlicker() {
            while (true) {
                _tapAnyKey.gameObject.SetActive(true);
                yield return new WaitForSeconds(_timeToFlick);
                
                _tapAnyKey.gameObject.SetActive(false);
                yield return new WaitForSeconds(_timeToFlick);
            }
        }

    }
}