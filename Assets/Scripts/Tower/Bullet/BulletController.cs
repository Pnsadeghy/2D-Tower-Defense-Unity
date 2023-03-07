using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using Helper;
using Tower.Bullet.Data;
using UnityEngine;

namespace Tower.Bullet {
    public class BulletController : MonoBehaviour
    {
        #region Properties

        [SerializeField]
        private BulletData bulletData;

        #endregion

        #region Variables

        private bool _isHit;
        private Transform _target;

        #endregion

        #region Events

        private void Update()
        {
            CheckHit();
            
            SetPosition();
        }

        #endregion

        #region Set

        private void SetPosition()
        {
            if (_target)
            {
                var lookAt = ValueHelper.LookAt2D(transform.position, _target.position);
                transform.position = Vector3.MoveTowards(transform.position, _target.position, bulletData.speed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookAt, 0.1f);
            }
            else
            {
                transform.position += transform.rotation * Vector3.up * bulletData.speed * Time.deltaTime;
            }
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        #endregion

        #region Check

        private void CheckHit()
        {
            if (_isHit) return;
            if (!Physics2D.OverlapCircle(transform.position, bulletData.detectRange, bulletData.enemyMask)) return;
            _isHit = true;
            var enemies = Physics2D.OverlapCircleAll(transform.position, bulletData.damageRange, bulletData.enemyMask);
            foreach (var enemy in enemies)
            {
                enemy.GetComponentInParent<EnemyController>().Damage(bulletData.damage);
            }
            Die();
        }

        #endregion

        #region Gizmos

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, bulletData.detectRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, bulletData.damageRange);
        }

        #endregion
    }
}