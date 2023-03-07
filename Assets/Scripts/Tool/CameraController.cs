using System;
using Tool.Data;
using UnityEngine;

namespace Tool
{
    public class CameraController : MonoBehaviour
    {
        #region Properties

        [SerializeField] 
        private CameraLevelData cameraData;

        #endregion
        
        #region Variables

        private Vector3 _targetPosition;
        private float _targetZoom;

        #endregion

        #region Components

        private Camera _camera;

        #endregion

        #region Events

        private void Start()
        {
            TryGetComponent(out _camera);
            
            _targetZoom = _camera.orthographicSize;
            _targetPosition = transform.position;
        }

        private void Update()
        {
            // Check
            CheckZoom();

            // Set
            SetZoom();
            SetPosition();
        }

        #endregion

        #region Set

        private void SetTargetPosition(Vector3 position)
        {
            _targetPosition = position;
        }

        private void SetZoom()
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _targetZoom, cameraData.zoomSpeed * Time.deltaTime);
        }

        private void SetPosition()
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, cameraData.dragSpeed * Time.deltaTime);
        }

        #endregion

        #region Check

        private void CheckZoom()
        {
            var scroll = Input.GetAxis("Mouse ScrollWheel");
            
            if (scroll.Equals(0)) return;

            var currentTarget = _targetZoom;
            var zoomIn = scroll > 0;
            if (zoomIn)
            {
                _targetZoom = Mathf.Max(cameraData.minZoom, _targetZoom - 1);
            } else
            {
                _targetZoom = Mathf.Min(cameraData.maxZoom, _targetZoom + 1);
            }

            if (zoomIn && !currentTarget.Equals(_targetZoom))
                SetTargetPosition(MousePosition());
        }

        #endregion

        #region Helper

        public Vector3 MousePosition()
        {
            return _camera.ScreenToWorldPoint(Input.mousePosition);
        }

        #endregion
    }
}
