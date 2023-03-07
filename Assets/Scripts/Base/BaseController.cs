using System;
using Base.Data;
using Enemy;
using Level;
using UnityEngine;

namespace Base
{
    public class BaseController: MonoBehaviour
    {
        #region Properties

        [SerializeField]
        private BaseData baseData;
        
        [SerializeField]
        private Transform healthBar;

        #endregion

        #region Variables

        private float _currentHealth;

        #endregion
        
        #region Components

        private Animator _animator;

        #endregion

        #region Events

        private void Awake()
        {
            TryGetComponent(out _animator);
   
            _currentHealth = baseData.health;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            EnemyController enemy;
            if (!col.TryGetComponent(out enemy)) return;
            _currentHealth -= enemy.DamageValue();
            enemy.Hit(1000f, transform.position);

            if (_currentHealth > 0)
            {
                _animator.SetTrigger("Hit");
                healthBar.gameObject.SetActive(true);
                healthBar.transform.localScale = new Vector3(_currentHealth / baseData.health, 1f, 1f);
            }
        }

        #endregion
    }
}