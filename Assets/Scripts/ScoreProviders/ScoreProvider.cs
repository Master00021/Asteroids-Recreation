using UnityEngine;
using System;

namespace Asteroids {

    internal class ScoreProvider : MonoBehaviour {

        public static Action<int> OnScoreSent;

        [SerializeField] protected int _scoreToGive;

    }
}

