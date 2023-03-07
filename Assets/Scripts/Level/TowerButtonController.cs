using Tower;
using UnityEngine;
using UnityEngine.UIElements;

namespace Level
{
    public class TowerButtonController : MonoBehaviour
    {
        #region Variables

        private float _point;
        private TowerController _tower;

        #endregion

        #region Components

        private Button _button;
        private Image _sprite;

        #endregion

        #region Events

        private void Awake()
        {
            TryGetComponent(out _button);
            _sprite = transform.GetChild(0).GetComponent<Image>();
        }

        private void Update()
        {
            var currentPoint = MenuController.Instance.CurrentPoint();
            _button.SetEnabled(currentPoint >= _point);
            _sprite.tintColor = currentPoint >= _point ? Color.white : Color.gray;
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
