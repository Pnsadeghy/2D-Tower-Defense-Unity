using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool.Data
{
    [CreateAssetMenu(fileName = "newCameraData", menuName = "Data/Level Data/Camera Data")]
    public class CameraLevelData : ScriptableObject
    {
        [Header("Zoom")]
        public float zoomSpeed;
        public float zoomMin, zoomMax;
        
        [Header("Drag")]
        public float dragSpeed;
        public Vector2 dragMin;
        public Vector2 dragMax;
    }
}