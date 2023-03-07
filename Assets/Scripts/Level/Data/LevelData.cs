using System.Collections;
using System.Collections.Generic;
using Enemy;
using Tower;
using UnityEngine;

namespace Level.Data
{
    [CreateAssetMenu(fileName = "newLevelData", menuName = "Data/Level Data/Basic Data")]
    public class LevelData : ScriptableObject
    {
        public float firstPoint;
        
        public List<TowerController> towers;
        public List<EnemyController> enemies;
        public List<WaveData> waves;
    }

}