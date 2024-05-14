using System.Collections;
using UnityEngine;
using System;
using TMPro;

namespace Game.Asteroids {

    internal sealed class ScoreMenu : MonoBehaviour {

        public static Action OnScoreShowed;

        [SerializeField] private GameObject _yourScore;
        [SerializeField] private GameObject _score;
        [SerializeField] private GameObject _tapAnyKey;
        [SerializeField] private float _timeToFlick;

        private Coroutine _pushStartFlicker;
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
            _yourScore.SetActive(true);
            _score.SetActive(true);

            _score.GetComponent<TextMeshProUGUI>().text = _finalScore.ToString();

            _pushStartFlicker = StartCoroutine(CO_PushStartFlicker());
            OnScoreShowed?.Invoke();
        }

        private void OnMenuExit() {
            _yourScore.SetActive(false);
            _score.SetActive(false);
            _tapAnyKey.SetActive(false);

            if (_pushStartFlicker != null) {
                StopCoroutine(_pushStartFlicker);
            }
        }

        private IEnumerator CO_PushStartFlicker() {
            while (true) {
                _tapAnyKey.SetActive(true);
                yield return new WaitForSeconds(_timeToFlick);
                _tapAnyKey.SetActive(false);
                yield return new WaitForSeconds(_timeToFlick);
            }
        }

    }
}