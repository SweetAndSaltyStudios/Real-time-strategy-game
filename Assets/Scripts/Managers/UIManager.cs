using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sweet_And_Salty_Studios
{
    public class UIManager : Singelton<UIManager>
    {
        public Transform SelectionPanel;

        public Transform ButtonParent;
        public List<UIButton> uiButtons = new List<UIButton>();
        public List<GameObject> selectableIcons = new List<GameObject>();

        private RectTransform selectionBox;
        private Vector3 startPosition;
        private Vector3 endPosition;
        private Vector3 centreposition;
        private Camera mainCamera;

        private Transform controlPanel;

        public bool IsSelectionBoxActive
        {
            get;
            private set;
        }

        public bool InsideSelectionBox(Vector3 position)
        {
            var screenPoint = mainCamera.WorldToScreenPoint(position);
            if (RectTransformUtility.RectangleContainsScreenPoint(selectionBox, screenPoint))
            {
                return true;
            }

            return false;
        }

        private Canvas hudCanvas;

        private void Awake()
        {
            mainCamera = Camera.main;

            hudCanvas = transform.Find("HUD_Canvas").GetComponent<Canvas>();
            selectionBox = hudCanvas.transform.Find("SelectionBox").GetComponent<RectTransform>();
            controlPanel = hudCanvas.transform.Find("ControlPanel").transform;
        }

        private void Start()
        {
            selectionBox.gameObject.SetActive(false);
            controlPanel.gameObject.SetActive(true);
        }

        public void ShowCursor()
        {
            if(IsSelectionBoxActive == false)
            {
                startPosition = InputManager.Instance.MousePosition;
                IsSelectionBoxActive = true;
            }     
        }

        public void UpdateCursor()
        {
            endPosition = InputManager.Instance.MousePosition;

            centreposition = (startPosition + endPosition) / 2f;
            selectionBox.position = centreposition;

            var sizeX = Mathf.Abs(startPosition.x - endPosition.x);
            var sizeY = Mathf.Abs(startPosition.y - endPosition.y);

            selectionBox.sizeDelta = new Vector2(sizeX, sizeY);

            if (!selectionBox.gameObject.activeSelf)
            {
                selectionBox.gameObject.SetActive(true);
            }
        }

        public void UpdateSelectedIcons(Unit[] selectedUnits)
        {
            for (int i = 0; i < selectedUnits.Length; i++)
            {
                var newSelectableIcon = Instantiate(ResourceManager.Instance.SelectedIconPrefab, SelectionPanel);
                selectableIcons.Add(newSelectableIcon);
            }
        }

        public void HideCursor()
        {
            if (IsSelectionBoxActive == true)
            {
                selectionBox.gameObject.SetActive(false);
                IsSelectionBoxActive = false;
            }
        }

        public void ShowBuildOptions(BuildingUIInfo buildingUIInfo)
        {
            for (int i = 0; i < buildingUIInfo.ButtonActions.Count; i++)
            {
                var newUIButton = Instantiate(ResourceManager.Instance.UIButtonPrefab, ButtonParent).GetComponent<UIButton>();
                newUIButton.Initialize(buildingUIInfo.ButtonActions[i]);
                uiButtons.Add(newUIButton);
            }
        }

        public void HideBuildOptions()
        {
            for (int i = 0; i < uiButtons.Count; i++)
            {
                Destroy(uiButtons[i].gameObject);
            }

            uiButtons.Clear();
        }
    }
}
