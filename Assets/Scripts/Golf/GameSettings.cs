using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    [CreateAssetMenu]
    public class GameSettings : ScriptableObject
    {
        public float minBallSpeed;
        public float maxBallSpeed;
        public float enemyPrecision;
        public byte winScore;
    }
}
