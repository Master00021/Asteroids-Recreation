using System;

namespace Asteroids{

    internal sealed class LiveEnemy : Enemy, IGiveLive {

        public static Action OnGiveLive;

        public void GiveLive() {
            OnGiveLive?.Invoke();
        }

    }
}