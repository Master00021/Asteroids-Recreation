using UnityEngine;
using System;

namespace Asteroids {

    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class Player : MonoBehaviour {
    
        public static Action OnPlayerDeath;

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.GetComponent<Asteroid>()) {
                Death();
            }

            if (other.GetComponent<Enemy>()) {
                Death();
            }
        }
        
        internal void Death() {
            OnPlayerDeath?.Invoke();
            gameObject.SetActive(false);
        }

    }
}