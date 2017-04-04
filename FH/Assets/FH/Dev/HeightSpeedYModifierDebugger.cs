using UnityEngine;
using System.Collections;
using FH.Gameplay;
using System;
using System.Collections.Generic;
using FH.DevTool.SceneViewUtility;
using FH.Gameplay.Controller;

namespace FH.Dev
{
    public class HeightSpeedYModifierDebugger : GizmosIllustrator
    {
        [Header("HeightSpeedYModifierDebugger")]
        [SerializeField]
        HeightSpeedYModifer heightSpeedYModifer;
        [SerializeField, Range(1, 100)]
        float speedX = 12;
        [SerializeField]
        float deltaTime = 1 / 60.0f;
        [SerializeField]
        float nodeInterval = 0.5f;
        [SerializeField, Range(2, 2000)]
        int nodesCount = 50;
        [SerializeField]
        float initialHeight;
        [SerializeField]
        float initialSpeedY;
        [SerializeField]
        float initialGravity;
        [SerializeField]
        Vector3 drawOffset;
        [SerializeField]
        float heightOffset = 10;
        [SerializeField]
        bool generateDataManually = false;

        HeightModel heightModel;

        [ContextMenu("GenerateData")]
        void GenerateData()
        {
            ///
            if (heightModel == null)
            {
                heightModel = new HeightModel();
            }

            ///
            heightModel.CurrentSpeedY = initialSpeedY;
            heightModel.Gravity = initialGravity;

            ///
            IHeightSpeedYModifer heightSpeedYModifer;
            if (this.heightSpeedYModifer != null)
            {
                heightSpeedYModifer = this.heightSpeedYModifer;
            }
            else
            {
                heightSpeedYModifer = GetComponent<IHeightSpeedYModifer>();
            }
            heightModel.GenerateHeights(speedX, deltaTime, nodeInterval, heightSpeedYModifer, initialHeight, nodesCount);
        }

        void Draw()
        {
            Color[] colors = { Color.red, Color.yellow };
            int currentColorIndex = 0;

            var savedColor = Gizmos.color;

            Vector3 startPoint = transform.position + drawOffset;
            float baseY = startPoint.y;
            float baseZ = startPoint.z;

            ///
            float currentX = startPoint.x;
            Vector3 lastPoint = startPoint;

            for (int i = 0; i < heightModel.NodesCount; i++)
            {
                Vector3 currentPoint = new Vector3()
                {
                    x = currentX,
                    y = baseY + heightModel.GetHeight(i) + heightOffset,
                    z = baseZ
                };

                if (i > 0)
                {
                    if (heightModel.ShouldChangeColor(i))
                    {
                        currentColorIndex = 1 - currentColorIndex;
                    }
                    Gizmos.color = colors[currentColorIndex];
                }
                else
                {
                    Gizmos.color = Color.green;
                }

                Gizmos.DrawLine(lastPoint, currentPoint);

                currentX += heightModel.NodeInterval;
                lastPoint = currentPoint;
            }

            ///
            Vector3 endPoint = lastPoint;
            endPoint.y = startPoint.y;
            Gizmos.color = Color.green;
            Gizmos.DrawLine(lastPoint, endPoint);
            Gizmos.DrawLine(endPoint, startPoint);
            Gizmos.color = savedColor;
        }

        public void Update()
        {
            // This method is meant to exist
        }

        protected override void DrawGizmos()
        {
#if UNITY_EDITOR

            if (!enabled)
            {
                return;
            }

            if (UnityEditor.EditorApplication.isPlaying)
            {
                return;
            }

            if (!generateDataManually || heightModel == null)
            {
                GenerateData();
            }
            Draw();
#endif
        }

        class HeightModel : IHeightModel
        {
            float currentSpeedY;
            float currentX;
            float currentY;
            float deltaTime;
            float gravity;
            float nodeInterval;
            IHeightSpeedYModifer speedYModifier;

            List<float> heights = new List<float>();
            HashSet<int> changeHeightSpeedYModiferNodes = new HashSet<int>();

            #region HeightModel
            public float CurrentSpeedY
            {
                get
                {
                    return currentSpeedY;
                }

                set
                {
                    currentSpeedY = value;
                }
            }

            public float CurrentX
            {
                get
                {
                    return currentX;
                }
            }

            public float CurrentY
            {
                get
                {
                    return currentY;
                }
            }

            public float DeltaTime
            {
                get
                {
                    return deltaTime;
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

            public float NodeInterval
            {
                get
                {
                    return nodeInterval;
                }
            }

            public int NodesCount
            {
                get
                {
                    return heights.Count;
                }
            }

            public IHeightSpeedYModifer SpeedYModifier
            {
                get
                {
                    throw new NotImplementedException();
                }

                set
                {
                    speedYModifier = value;
                    speedYModifier.Activate(this);
                    if (heights.Count > 0)
                    {
                        changeHeightSpeedYModiferNodes.Add(heights.Count - 1);
                    }
                }
            }

            public void Cull(int startIndex, int count)
            {
                throw new NotImplementedException();
            }

            public float GetHeight(int nodeIndex)
            {
                return heights[nodeIndex];
            }

            public float GetPositionX(int nodeIndex)
            {
                throw new NotImplementedException();
            }
            
            public NodeData GetNodeData(int nodeIndex)
            {
                throw new NotImplementedException();
            }

            public int GetNearestNode(float x)
            {
                throw new NotImplementedException();
            }

            public int GetNearestNodeBackward(float x)
            {
                throw new NotImplementedException();
            }

            public int GetNearestNodeForward(float x)
            {
                throw new NotImplementedException();
            }

            public void SetIntialHeight(float height)
            {
                throw new NotImplementedException();
            }

            public void UpdateByDistance(float horizontalDistance)
            {
                throw new NotImplementedException();
            }

            public void UpdateByNodes(int numberOfNewNodes)
            {
                throw new NotImplementedException();
            }
            #endregion

            public bool ShouldChangeColor(int nodeIndex)
            {
                return changeHeightSpeedYModiferNodes.Contains(nodeIndex);
            }

            public void GenerateHeights(float speedX, float deltaTime, float nodeInterval, IHeightSpeedYModifer speedYModifier, float initialHeight, int amount)
            {
                this.deltaTime = deltaTime;
                this.nodeInterval = nodeInterval;
                this.speedYModifier = speedYModifier;



                float nodeIntervalTracking = 0;

                changeHeightSpeedYModiferNodes.Clear();
                heights.Clear();
                currentX = 0;
                currentY = initialHeight;

                speedYModifier.Activate(this);

                while (heights.Count < amount)
                {
                    UpdateStep(speedX);

                    ///
                    nodeIntervalTracking += speedX * deltaTime;
                    if (nodeIntervalTracking >= nodeInterval)
                    {
                        nodeIntervalTracking = 0;
                        heights.Add(currentY);
                    }
                }
            }

            void UpdateStep(float speedX)
            {
                // Update ISpeedYModifier
                if (speedYModifier != null)
                {
                    speedYModifier.GetNewSpeedY(this);
                }

                // Update speedY
                currentSpeedY += Gravity * deltaTime;

                ///
                currentY += currentSpeedY * deltaTime;

                ///
                float deltaX = speedX * deltaTime;
                currentX += deltaX;

            }
        }
    }

}