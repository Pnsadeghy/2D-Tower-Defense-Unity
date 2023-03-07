using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Level.Element;
using Tower;
using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class MenuController : MonoBehaviour
    {
        #region Init

        public static MenuController Instance;

        public MenuController()
        {
            Instance = this;
        }

        #endregion
        
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

        private TowerController _selectedTower;

        #endregion

        #region Events

        private void Awake()
        {
            _towersContainer = transform.GetChild(1);

            var levelController = GetComponent<LevelController>();
            
            SetTowerButtons(levelController.TowersList());
            
            _currentPoint = levelController.FirstPoint();
            SetPointText();
        }

        #endregion

        #region Set

        private void SetPointText()
        {
            MenuPointText.text = "$ " + _currentPoint;
        }

        private void SetTowerButtons(List<TowerController> towers)
        {
            // Set Panel sizes for put buttons
            MenuButtonsPanel.sizeDelta = new Vector2(MenuButtonsPanel.sizeDelta.x, towers.Count * 90);
            MenuButtonsPanel.anchoredPosition = new Vector2(0, MenuButtonsPanel.sizeDelta.y / 2);
            MenuPanel.sizeDelta = new Vector2(MenuPanel.sizeDelta.x, MenuButtonsPanel.sizeDelta.y + MenuPanel.sizeDelta.y);
            
            // select first tower auto
            _selectedTower = towers[0];
            
            // reverse list for better placement
            towers.Reverse();
            
            // create buttons
            var pos = 0;
            foreach (var tower in towers)
            {
                CreateTowerButton(tower, ref pos);
            }
        }

        public void SetSelectedTower(TowerController tower)
        {
            _selectedTower = tower;
        }

        #endregion

        #region Get

        public float CurrentPoint() => _currentPoint;
        public TowerController SelectedTower() => _selectedTower;

        #endregion

        #region Helper
        
        private void CreateTowerButton(TowerController tower, ref int pos)
        {
            var button = Instantiate(towerButton, MenuButtonsPanel);
            var buttonRect = button.transform.GetComponent<RectTransform>();

            buttonRect.anchoredPosition = new Vector2(0, pos + 50);
            pos += 90;

            // set preview sprite
            button.transform.GetChild(0).GetComponent<Image>().sprite = tower.PreviewSprite();
            
            // set button tower
            button.transform.GetComponent<TowerButtonController>().SetTower(tower);
        }

        #endregion
    }

}