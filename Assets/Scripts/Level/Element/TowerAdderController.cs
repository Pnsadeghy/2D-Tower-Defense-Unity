using System;
using Tool;
using Tower;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Level.Element
{
    public class TowerAdderController : MonoBehaviour
    {
        #region Properties

        [SerializeField]
        private LayerMask blockLayer;

        #endregion
        
        #region Variables

        private TowerController _tower;
        private float _towerLand;
        private float _towerPoint;
        
        private SpriteRenderer _landChild;
        private SpriteRenderer _rangeChild;
        private Color _landColor;
        private Color _rangeColor;

        #endregion
        
        #region Events

        private void Awake()
        {
            var landChild = transform.GetChild(0);
            var rangeChild = transform.GetChild(1);

            _landChild = landChild.GetComponent<SpriteRenderer>();
            _rangeChild = rangeChild.GetComponent<SpriteRenderer>();

            _landColor = _landChild.color;
            _rangeColor = _rangeChild.color;
        }

        private void Update()
        {
            CheckTower();

            if (HasTower())
            {
                SetPosition();
                CheckPlacement();
            }
            else
            {
                _landChild.gameObject.SetActive(false);
                _rangeChild.gameObject.SetActive(false);
            }
        }

        #endregion

        #region Set

        private void SetPosition()
        {
            transform.position = (Vector2)CameraController.Instance.MousePosition();
        }

        private void SetSize()
        {
            var data = _tower.Data();
            _towerLand = data.landSize;
            _towerPoint = data.point;
            _landChild.transform.localScale = new Vector3(data.landSize, data.landSize, 1);
            _rangeChild.transform.localScale = new Vector3(data.range, data.range, 1);
        }

        private void CreateTower()
        {
            Instantiate(_tower.gameObject, transform.position, Quaternion.identity,
                MenuController.Instance.TowerContainer());
        }

        #endregion

        #region Check

        private void CheckTower()
        {
            var selectedTower = MenuController.Instance.SelectedTower();
            if (selectedTower == _tower) return;
            _tower = selectedTower;
            if (_tower != null)
                SetSize();
        }

        private void CheckPlacement()
        {
            _landChild.gameObject.SetActive(true);
            _rangeChild.gameObject.SetActive(true);
            
            var colliders = Physics2D.OverlapCircleAll(transform.position, _towerLand / 2, blockLayer);

            var isValidate = colliders.Length.Equals(0);
            
            _landChild.color = isValidate ? _landColor : new Color(1f, 0f, 0f, 0.5f);
            _rangeChild.color = isValidate ? _rangeColor : new Color(1f, 0f, 0f, 0.2f);
            
            CheckClick(isValidate);
        }

        private void CheckClick(bool isValidate)
        {
            if (!Input.GetMouseButtonDown(0)) return;
            if (!isValidate) return;
            
            MenuController.Instance.DecPoint(_towerPoint);
            CreateTower();
        }

        private bool HasTower() => _tower != null && !EventSystem.current.IsPointerOverGameObject();

        #endregion
    }
}