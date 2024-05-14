using System.Collections;
using UnityEngine;
using System;

namespace Game.Asteroids {

    internal sealed class EnemyAttack : MonoBehaviour {

        public static Action OnAttack;
        
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _minTimeDirectionDuration;
        [SerializeField] private float _maxTimeDirectionDuration;
        [SerializeField] private Transform _bulletSpawn;
        [SerializeField] private Transform _pivot;
        
        [SerializeField] private float _timeToAttack;
        [SerializeField] private GameObject _bullet;

        private float _timeDirectionDuration;
        private bool _rightDirection;

        private void Start() {
            StartCoroutine(CO_Rotate());
            StartCoroutine(CO_ChangeDirection());
            StartCoroutine(CO_Attack());
        }

        private IEnumerator CO_Attack() {
            while (true) {
                Instantiate(_bullet, _bulletSpawn.position, _bulletSpawn.rotation);
                OnAttack?.Invoke();

                yield return new WaitForSeconds(_timeToAttack);

                yield return null;
            }
        }

        private IEnumerator CO_Rotate() {
            while (true) {
                if (_rightDirection) {
                    _pivot.rotation *= Quaternion.Euler(0.0f, 0.0f, _rotationSpeed * Time.deltaTime);
                }
                else {
                    _pivot.rotation *= Quaternion.Euler(0.0f, 0.0f, -_rotationSpeed * Time.deltaTime);
                }
                
                yield return null;
            }
        }

        private IEnumerator CO_ChangeDirection() {
            while (true) {
                _timeDirectionDuration -= Time.deltaTime;

                if (_timeDirectionDuration <= 0.0f) {
                    _rightDirection = !_rightDirection;
                    RandomNumber();
                }

                yield return null;
            }
        }

        private void RandomNumber() {
            _timeDirectionDuration = UnityEngine.Random.Range(_minTimeDirectionDuration, _maxTimeDirectionDuration);
        }

    }
}

