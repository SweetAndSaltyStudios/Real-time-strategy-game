using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    [Serializable]
    public class PlayerEngine : MonoBehaviour
    {
        #region VARIABLES

        public Building SelectedBuilding;

        private int id;
        private Transform unitContainer;

        private List<Unit> units = new List<Unit>();
        [SerializeField]
        private List<Unit> selectedUnits = new List<Unit>();
        [SerializeField]
        private List<Unit> preSelectedUnits = new List<Unit>();

        private Ray mouseRay;
        private RaycastHit hitInfo;
        private LayerMask hitLayer;
        private readonly float mouseRayLenght = 100f;

        private bool selectableObjectTargeted;

        #endregion VARIABLES

        #region PROPERTIES

        public bool IsLegalCommand
        {
            get
            {
                return selectableObjectTargeted == false && UnitsSelected;
            }
        }
        public bool UnitsSelected
        {
            get
            {
                return selectedUnits.Count > 0;
            }
        }
      
        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        private void Start()
        {
            SpawnBuilding(ResourceManager.Instance.CommandCenterPrefab);
        }

        private void Update()
        {
            if (InputManager.Instance.GetKeyDown(KeyCode.Escape))
            {
                if (UnitsSelected)
                {
                    RemoveAllSelectedUnits();
                }
            }

            if (InputManager.Instance.MouseButtonDown(0))
            {
                if (SelectedBuilding != null && InputManager.Instance.IsPointerOverUI == false)
                {
                    UnselectBuilding();
                }

                if (UnitsSelected)
                {
                    RemoveAllSelectedUnits();
                }

                CastMouseRay();

                UIManager.Instance.ShowCursor();
            }

            if (InputManager.Instance.MouseButton(0))
            {
                UIManager.Instance.UpdateCursor();

                if (UIManager.Instance.IsSelectionBoxActive)
                {
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (UIManager.Instance.InsideSelectionBox(units[i].transform.position))
                        {
                            SelectUnit(units[i], preSelectedUnits);
                        }
                        else
                        {
                            if(selectedUnits.Contains(units[i]) == false)
                                RemoveUnit(units[i], preSelectedUnits);
                        }
                    }
                }
            }

            if (InputManager.Instance.MouseButtonUp(0))
            {
                if(preSelectedUnits.Count > 0)
                {
                    for (int i = 0; i < preSelectedUnits.Count; i++)
                    {
                        SelectUnit(preSelectedUnits[i], selectedUnits);
                    }
                }

                preSelectedUnits.Clear();

                UIManager.Instance.HideCursor();
            }

            if (InputManager.Instance.MouseButtonDown(1))
            {
                CastMouseRay();

                if (IsLegalCommand)
                {
                    for (int i = 0; i < selectedUnits.Count; i++)
                    {
                        selectedUnits[i].Move(hitInfo.point);
                    }
                }

                if(SelectedBuilding != null)
                {
                    SelectedBuilding.SetNewRallyPoint(hitInfo.point);
                }
            }

            if (InputManager.Instance.MouseButtonDown(2))
            {
                if (UnitsSelected)
                {
                    for (int i = 0; i < selectedUnits.Count; i++)
                    {
                        selectedUnits[i].Stop();
                    }
                }
            }
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        public void Initialize(int id)
        {
            this.id = id;

            gameObject.name = "Player " + id;

            hitLayer = LayerMask.GetMask("Default", "Unit", "Building");

            unitContainer = new GameObject("Units").transform;
            unitContainer.transform.SetParent(transform);
        }

        private void SelectUnit(Unit unit, List<Unit> unitSelection)
        {
            if (unitSelection.Contains(unit) == false)
            {
                unit.OnSelected();
                unitSelection.Add(unit);
            }
        }

        private void RemoveUnit(Unit unit, List<Unit> unitSelection)
        {
            unit.OnDeselected();
            unitSelection.Remove(unit);
        }

        private void RemoveAllSelectedUnits()
        {
            for (int i = 0; i < selectedUnits.Count; i++)
            {
                selectedUnits[i].OnDeselected();
            }

            selectedUnits.Clear();
        }

        private void MoveUnits(Vector3 newPosition)
        {
            for (int i = 0; i < selectedUnits.Count; i++)
            {
                selectedUnits[i].Move(newPosition);
            }
        }

        private void CastMouseRay()
        {
            mouseRay = Camera.main.ScreenPointToRay(InputManager.Instance.MousePosition);
            hitInfo = new RaycastHit();

            if (Physics.Raycast(mouseRay, out hitInfo, mouseRayLenght, hitLayer))
            {
                var hittedObject = hitInfo.collider.gameObject;

                switch (hittedObject.layer)
                {
                    case 9:

                        selectableObjectTargeted = true;

                        SelectUnit(hittedObject.GetComponent<Unit>(), selectedUnits); 

                        break;

                    case 10:

                        SelectBuilding(hittedObject.GetComponent<Building>());

                        break;

                    default:

                        selectableObjectTargeted = false;

                        break;
                }
            }
        }

        private void SelectBuilding(Building building)
        {
            SelectedBuilding = building;
            SelectedBuilding.Selected = true;
            UIManager.Instance.ShowBuildOptions(building.BuildingUIInfo);
        }

        private void UnselectBuilding()
        {
           
                UIManager.Instance.HideBuildOptions();
                SelectedBuilding.Selected = false;
                SelectedBuilding = null;
                    
        }

        public Unit SpawnUnit(GameObject unitPrefab, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion())
        {
            var newUnit = Instantiate(unitPrefab, position, rotation, unitContainer).GetComponent<Unit>();
            newUnit.Initialize(this, 100, 10, 3.5f);
            units.Add(newUnit);

            return newUnit;
        }

        public void SpawnBuilding(GameObject buildingPrefab, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion())
        {
            var newBuilding = Instantiate(buildingPrefab, position, rotation, unitContainer).GetComponent<Building>();
            newBuilding.Initialize(this);
        }

        #endregion CUSTOM_FUNCTIONS 
    }
}