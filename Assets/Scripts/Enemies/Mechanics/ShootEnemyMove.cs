using System.Collections;
using UnityEngine;

namespace Game.Asteroids {

    internal sealed class ShootEnemyMove : EnemyMove {

        [SerializeField] private Rigidbody2D _rigidbody;

        protected override void Start() {
            base.Start();

            StartCoroutine(CO_VerticalMove());  
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