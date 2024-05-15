using System.Collections;
using UnityEngine;
using System;

namespace Game.Asteroids {

    internal sealed class EnemyAttack : MonoBehaviour {

        public static Action OnAttack;
        
        [Header("Bullet")]
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _bulletSpawn;
        [SerializeField] private Transform _pivot;

        [Header("Configuration")]
        [SerializeField] private float _timeToAttack;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _minRotationDirectionTime;
        [SerializeField] private float _maxRotationDirectionTime;
        
        private float _timeDirectionDuration;
        private bool _rightDirection;

        private void Start() {
            StartCoroutine(CO_Attack());
            StartCoroutine(CO_Rotate());
            StartCoroutine(CO_ChangeDirection());
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
                    _timeDirectionDuration = UnityEngine.Random.Range(_minRotationDirectionTime, _maxRotationDirectionTime);
                }

                yield return null;
            }
        }

    }
}