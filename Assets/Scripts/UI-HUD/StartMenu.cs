using System.Collections;
using System.Security;
using UnityEngine;

namespace Game.Asteroids {

    internal sealed class StartMenu : MonoBehaviour {
    
        [SerializeField] private GameObject _tutorial;
        [SerializeField] private GameObject _creatorName;
        [SerializeField] private GameObject _pushStart;
        [SerializeField] private float _timeToFlick;

        private Coroutine _pushStartFlicker;

        private void OnEnable() {
            GameLifeCycle.OnGameLoaded += OnMenuEnter;
            GameLifeCycle.OnGameStart += OnMenuExit;
        }

        private void OnDisable() {
            GameLifeCycle.OnGameLoaded -= OnMenuEnter;
            GameLifeCycle.OnGameStart -= OnMenuExit;
        }

        private void OnMenuEnter() {
            _creatorName.SetActive(true);
            _pushStart.SetActive(true);
            _tutorial.SetActive(true);

            _pushStartFlicker = StartCoroutine(CO_PushStartFlicker());
        }

        private IEnumerator CO_PushStartFlicker() {
            while (true) {
                _pushStart.SetActive(true);
                yield return new WaitForSeconds(_timeToFlick);
                _pushStart.SetActive(false);
                yield return new WaitForSeconds(_timeToFlick);
            }
        }

        private void OnMenuExit() {
            _creatorName.SetActive(false);
            _pushStart.SetActive(false);
            _tutorial.SetActive(false);

            StopCoroutine(_pushStartFlicker);
        }

    }
}