using System;
using System.Collections;
using System.Collections.Generic;
using Enemy.Data;
using Helper;
using Level;
using Path;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        #region Properties

        [SerializeField]
        private EnemyData enemyData;
        
        [SerializeField]
        private Transform sprite;
        
        [SerializeField]
        private Transform healthBar;

        #endregion

        #region Variables
        
        private Quaternion _lookAt;
        private float _currentHealth;

        private Transform _targetPoint;

        #endregion

        #region Components

        private Animator _animator;

        #endregion

        #region Events

        private void Awake()
        {
            _currentHealth = enemyData.health;

            TryGetComponent(out _animator);
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
            if (_targetPoint == null) return;
            
            // get current position
            var currentPos = transform.position;
            
            // if item reached the target position, get next point
            if (currentPos.Equals(_targetPoint.position))
            {
                _targetPoint = _targetPoint.GetComponent<PointController>().GetNextPoint();
                if (_targetPoint == null) return;
                _lookAt = ValueHelper.LookAt2D(currentPos, _targetPoint.position);
            }

            // smoothly rotate sprite
            sprite.rotation = Quaternion.Slerp(sprite.rotation, _lookAt, 0.1f);
            
            // move to target
            transform.position = Vector3.MoveTowards(currentPos, _targetPoint.position, enemyData.speed * Time.deltaTime);
        }

        public void SetTargetPoint(Transform targetPoint)
        {
            _targetPoint = targetPoint;
        }

        public void Damage(float damage)
        {
            _currentHealth -= damage;
            if (_currentHealth < 0)
            {
                Die();
            }
            else
            {
                _animator.SetTrigger("Hit");
                healthBar.gameObject.SetActive(true);
                healthBar.transform.localScale = new Vector3(_currentHealth / enemyData.health, 1f, 1f);
            }
        }

        private void Die()
        {
            MenuController.Instance.IncPoint(enemyData.point);
            Destroy(gameObject);
        }

        #endregion
    }
}