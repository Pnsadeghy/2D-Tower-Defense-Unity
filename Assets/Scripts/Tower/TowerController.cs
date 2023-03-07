using System;
using System.Collections;
using System.Collections.Generic;
using Tower.Data;
using UnityEngine;

namespace Tower
{
    public class TowerController : MonoBehaviour
    {
        #region Properties

        [SerializeField]
        private TowerData towerData;
        
        [SerializeField]
        private Transform container;

        #endregion

        #region Gizmos

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, towerData.range);
        }

        #endregion
    }
}