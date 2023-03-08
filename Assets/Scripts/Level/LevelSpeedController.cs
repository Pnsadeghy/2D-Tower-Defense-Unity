using System;
using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class LevelSpeedController: MonoBehaviour
    {
        #region Properties

        [SerializeField]
        private Text speedText;

        [SerializeField]
        private int maxTimeSpeed;

        #endregion
        
        #region Variables

        private float _currentSpeed;

        #endregion

        #region Events

        private void Awake()
        {
            _currentSpeed = Time.timeScale;
            SetSpeedText();
        }

        #endregion

        #region Set

        private void SetSpeedText()
        {
            speedText.text = "X" + (int)_currentSpeed;
        }

        public void UpdateSpeed()
        {
            _currentSpeed++;
            if (_currentSpeed > maxTimeSpeed)
                _currentSpeed = 1;
            Time.timeScale = _currentSpeed;
            SetSpeedText();
        }

        #endregion
    }
}