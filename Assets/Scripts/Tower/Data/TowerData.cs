using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Data
{
    [CreateAssetMenu(fileName = "newTowerData", menuName = "Data/Tower Data/Basic Data")]
    public class TowerData : ScriptableObject
    {
        [Header("Gameplay")]
        public float timeout;
        public GameObject bullet;
        
        [Header("Place")]
        public float landSize;
        public float range;
        
        [Header("Menu")]
        public float point;
        public Sprite preview;
    }
}
