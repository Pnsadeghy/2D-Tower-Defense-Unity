using System;
using System.Collections;
using System.Collections.Generic;
using Helper;
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

        [SerializeField]
        private LayerMask enemyMask;

        #endregion

        #region Variables

        private Transform _enemy;

        private float _lastShot;

        #endregion

        #region Events

        private void Update()
        {
            CheckEnemy();
            CheckEnemies();
            
            LookAt();
            CheckShoot();
        }

        #endregion

        #region Set

        private void LookAt()
        {
            if (!HasEnemy()) return;
            container.rotation = ValueHelper.LookAt2D(container.position, _enemy.position);
        }

        private void Shoot()
        {
            _lastShot = Time.time;
            // #TODO animation
            // #TODO create bullet
        }

        #endregion

        #region Check

        private bool HasEnemy() => _enemy != null;
        
        private void CheckShoot()
        {
            if (!HasEnemy()) return;
            if (Time.time < _lastShot + towerData.timeout) return;
            Shoot();
        }
        
        private void CheckEnemy()
        {
            if (HasEnemy() && Vector3.Distance(transform.position, _enemy.position) > towerData.range)
            {
                _enemy = null;
            }
        }
        
        private void CheckEnemies()
        {
            if (HasEnemy()) return;

            var currentPosition = transform.position;
            var colliders = Physics2D.OverlapCircleAll(currentPosition, towerData.range, enemyMask);

            Transform nearestEnemy = null;
            var nearestDistance = Mathf.Infinity;
            foreach (var enemy in colliders)
            {
                var distance = Vector3.Distance(currentPosition, enemy.transform.position);
                if (!(distance < nearestDistance)) continue;
                nearestDistance = distance;
                nearestEnemy = enemy.transform;
            }

            _enemy = nearestEnemy;
        }

        #endregion

        #region Gizmos

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, towerData.range);
        }

        #endregion
    }
}