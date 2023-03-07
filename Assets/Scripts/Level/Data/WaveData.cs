using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level.Data
{
    [CreateAssetMenu(fileName = "newWaveData", menuName = "Data/Level Data/Wave Data")]
    public class WaveData : ScriptableObject
    {
        public float timeout;
        public float enemyTimeout;
        public int enemyCount;

        public List<int> enemies;
    }
}
