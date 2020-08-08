using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class CommandCenter : Building
    {
        static int ID;

        protected override void Awake()
        {
            base.Awake();

            trainDuration = 2f;

            ID++;
            gameObject.name = "Command Center " + ID;

            BuildingUIInfo.AddButtonAction(TrainSCV);
            BuildingUIInfo.AddButtonAction(TrainMarine);
        }
    }
}
