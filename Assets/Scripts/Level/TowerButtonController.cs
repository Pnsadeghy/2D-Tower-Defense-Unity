using System;
using Tower;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class TowerButtonController : MonoBehaviour
    {
        #region Properties
        
        [SerializeField]
        private Image sprite;

        #endregion
        
        #region Variables

        private float _point;
        private TowerController _tower;

        #endregion

        #region Components
        
        private Button _button;

        #endregion

        #region Events

        private void Awake()
        {
            TryGetComponent(out _button);
        }

        private void Update()
        {
            var currentPoint = MenuController.Instance.CurrentPoint();
            var isFocused = _tower.Equals(MenuController.Instance.SelectedTower());
            var isEnabled = currentPoint >= _point;
            _button.interactable = isEnabled;
            sprite.color = isEnabled ? (isFocused ? Color.yellow : Color.white) : Color.gray;
        }

        public void OnClick()
        {
            MenuController.Instance.SetSelectedTower(_tower);
        }

        #endregion

        #region Set

        public void SetTower(TowerController tower)
        {
            _tower = tower;
            _point = tower.PointRequired();
        }

        #endregion
    }    
}
