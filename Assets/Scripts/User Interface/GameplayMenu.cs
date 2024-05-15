using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Asteroids {

    internal sealed class GameplayMenu : MonoBehaviour {

        [SerializeField] private List<GameObject> _playerLifes;
        [SerializeField] private TextMeshProUGUI _score;

        private void OnEnable() {
            GameLifeCycle.OnGameStart += ActivateMenu;
            PlayerData.OnGameOver += DeactivateMenu;
        
            PlayerData.OnLivesIncreased += IncreaseLives;
            PlayerData.OnLivesDecreased += DecreaseLives;
            PlayerData.OnScoreModified += RefreshScore;

            DeactivateMenu();
        }

        private void OnDisable() {
            GameLifeCycle.OnGameStart -= ActivateMenu;
            PlayerData.OnGameOver -= DeactivateMenu;
            
            PlayerData.OnLivesIncreased -= IncreaseLives;
            PlayerData.OnLivesDecreased -= DecreaseLives;
            PlayerData.OnScoreModified -= RefreshScore;
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

        private void IncreaseLives() {
            for (int i = 0; i < _playerLifes.Count; i++) {
                if (!_playerLifes[i].activeInHierarchy) {
                    _playerLifes[i].SetActive(true);
                    return;
                }
            }
        }

        private void DecreaseLives() {
            for (int i = _playerLifes.Count - 1; i > 0; i--) {
                if (_playerLifes[i].activeInHierarchy) {
                    _playerLifes[i].SetActive(false);
                    return;
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