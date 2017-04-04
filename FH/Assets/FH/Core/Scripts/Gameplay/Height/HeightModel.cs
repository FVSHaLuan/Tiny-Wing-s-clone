using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace FH.Gameplay
{
    public class HeightModel : MonoBehaviour, IHeightModel, ISerializationCallbackReceiver
    {
        [SerializeField, Range(1, 100)]
        float speedX;
        [SerializeField]
        float nodeInterval;
        [SerializeField]
        float deltaTime;
        float speedY;
        float gravity;

        [Header("Initial position")]
        [SerializeField, UnityEngine.Serialization.FormerlySerializedAs("currentX")]
        float initialX;
        [SerializeField, UnityEngine.Serialization.FormerlySerializedAs("currentY")]
        float initialY;

        float currentX;
        float currentY;

        int nodesCount = 0;

        float nodeIntervalTracking = 0;

        Dictionary<int, NodeData> nodes = new Dictionary<int, NodeData>();

        IHeightSpeedYModifer speedYModifier;

        bool setInitialHeight = false;

        int minIndex = -1;

        NodeData lastSavedNodeData;

        Extremeness lastExtremeness = Extremeness.None;
        int lastExtremeNodeInterval = -1;

        #region IHeightModel
        public float CurrentSpeedY
        {
            get
            {
                return speedY;
            }

            set
            {
                speedY = value;
            }
        }

        public int NodesCount
        {
            get
            {
                return nodesCount;
            }
        }

        public IHeightSpeedYModifer SpeedYModifier
        {
            get
            {
                return speedYModifier;
            }

            set
            {
                speedYModifier = value;
                speedYModifier.Activate(this);
            }
        }

        public float CurrentX
        {
            get
            {
                return currentX;
            }
        }

        public float NodeInterval
        {
            get
            {
                return nodeInterval;
            }
        }

        public float CurrentY
        {
            get
            {
                return currentY;
            }
        }

        public float Gravity
        {
            get
            {
                return gravity;
            }

            set
            {
                gravity = value;
            }
        }

        public float DeltaTime
        {
            get
            {
                return deltaTime;
            }
        }

        public void SetIntialHeight(float height)
        {
            ///
            if (setInitialHeight)
            {
                throw new System.NotImplementedException();
            }

            ///
            currentY = height;
            setInitialHeight = true;
        }

        public float GetHeight(int nodeIndex)
        {
            try
            {
                return nodes[nodeIndex].Height;
            }
            catch (Exception)
            {
                throw new Exception(string.Format("nodeIndex {0} not found", nodeIndex));
            }
        }

        public float GetPositionX(int nodeIndex)
        {
            try
            {
                return nodes[nodeIndex].PositionX;
            }
            catch (Exception)
            {
                throw new Exception(string.Format("nodeIndex {0} not found", nodeIndex));
            }
        }

        public int GetNearestNode(float x)
        {
            Assert.IsTrue(x >= initialX);

            int backwardNode = (int)((x - initialX) / nodeInterval);

            if ((x - initialX) % nodeInterval > (nodeInterval / 2.0f))
            {
                return backwardNode + 1;
            }

            return backwardNode;
        }

        public int GetNearestNodeBackward(float x)
        {
            Assert.IsTrue(x >= initialX);

            return (int)((x - initialX) / nodeInterval);
        }

        public int GetNearestNodeForward(float x)
        {
            Assert.IsTrue(x >= initialX);

            return (int)((x - initialX) / nodeInterval) + 1;
        }

        public NodeData GetNodeData(int nodeIndex)
        {
            try
            {
                return nodes[nodeIndex];
            }
            catch (Exception)
            {
                throw new Exception(string.Format("nodeIndex {0} not found", nodeIndex));
            }
        }

        public void UpdateByNodes(int numberOfNewNodes)
        {
            int newNodesCount = nodesCount + numberOfNewNodes;
            while (nodesCount != newNodesCount)
            {
                UpdateStep();
            }
        }

        public void UpdateByDistance(float horizontalDistance)
        {
            float newX = currentX + horizontalDistance;
            while (currentX < newX)
            {
                UpdateStep();
            }
        }

        public void Cull(int startIndex, int endIndex)
        {
            ///
            if (minIndex < 0)
            {
                return;
            }

            ///
            startIndex = Mathf.Max(startIndex, minIndex);
            endIndex = Math.Min(endIndex, NodesCount - 1);

            ///
            for (int i = startIndex; i <= endIndex; i++)
            {
                nodes.Remove(i);
            }

            ///
            if ((endIndex + 1) == NodesCount)
            {
                minIndex = -1;
            }
            else
            {
                minIndex = endIndex + 1;
            }
        }

        #endregion

        void UpdateStep()
        {
            // Update ISpeedYModifier
            if (speedYModifier != null)
            {
                speedYModifier.GetNewSpeedY(this);
            }

            // Update speedY
            speedY += Gravity * deltaTime;

            ///
            currentY += speedY * deltaTime;

            ///
            float deltaX = speedX * deltaTime;
            currentX += deltaX;

            ///
            nodeIntervalTracking += deltaX;
            if (nodeIntervalTracking >= nodeInterval)
            {
                nodeIntervalTracking = 0;
                SaveCurrentAsNewNode();
            }
        }

        void SaveCurrentAsNewNode()
        {
            // Determine extremeness            
            Extremeness extremeness = Extremeness.None;
            if (nodesCount > 0)
            {
                if (lastSavedNodeData.Velocity.y > 0 && speedY <= 0)
                {
                    extremeness = Extremeness.Maximum;
                }
                if (lastSavedNodeData.Velocity.y < 0 && speedY >= 0)
                {
                    extremeness = Extremeness.Minimum;
                }
            }

            ///
            if (extremeness != Extremeness.None)
            {
                lastExtremeness = extremeness;
                lastExtremeNodeInterval = 0;
            }
            else
            {
                lastExtremeNodeInterval++;
            }

            ///
            nodesCount++;
            NodeData nodeData = new NodeData(currentY, currentX, new Vector2(speedX, speedY), extremeness, lastExtremeness, lastExtremeNodeInterval);
            nodes.Add(nodesCount - 1, nodeData);

            ///
            if (minIndex < 0)
            {
                minIndex = nodesCount - 1;
            }

            ///
            lastSavedNodeData = nodeData;
        }

        #region ISerializationCallbackReceiver
        public void OnBeforeSerialize()
        {

        }

        public void OnAfterDeserialize()
        {
            currentX = initialX;
            currentY = initialY;
        }
        #endregion
    }

}