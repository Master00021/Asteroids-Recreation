using System.Collections;
using UnityEngine;

namespace Game.Asteroids {

    internal sealed class AsteroidMove : MonoBehaviour {
        
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;
        
        private void Start() {
            StartCoroutine(CO_Movement());
        }

        private IEnumerator CO_Movement() {
            var randomRotation = Random.Range(1.0f, 360.0f);
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, randomRotation);
            var speed = Random.Range(_minSpeed, _maxSpeed);

            while (true) {
                transform.position += transform.up * speed * Time.deltaTime;

                yield return null;
            }
        }

    }
}