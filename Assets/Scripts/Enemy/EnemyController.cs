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
    enum EnemyState
    {
        Active,
        Charge,
        Attack
    }
    
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

        private EnemyState _state;
        
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

            _state = EnemyState.Active;
        }

        private void Update()
        {
            if (_state.Equals(EnemyState.Active))
                CheckBase();

            SetPosition();
        }

        #endregion

        #region Set

        private void SetPosition()
        {
            // If it is in charge state leave
            if (_state.Equals(EnemyState.Charge)) return;
            
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

            var speed = enemyData.speed;

            if (_state.Equals(EnemyState.Attack))
                speed *= 2;
            
            // move to target
            transform.position = Vector3.MoveTowards(currentPos, _targetPoint.position, speed * Time.deltaTime);
        }

        public void SetTargetPoint(Transform targetPoint)
        {
            _targetPoint = targetPoint;
        }

        public void Hit(float damage, Vector3 from)
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

        public void Die()
        {
            MenuController.Instance.IncPoint(enemyData.point);
            Destroy(gameObject);
        }

        public void ChargeEnded()
        {
            _state = EnemyState.Attack;
        }

        #endregion

        #region Get

        public float DamageValue() => enemyData.damage;

        #endregion

        #region Check

        private void CheckBase()
        {
            if (!Physics2D.Raycast(transform.position, sprite.rotation * Vector3.up, enemyData.baseRange, enemyData.baseLayer)) return;
            _state = EnemyState.Charge;
            _animator.SetBool("Attack", true);
        }

        #endregion

        private void OnDrawGizmos()
        {
            var direction = sprite.rotation * Vector3.up;
            Gizmos.DrawLine(transform.position, transform.position + (direction * enemyData.baseRange));
        }
    }
}