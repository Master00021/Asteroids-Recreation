using System.Collections;
using UnityEngine;

namespace Game.Asteroids {

    internal enum SpawnLocation {
        Right,
        Left
    }

    internal sealed class EnemyMove : MonoBehaviour {
        
        [SerializeField] private bool _normalEnemy;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;

        internal SpawnLocation _spawnLocation;
        private Vector3 _moveDirection;
        private float _speed;

        private void Start() {
            if (_spawnLocation == SpawnLocation.Right) {
                _moveDirection = transform.right;
            }
            else {
                _moveDirection = -transform.right;
            }

            _speed = Random.Range(_minSpeed, _maxSpeed);

            StartCoroutine(CO_HorizontalMove());

            if (_normalEnemy) {
                StartCoroutine(CO_VerticalMove());                
            }
        }

        private IEnumerator CO_HorizontalMove() {
            while (true) {
                transform.position += _speed * Time.deltaTime * _moveDirection;
                yield return null;
            }
        }

        private IEnumerator CO_VerticalMove() {
            bool playCoroutine = true;
            int count = 0;

            while (playCoroutine) {
                count++;
                float timeToChangeMovement = Random.Range(1.0f, 2.0f);
                bool randomBool = Random.value > 0.5f;  
                var rotateDirection = Vector2.zero;

                if (randomBool) {
                    rotateDirection = new Vector2(0.0f, -45.0f);
                }
                else {
                    rotateDirection = new Vector2(0.0f, 45.0f);
                }

                yield return new WaitForSeconds(timeToChangeMovement);

                _rigidbody.AddForce(rotateDirection);

                yield return new WaitForSeconds(timeToChangeMovement);

                _rigidbody.velocity = Vector3.zero;

                if (count >= 2) {
                    playCoroutine = false;
                }
            }
        }

    }
}

