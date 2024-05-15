using System;

namespace Game.Asteroids{

    internal sealed class LiveEnemy : Enemy, ILiveEnemy {

        public static Action OnLiveSent;

        public void SendLive() {
            OnLiveSent?.Invoke();
        }

    }
}