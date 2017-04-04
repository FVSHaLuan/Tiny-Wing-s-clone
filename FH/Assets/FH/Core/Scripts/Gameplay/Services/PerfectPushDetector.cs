using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FH.Gameplay.Service
{
    public class PerfectPushDetector : MonoBehaviour
    {
        public event System.Action OnStartPerfectPush;
        public event System.Action OnCancelPerfectPush;
        public event System.Action OnCompletePerfectPush;

        [SerializeField]
        [Range(0, 10)]
        int startNodeIntervalTolerance = 5;
        [SerializeField]
        int finishNodeIntervalTolerance = 2;
        [SerializeField]
        float miniumHeight = 2.0f;
        [SerializeField]
        int maxiumNodeInterval = 50;

        PlayerCurrentNearestNode playerCurrentNearestNode;
        InputStatus inputStatus;
        IHeightModel heightModel;

        int currentExpectedTargetNodeId;
        NodeData currentExpectedTargetNodeData;

        public void Awake()
        {
            playerCurrentNearestNode = GameplayEntry.Instance.Player.CurrentNearestNode;
            inputStatus = GameplayEntry.Instance.Model.InputStatus;
            heightModel = GameplayEntry.Instance.Model.Height;

            GameplayEntry.Instance.Model.InputStatus.OnEndHolding += InputStatus_OnEndHolding;
            GameplayEntry.Instance.Player.AirState.OnEndAir += AirState_OnEndAir;
        }

        private void AirState_OnEndAir()
        {
            // Landing while holding input?
            if (!inputStatus.Holding)
            {
                return;
            }

            // Tolerable start node?
            var currentNodeData = playerCurrentNearestNode.NearestNodeForwardData;
            if (currentNodeData.LastExtremeness != Extremeness.Maximum)
            {
                return;
            }
            if (currentNodeData.LastExtremeNodeInterval > startNodeIntervalTolerance)
            {
                return;
            }


            // Find expected target
            int currentNodeId = playerCurrentNearestNode.NearestNodeForwardId;
            int maxNodeId = currentNodeId + maxiumNodeInterval;
            bool foundTarget = false;
            //--
            for (int i = currentNodeId + 1; i < maxNodeId; i++)
            {
                currentExpectedTargetNodeData = heightModel.GetNodeData(i);
                if (currentExpectedTargetNodeData.Extremeness == Extremeness.Minimum)
                {
                    if ((currentNodeData.Height - currentExpectedTargetNodeData.Height) >= miniumHeight)
                    {
                        foundTarget = true;
                        currentExpectedTargetNodeId = i;
                    }
                    break;
                }
            }
            //--
            if (!foundTarget)
            {
                return;
            }

            ///
            if (OnStartPerfectPush != null)
            {
                OnStartPerfectPush();
            }
        }

        private void InputStatus_OnEndHolding()
        {
            ///
            int currentNodeId = playerCurrentNearestNode.NearestNodeForwardId;
            if (Mathf.Abs(currentNodeId - currentExpectedTargetNodeId) > finishNodeIntervalTolerance)
            {
                if (OnCancelPerfectPush!=null)
                {
                    OnCancelPerfectPush();
                }
                return;
            }

            ///
            if (OnCompletePerfectPush != null)
            {
                OnCompletePerfectPush();
            }
        }
    }

}