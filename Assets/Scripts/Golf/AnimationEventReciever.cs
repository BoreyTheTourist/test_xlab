using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    public class AnimationEventReciever : MonoBehaviour
    {
        public event System.Action OnBallScreamed;
        public event System.Action OnBallReturned;

        public void BallScreamed()
        {
            OnBallScreamed.Invoke();
        }

        public void BallReturned()
        {
            OnBallReturned.Invoke();
        }
    }
}
