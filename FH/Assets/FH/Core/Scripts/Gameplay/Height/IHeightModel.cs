using UnityEngine;
using System.Collections;

namespace FH.Gameplay
{
    public interface IHeightModel
    {
        IHeightSpeedYModifer SpeedYModifier { get; set; }

        float DeltaTime { get; }

        int NodesCount { get; }

        float NodeInterval { get; }

        float Gravity { get; set; }

        float CurrentSpeedY { get; set; }

        float CurrentY { get; }

        float CurrentX { get; }

        void SetIntialHeight(float height);

        float GetHeight(int nodeIndex);

        float GetPositionX(int nodeIndex);

        int GetNearestNode(float x);

        int GetNearestNodeBackward(float x);

        int GetNearestNodeForward(float x);

        NodeData GetNodeData(int nodeIndex);

        void UpdateByNodes(int numberOfNewNodes);

        void UpdateByDistance(float horizontalDistance);

        void Cull(int startIndex, int count);
    }

}