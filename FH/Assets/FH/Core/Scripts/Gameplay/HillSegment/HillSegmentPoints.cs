using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FH.Gameplay
{
    [DisallowMultipleComponent]
    public class HillSegmentPoints : MonoBehaviour
    {
        [SerializeField]
        float heightComplement;

        int themeId;

        public event System.Action OnPointsSet;

        List<Vector3> points = new List<Vector3>();
        List<NodeData> nodesData = new List<NodeData>();

        public int StartIndex { get; private set; }

        public int EndIndex { get; private set; }

        public float Width { get; private set; }

        public int PointsCount
        {
            get
            {
                return points.Count;
            }
        }

        public int ThemeId
        {
            get
            {
                return themeId;
            }
        }

        public Vector3 GetPoint(int index)
        {
            return points[index];
        }

        public NodeData GetNodeData(int index)
        {
            return nodesData[index];
        }

        public void SetPointsFromHeight(int startIndex, int numberOfPoints)
        {
            var heightModel = GameplayEntry.Instance.Model.Height;
            float currentX = 0;
            float intervalX = heightModel.NodeInterval;

            // Set theme
            float startPositionX = heightModel.GetPositionX(startIndex);
            themeId = GameplayEntry.Instance.Model.Theme.GetThemeAt(startPositionX);

            ///
            for (int heightIndex = startIndex; heightIndex < startIndex + numberOfPoints; heightIndex++)
            {
                Vector3 point = new Vector3(currentX, heightModel.GetHeight(heightIndex) + heightComplement, 0);
                int listIndex = heightIndex - startIndex;

                if (listIndex < points.Count)
                {
                    points[listIndex] = point;
                    nodesData[listIndex] = heightModel.GetNodeData(heightIndex);
                }
                else
                {
                    points.Add(point);
                    nodesData.Add(heightModel.GetNodeData(heightIndex));
                }

                currentX += intervalX;
            }

            ///
            Width = currentX - intervalX;

            ///
            StartIndex = startIndex;
            EndIndex = startIndex + numberOfPoints - 1;

            ///
            if (OnPointsSet != null)
            {
                OnPointsSet();
            }
        }
    }

}