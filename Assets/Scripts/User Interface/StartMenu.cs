using System.Collections;
using UnityEngine;

namespace Asteroids {

    internal sealed class StartMenu : MonoBehaviour {
    
        [SerializeField] private GameObject _tutorial;
        [SerializeField] private GameObject _creatorName;
        [SerializeField] private GameObject _pushStart;
        [SerializeField] private float _timeToFlick;

        private Coroutine _coroutine;

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

            _coroutine = StartCoroutine(CO_PushStartFlicker());
        }

        private void OnMenuExit() {
            _creatorName.SetActive(false);
            _pushStart.SetActive(false);
            _tutorial.SetActive(false);

            StopCoroutine(_coroutine);
        }

        private IEnumerator CO_PushStartFlicker() {
            while (true) {
                _pushStart.SetActive(true);
                yield return new WaitForSeconds(_timeToFlick);
                
                _pushStart.SetActive(false);
                yield return new WaitForSeconds(_timeToFlick);
            }
        }

    }
}