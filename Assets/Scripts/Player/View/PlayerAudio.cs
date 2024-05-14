using System.Collections;
using UnityEngine;

namespace Game.Asteroids {

    internal sealed class PlayerAudio : MonoBehaviour {

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _fire;
        [SerializeField] private AudioClip _move;
        [SerializeField] private float _timeToPlaySound;

        private bool _play;

        private void OnEnable() {
            _play = false;
            StartCoroutine(CO_MoveAnimation());

            PlayerAttack.OnFire += PlayFire;
            PlayerMove.OnMove += PlayMove;
        }

        private void OnDisable() {
            PlayerAttack.OnFire -= PlayFire;
            PlayerMove.OnMove -= PlayMove;
        }

        private void PlayFire() {
            _audioSource.PlayOneShot(_fire);
        }

        private void PlayMove() {
            _play = !_play;
        }

        private IEnumerator CO_MoveAnimation() {
            while (true) {
                if (_play) {
                    _audioSource.PlayOneShot(_move);
                    yield return new WaitForSeconds(_timeToPlaySound);
                }

                yield return null;
            }
        }

    }
}