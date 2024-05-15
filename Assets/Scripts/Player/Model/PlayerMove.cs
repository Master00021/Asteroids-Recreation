using UnityEngine;
using System;

namespace Asteroids {

    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class PlayerMove : MonoBehaviour {

        public static Action OnMove;

        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _maxSpeed = 12.0f;
        [SerializeField] private float _speed = 10.0f;
        
        private bool _move;

        private void OnEnable() {
            _move = false;
        }

        private void Update() {
            if (_move) {
                float moveAmount = _speed * Time.deltaTime;

                _rigidbody.AddForce(transform.up * moveAmount, ForceMode2D.Impulse);

                if (_rigidbody.velocity.magnitude > _maxSpeed) {
                    _rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;
                }
            }
        }

        public void Move() {
            _move = !_move;
            OnMove?.Invoke();
        }

    }
}