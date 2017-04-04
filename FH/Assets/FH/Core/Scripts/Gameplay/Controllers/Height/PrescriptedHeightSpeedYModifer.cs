using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace FH.Gameplay.Controller
{
    public class PrescriptedHeightSpeedYModifer : ModularHeightSpeedYModifier
    {
        [Serializable]
        struct ScriptedPoint
        {
            [SerializeField]
            float delay;
            [SerializeField]
            float gravity;

            public float Delay
            {
                get
                {
                    return delay;
                }

            }

            public float Gravity
            {
                get
                {
                    return gravity;
                }
            }
        }

        [Header("PrescriptedHeightSpeedYModifer")]
        [SerializeField]
        List<ScriptedPoint> scriptedPoints = new List<ScriptedPoint>();

        [Header("Switching condition")]
        [SerializeField]
        float switchingDelay = 1;

        int currentScriptedPointIndex;
        float nextCheckPoint;
        float timeTracking;

        protected override void OnActivate(IHeightModel heightModel)
        {
            currentScriptedPointIndex = 0;
            timeTracking = 0;
            nextCheckPoint = GetNextCheckPoint();
        }

        public override void OnGetNewSpeedY(IHeightModel heightModel)
        {
            if (timeTracking >= nextCheckPoint && currentScriptedPointIndex < scriptedPoints.Count)
            {
                heightModel.Gravity = scriptedPoints[currentScriptedPointIndex].Gravity;

                currentScriptedPointIndex++;

                nextCheckPoint = GetNextCheckPoint();

                timeTracking = 0;
            }

            ///
            timeTracking += heightModel.DeltaTime;
        }

        protected override bool ShouldSwitch(IHeightModel heightModel)
        {
            return (timeTracking > nextCheckPoint) && (currentScriptedPointIndex == scriptedPoints.Count);
        }

        float GetNextCheckPoint()
        {
            if (currentScriptedPointIndex < scriptedPoints.Count)
            {
                return scriptedPoints[currentScriptedPointIndex].Delay;
            }
            else
            {
                return switchingDelay;
            }
        }
    }

}