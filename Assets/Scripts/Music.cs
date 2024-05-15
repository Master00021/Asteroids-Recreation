using System.Collections;
using UnityEngine;

namespace Asteroids {

    [RequireComponent(typeof(AudioSource))]
    internal sealed class Music : MonoBehaviour {

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _beatOne;
        [SerializeField] private AudioClip _beatTwo;

        private bool _playMusic;
        
        private const float MINIMUM_TIME_BETWEEN_BEATS = 0.175f;
        private const float INITIAL_TIME_BETWEEN_BEATS = 0.6f;
        private const float MODIFIER_VALUE = 0.004f;

        private Coroutine _coroutine;

        private void OnEnable() {
            AsteroidWaves.OnWaveStart += StartMusic;
            AsteroidWaves.OnWaveEnd += StopMusic;
            PlayerData.OnGameOver += StopMusic;

            PlayerSpawner.OnPlayerSpawned += PlayMusic;
            PlayerData.OnLivesDecreased += PauseMusic;
        }

        private void OnDisable() {
            AsteroidWaves.OnWaveStart -= StartMusic;
            AsteroidWaves.OnWaveEnd -= StopMusic;
            PlayerData.OnGameOver -= StopMusic;

            PlayerSpawner.OnPlayerSpawned -= PlayMusic;
            PlayerData.OnLivesDecreased -= PauseMusic;
        }

        private void StartMusic() {
            _coroutine = StartCoroutine(CO_GameMusic());
        }

        private void StopMusic() {
            StopCoroutine(_coroutine);
        }

        private void PlayMusic() {
            _playMusic = true;
        }

        private void PauseMusic() {
            _playMusic = false;
        }

        private IEnumerator CO_GameMusic() {
            bool playBeatOne = true;
            float incrementValue = 0.0f;

            while (true) {
                if (_playMusic) {
                    incrementValue++;
                    float timeBetweenBeats = (-MODIFIER_VALUE * incrementValue) + INITIAL_TIME_BETWEEN_BEATS;

                    if (timeBetweenBeats < MINIMUM_TIME_BETWEEN_BEATS) {
                        timeBetweenBeats = MINIMUM_TIME_BETWEEN_BEATS;
                    }

                    if (playBeatOne) {
                        _audioSource.PlayOneShot(_beatOne);
                        playBeatOne = false;
                    }
                    else {
                        _audioSource.PlayOneShot(_beatTwo);
                        playBeatOne = true;
                    }

                    yield return new WaitForSeconds(timeBetweenBeats);
                }
                
                yield return null;
            }
        }
    
    }
}