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

        public GameSettings(GameSettings other) {
            minBallSpeed = other.minBallSpeed;
            maxBallSpeed = other.maxBallSpeed;
            enemyPrecision = other.enemyPrecision;
            winScore = other.winScore;
        }
    }
}
