using UnityEngine;
using System.Collections;
using System;

namespace FH.Gameplay.Controller
{
    public class SteepHeightSpeedYModifier : ModularHeightSpeedYModifier
    {
        [Header("SteepHeightSpeedYModifier")]
        [SerializeField]
        float mainGravity = -5;
        [SerializeField]
        float spikeGravity = 5;
        [SerializeField]
        float mainGravityDuration = 3;
        [SerializeField]
        float spikeGravityDuration = 1.0f;

        [Header("Switching condition")]
        [SerializeField]
        SwitchingPolicy switchingPolicy;
        [SerializeField]
        float switchingHeight;

        enum SwitchingPolicy { HigherThan, LowerThan }
        enum GravityType { Main, Spike }

        float timeTracking;
        GravityType currentGravityType;

        protected override void OnActivate(IHeightModel heightModel)
        {
            heightModel.CurrentSpeedY = 0;
            heightModel.Gravity = mainGravity;
            timeTracking = 0;
            currentGravityType = GravityType.Main;
        }

        public override void OnGetNewSpeedY(IHeightModel heightModel)
        {
            ///
            switch (currentGravityType)
            {
                case GravityType.Main:
                    if (timeTracking > mainGravityDuration)
                    {
                        timeTracking = 0;
                        heightModel.Gravity = spikeGravity;
                        heightModel.CurrentSpeedY = 0;
                        currentGravityType = GravityType.Spike;
                    }
                    break;
                case GravityType.Spike:
                    if (timeTracking > spikeGravityDuration)
                    {
                        timeTracking = 0;
                        heightModel.Gravity = mainGravity;
                        currentGravityType = GravityType.Main;
                    }
                    break;
                default:
                    break;
            }


            ///
            timeTracking += heightModel.DeltaTime;
        }

        protected override bool ShouldSwitch(IHeightModel heightModel)
        {
            switch (switchingPolicy)
            {
                case SwitchingPolicy.HigherThan:
                    return heightModel.CurrentY > switchingHeight;
                case SwitchingPolicy.LowerThan:
                    return heightModel.CurrentY < switchingHeight;
                default:
                    throw new System.NotImplementedException();
            }
        }
    }

}