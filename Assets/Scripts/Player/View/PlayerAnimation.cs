using System.Collections;
using UnityEngine;

namespace Game.Asteroids {

    internal sealed class PlayerAnimation : MonoBehaviour {

        [SerializeField] private GameObject _deathParticles;
        [SerializeField] private GameObject _fire;
        [SerializeField] private float _flickTime;

        private bool _animate;

        private void OnEnable() {
            _fire.SetActive(false);
            _animate = false;
            
            StartCoroutine(CO_Animate());

            PlayerMove.OnMove += Animate;
            PlayerData.OnLivesDecreased += DeathAnimation;
            PlayerData.OnGameOver += DeathAnimation;
        }

        private void OnDisable() {
            PlayerMove.OnMove -= Animate;
            PlayerData.OnLivesDecreased -= DeathAnimation;
            PlayerData.OnGameOver -= DeathAnimation;
        }

        public void Animate() {
            _animate = !_animate;
        }

        private IEnumerator CO_Animate() {
            while (true) {
                if (_animate) {
                    _fire.SetActive(true);
                    yield return new WaitForSeconds(_flickTime);

                    _fire.SetActive(false);
                    yield return new WaitForSeconds(_flickTime);
                }
                
                yield return null;
            }
        }

        private void DeathAnimation() {
            Instantiate(_deathParticles, transform.position, Quaternion.identity);
        }

    }
}