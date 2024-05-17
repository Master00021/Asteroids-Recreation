using UnityEngine;
using System;

namespace Asteroids {

    internal sealed class PlayerRotate : MonoBehaviour {

        public static Action<Quaternion> OnPlayerDeath;

        [SerializeField] private float _speed = 250.0f;

        private Quaternion _currentRotation;
        private float _direction;        

        private void OnDisable() {  
            OnPlayerDeath?.Invoke(_currentRotation);
        }

        private void Update() {
            float rotationAmount = -_direction * _speed * Time.deltaTime;
            transform.Rotate(Vector3.forward, rotationAmount);
            _currentRotation = transform.rotation;
        }

        public void GetRotationDirection(float direction) {
            _direction = direction;
        }

    }
}