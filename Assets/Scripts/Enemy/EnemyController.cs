using System;
using System.Collections;
using System.Collections.Generic;
using Enemy.Data;
using Helper;
using Path;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        #region Properties

        [SerializeField]
        private EnemyData enemyData;
        
        [SerializeField]
        private Transform sprite;

        public Transform targetPoint;

        #endregion

        #region Variables
        
        private Quaternion _lookAt;
        private float _currentHealth;

        #endregion

        #region Events

        private void Start()
        {
            _currentHealth = enemyData.health;
        }

        private void Update()
        {
            SetPosition();
        }

        #endregion

        #region Set

        private void SetPosition()
        {
            // leave if there is no target point
            if (targetPoint == null) return;
            
            // get current position
            var currentPos = transform.position;
            
            // if item reached the target position, get next point
            if (currentPos.Equals(targetPoint.position))
            {
                targetPoint = targetPoint.GetComponent<PointController>().GetNextPoint();
                if (targetPoint == null) return;
                _lookAt = ValueHelper.LookAt2D(currentPos, targetPoint.position);
            }

            // smoothly rotate sprite
            sprite.rotation = Quaternion.Slerp(sprite.rotation, _lookAt, 0.1f);
            
            // move to target
            transform.position = Vector3.MoveTowards(currentPos, targetPoint.position, enemyData.speed * Time.deltaTime);
        }

        #endregion
    }

}