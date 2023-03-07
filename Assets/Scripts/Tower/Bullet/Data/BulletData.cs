using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Bullet.Data
{
    [CreateAssetMenu(fileName = "newBulletData", menuName = "Data/Tower Data/Bullet Data")]
    public class BulletData : ScriptableObject
    {
        public float damage;
        public float speed;
        
        public float detectRange;
        public float damageRange;

        public LayerMask enemyMask;
    }
}