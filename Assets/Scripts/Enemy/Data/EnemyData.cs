using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Data
{
    [CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Basic Data")]
    public class EnemyData : ScriptableObject
    {
        public float health;
        public float point;

        public float speed;
        public float damage;

        public float baseRange;
        public LayerMask baseLayer;
    }
}
