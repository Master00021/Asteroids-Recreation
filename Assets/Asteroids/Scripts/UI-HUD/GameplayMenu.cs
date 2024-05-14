using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Asteroids {

    internal sealed class GameplayMenu : MonoBehaviour {

        [SerializeField] private List<GameObject> _playerLifes;
        [SerializeField] private GameObject _life;
        [SerializeField] private TextMeshProUGUI _score;

        private void OnEnable() {
            GameLifeCycle.OnGameStart += ActivateMenu;
            PlayerData.OnLivesModified += RefreshLives;
            PlayerData.OnScoreModified += RefreshScore;
            PlayerData.OnGameOver += DeactivateMenu;

            DeactivateMenu();
        }

        private void OnDisable() {
            GameLifeCycle.OnGameStart -= ActivateMenu;
            PlayerData.OnLivesModified -= RefreshLives;
            PlayerData.OnScoreModified -= RefreshScore;
            PlayerData.OnGameOver -= DeactivateMenu;
        }

        private void ActivateMenu() {
            _score.gameObject.SetActive(true);
        }

        private void DeactivateMenu() {
            for (int i = 0; i < _playerLifes.Count; i++) {
                _playerLifes[i].SetActive(false);
            }
            _score.gameObject.SetActive(false);
        }

        private void RefreshLives(int playerLives) {
            for (int currentLive = 0; currentLive < _playerLifes.Count; currentLive++) {
                bool currentLiveActive = _playerLifes[currentLive].activeInHierarchy;

                if (currentLiveActive && currentLive >= playerLives) {
                    _playerLifes[currentLive].SetActive(false);
                }
                else if (!currentLiveActive && currentLive < playerLives) {
                    _playerLifes[currentLive].SetActive(true);
                }
            }
        }

        private void RefreshScore(int score) {
            if (score == 0) {
                _score.text = "00";
            }
            else {
                _score.text = score.ToString();
            }
        }

    }
}

