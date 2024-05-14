using System.Collections;
using UnityEngine;

namespace Game.Asteroids {

    internal sealed class Music : MonoBehaviour {

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _beatOne;
        [SerializeField] private AudioClip _beatTwo;

        private float _incrementValue;
        private bool _playMusic;
        
        private const float MINIMUM_TIME_BETWEEN_BEATS = 0.175f;
        private const float INITIAL_TIME_BETWEEN_BEATS = 0.6f;
        private const float MODIFIER_VALUE = 0.004f;

        private Coroutine _music;

        private void OnEnable() {
            PlayerSpawner.OnPlayerSpawned += PlayMusic;
            PlayerData.OnLivesDecreased += PauseMusic;
            PlayerData.OnGameOver += StopMusic;

            AsteroidWaves.OnNewWave += StartMusic;
            AsteroidWaves.OnWaveEnd += StopMusic;
        }

        private void OnDisable() {
            PlayerSpawner.OnPlayerSpawned -= PlayMusic;
            PlayerData.OnLivesDecreased -= PauseMusic;
            PlayerData.OnGameOver -= StopMusic;
            
            AsteroidWaves.OnNewWave -= StartMusic;
            AsteroidWaves.OnWaveEnd -= StopMusic;
        }

        private void StartMusic() {
            _music = StartCoroutine(CO_GameMusic());
        }

        private void StopMusic() {
            StopCoroutine(_music);
        }

        private void PlayMusic(GameObject _) {
            _playMusic = true;
        }

        private void PauseMusic() {
            _playMusic = false;
        }

        private IEnumerator CO_GameMusic() {
            bool playBeatOne = true;
            _incrementValue = 0.0f;

            while (true) {
                if (_playMusic) {
                    _incrementValue++;
                    float timeBetweenBeats = (-MODIFIER_VALUE * _incrementValue) + INITIAL_TIME_BETWEEN_BEATS;

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
                else {
                    yield return null;
                }
            }
        }
    
    }
}

