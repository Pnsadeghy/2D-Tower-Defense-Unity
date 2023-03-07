using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class MenuController : MonoBehaviour
    {
        #region Properties

        [SerializeField]
        private RectTransform MenuPanel;
        
        [SerializeField]
        private RectTransform MenuButtonsPanel;
        
        [SerializeField]
        private Text MenuPointText;

        [SerializeField]
        private GameObject towerButton;

        #endregion

        #region Variables
        
        private Transform _towersContainer;

        private float _currentPoint;

        #endregion

        #region Events

        private void Awake()
        {
            _towersContainer = transform.GetChild(1);

            var levelController = GetComponent<LevelController>();
            
            var towers = levelController.towers();
            
            MenuButtonsPanel.sizeDelta = new Vector2(MenuButtonsPanel.sizeDelta.x, towers.Count * 90);
            MenuButtonsPanel.anchoredPosition = new Vector2(0, MenuButtonsPanel.sizeDelta.y / 2);
            MenuPanel.sizeDelta = new Vector2(MenuPanel.sizeDelta.x, MenuButtonsPanel.sizeDelta.y + MenuPanel.sizeDelta.y);
            
            var pos = 0;
            towers.Reverse();
            foreach (var tower in towers)
            {
                var button = Instantiate(towerButton, MenuButtonsPanel);
                var buttonRect = button.GetComponent<RectTransform>();
                
                buttonRect.anchoredPosition = new Vector2(0, pos + 50);
                pos += 90;
                
                button.transform.GetChild(0).GetComponent<Image>().sprite = tower.GetSprite();
                button.GetComponent<TowerButtonController>().SetTower(tower);
            }

            _currentPoint = levelController.firstPoint();
            MenuPointText.text = "$ " + _currentPoint;
        }

        #endregion
    }

}