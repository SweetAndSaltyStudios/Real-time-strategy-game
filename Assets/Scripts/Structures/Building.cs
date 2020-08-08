using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sweet_And_Salty_Studios
{
    public class BuildingUIInfo
    {
        public List<UnityAction> ButtonActions { get; } = new List<UnityAction>();

        public void AddButtonAction(UnityAction newAction)
        {
            if (ButtonActions.Contains(newAction))
            {
                return;
            }

            ButtonActions.Add(newAction);
        }
    }

    public abstract class Building : MonoBehaviour
    {
        public bool Selected;

        public BuildingUIInfo BuildingUIInfo;

        private Vector3 rallyPoint;

        private PlayerEngine owner;

        protected float trainDuration;

        protected virtual void Awake()
        {
            BuildingUIInfo = new BuildingUIInfo();
        }

        private void Start()
        {
            rallyPoint = Vector3.back * 2;
        }

        public void Initialize(PlayerEngine owner)
        {
            this.owner = owner;
        }

        public void SetNewRallyPoint(Vector3 point)
        {
            rallyPoint = point;
        }

        private void OnDrawGizmos()
        {
            if (Selected)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position + Vector3.up, rallyPoint);
            }
        }

        public void TrainSCV()
        {
            TrainUnit(ResourceManager.Instance.SCVPrefab);
        }

        public void TrainMarine()
        {
            TrainUnit(ResourceManager.Instance.MarinePrefab);
        }

        private void TrainUnit(GameObject unitPrefab)
        {
            StartCoroutine(ITrainUnit(unitPrefab, trainDuration));
        }

        private IEnumerator ITrainUnit(GameObject unitPrefab, float trainDuration)
        {
            yield return new WaitForSeconds(trainDuration);

            owner.SpawnUnit(unitPrefab).Move(rallyPoint);
        }
    }
}