using System.Collections;
using System.Collections.Generic;
using Tower;
using UnityEngine;

namespace Level
{
    public class TowerButtonController : MonoBehaviour
    {
        #region Variables

        private TowerController _tower;

        #endregion

        #region Set

        public void SetTower(TowerController tower)
        {
            _tower = tower;
        }

        #endregion
    }    
}
