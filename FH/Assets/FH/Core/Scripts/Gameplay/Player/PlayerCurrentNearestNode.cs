using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FH.Gameplay
{
    public class PlayerCurrentNearestNode : MonoBehaviour
    {
        const int NumberOfSkipFrames = 2;

        public int NearestNodeId { get; private set; }
        public int NearestNodeBackwardId { get; private set; }
        public int NearestNodeForwardId { get; private set; }

        public NodeData NearestNodeData { get; private set; }
        public NodeData NearestNodeBackwardData { get; private set; }
        public NodeData NearestNodeForwardData { get; private set; }

        public bool DataReady
        {
            get
            {
                return dataReady;
            }

            private set
            {
                dataReady = value;
            }
        }

        bool dataReady = false;

        IHeightModel heightModel;

        int skipFramesCount = 0;

        public void Awake()
        {
            heightModel = GameplayEntry.Instance.Model.Height;
            UpdateData();
        }

        public void LateUpdate()
        {
            UpdateData();
        }

        void UpdateData()
        {
            float currentX = transform.position.x;

            NearestNodeId = heightModel.GetNearestNode(currentX);
            NearestNodeBackwardId = heightModel.GetNearestNodeBackward(currentX);
            NearestNodeForwardId = heightModel.GetNearestNodeForward(currentX);

            if (skipFramesCount > NumberOfSkipFrames)
            {
                NearestNodeData = heightModel.GetNodeData(NearestNodeId);
                NearestNodeBackwardData = heightModel.GetNodeData(NearestNodeBackwardId);
                NearestNodeForwardData = heightModel.GetNodeData(NearestNodeForwardId);
                dataReady = true;
            }
            else
            {
                skipFramesCount++;
            }
        }

    }

}