using UnityEngine;
using System.Collections;
using FH.Gameplay;

namespace FH.Test
{
    public class Test_Height : MonoBehaviour
    {
        [SerializeField]
        LineRenderer lineRenderer;
        [SerializeField]
        int numberOfNodes = 20;

        IHeightModel heightModel;
        Vector3[] points;
        int currentIndex = 0;

        public void Awake()
        {
            heightModel = Gameplay.GameplayEntry.Instance.Model.Height;
            points = new Vector3[numberOfNodes];
            lineRenderer.SetVertexCount(numberOfNodes);
        }

        public void Update()
        {
            UpdatePoints();
            lineRenderer.SetPositions(points);
            currentIndex++;
        }

        void UpdatePoints()
        {
            float currentX = 0;
            heightModel.UpdateByNodes(5);
            for (int i = currentIndex; i < currentIndex + numberOfNodes; i++)
            {
                points[i - currentIndex] = new Vector3(currentX, heightModel.GetHeight(i));
                currentX += heightModel.NodeInterval;
            }
        }
    }

}