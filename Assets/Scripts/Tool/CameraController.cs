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
        
        private Vector3 _dragOriginPosition;

        #endregion

        #region Components

        private Camera _camera;

        #endregion

        #region Events

        private void Awake()
        {
            TryGetComponent(out _camera);
            
            // Initial target values ( values camera will reach smoothly )
            _targetZoom = _camera.orthographicSize;
            _targetPosition = transform.position;
        }

        private void Update()
        {
            // Check
            CheckZoom();
            CheckDrag();

            // Set
            SetZoom();
            SetPosition();
        }

        #endregion

        #region Set
        
        private void SetTargetPosition(Vector3 position)
        {
            // Check camera limitation movements
            var cameraHeight = _camera.orthographicSize / 2;
            var cameraWidth = (_camera.orthographicSize * _camera.aspect) / 2;

            position.x = Mathf.Max(cameraData.dragMin.x + cameraWidth,
                Mathf.Min(cameraData.dragMax.x - cameraWidth, position.x));
            
            position.y = Mathf.Max(cameraData.dragMin.y + cameraHeight,
                Mathf.Min(cameraData.dragMax.y - cameraHeight, position.y));
            
            // Set new target position
            _targetPosition = position;
        }

        private void SetZoom()
        {
            // Smoothly reach target zoom
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _targetZoom, cameraData.zoomSpeed * Time.deltaTime);
        }

        private void SetPosition()
        {
            // Smoothly reach target position
            transform.position = Vector3.Lerp(transform.position, _targetPosition, cameraData.dragSpeed * Time.deltaTime);
        }

        #endregion

        #region Check

        private void CheckZoom()
        {
            // Get scroll value
            var scroll = Input.GetAxis("Mouse ScrollWheel");
            
            // exit function when scroll is 0
            if (scroll.Equals(0)) return;

            // save current zoom for after checks
            var currentTarget = _targetZoom;
            
            var zoomIn = scroll > 0;
            
            // Calculate new zoom value base on scroll and zoom limitations
            if (zoomIn)
            {
                _targetZoom = Mathf.Max(cameraData.zoomMin, _targetZoom - 1);
            } else
            {
                _targetZoom = Mathf.Min(cameraData.zoomMax, _targetZoom + 1);
            }

            // In ZoomIn, the camera will move to the mouse position
            if (zoomIn && !currentTarget.Equals(_targetZoom))
                SetTargetPosition(MousePosition());
        }

        private void CheckDrag()
        {
            // Save mouse position when drag start
            if (Input.GetMouseButtonDown(0))
            {
                _dragOriginPosition = MousePosition();
            }

            // Calculate distance between drag origin and new mouse pos if it is still held down
            if (Input.GetMouseButton(0))
            {
                SetTargetPosition(_targetPosition + (_dragOriginPosition - MousePosition()));
            }
        }

        #endregion

        #region Helper

        public Vector3 MousePosition()
        {
            // Get mouse current position in Game world
            return _camera.ScreenToWorldPoint(Input.mousePosition);
        }

        #endregion
    }
}
