using System.Collections;
using UnityEngine;
using System;

namespace Game.Asteroids {

    internal sealed class PlayerRotate : MonoBehaviour {

        public static Action<Quaternion> OnPlayerDeath;

        [SerializeField] private float _speed = 250.0f;

        private Quaternion _currentRotation;
        private float _direction;        
        private bool _rotate;

        private void OnEnable() {
            _rotate = false;
            StartCoroutine(CO_Rotate());
        }

        private void OnDisable() {  
            OnPlayerDeath?.Invoke(_currentRotation);
        }

        public void IsRotating(float direction) {
            _direction = direction;
            _rotate = !_rotate;
        }

        private IEnumerator CO_Rotate() {
            while (true) {
                if (_rotate) {
                    float rotationAmount = -_direction * _speed * Time.deltaTime;

                    transform.Rotate(Vector3.forward, rotationAmount);
                }

                _currentRotation = transform.rotation;

                yield return null;
            }
        }

    }
}