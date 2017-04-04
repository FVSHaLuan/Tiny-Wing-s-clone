using UnityEngine;
using System.Collections;

namespace FH.Gameplay
{
    [RequireComponent(typeof(MeshFilter), typeof(HillSegmentPoints))]
    public class HillSegmentMesh : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        MeshFilter meshFilter;
        [SerializeField, HideInInspector]
        HillSegmentPoints hillSegmentPoints;

        Vector3[] vertices;
        Vector2[] uvs;
        int[] triangles;
        Mesh mesh;

        #region MonoB
        public void Awake()
        {
            mesh = meshFilter.mesh;
            hillSegmentPoints.OnPointsSet += HillSegmentPoints_OnPointsSet;

            SetMeshContent();
        }

        public void Reset()
        {
            meshFilter = GetComponent<MeshFilter>();
            hillSegmentPoints = GetComponent<HillSegmentPoints>();
        }
        #endregion

        void HillSegmentPoints_OnPointsSet()
        {
            SetMeshContent();
        }

        void SetMeshContent()
        {
            RenewArrayIfNeeded();

            SetVeticesContent();
            SetUvsContent();
            SetTrianglesContent();

            mesh.vertices = vertices;
            mesh.uv = uvs;
            mesh.triangles = triangles;

            mesh.RecalculateBounds();
        }

        void SetVeticesContent()
        {
            int iterations = hillSegmentPoints.PointsCount;

            for (int i = 0; i < iterations; i++)
            {
                Vector3 upperPoint = hillSegmentPoints.GetPoint(i);
                Vector3 lowerPoint = upperPoint;
                lowerPoint.y = 0;
                vertices[i * 2] = lowerPoint;
                vertices[i * 2 + 1] = upperPoint;
            }
        }

        void SetUvsContent()
        {
            int iterations = hillSegmentPoints.PointsCount;

            for (int i = 0; i < iterations; i++)
            {
                float uvX = i / (float)(iterations - 1);
                uvs[i * 2] = new Vector2(uvX, 0);
                uvs[i * 2 + 1] = new Vector2(uvX, 1);
            }
        }

        void SetTrianglesContent()
        {
            int iterations = hillSegmentPoints.PointsCount;

            for (int i = 1; i < iterations; i++)
            {
                int lowerLeft = (i - 1) * 2;
                int upperLeft = (i - 1) * 2 + 1;
                int lowerRight = i * 2;
                int upperRight = i * 2 + 1;

                int firstTriangleStartIndex = (i - 1) * 2 * 3;
                int secondTriangleStartIndex = (i - 1) * 2 * 3 + 3;

                triangles[firstTriangleStartIndex] = lowerRight;
                triangles[firstTriangleStartIndex + 1] = upperLeft;
                triangles[firstTriangleStartIndex + 2] = lowerLeft;

                triangles[secondTriangleStartIndex] = lowerRight;
                triangles[secondTriangleStartIndex + 1] = upperRight;
                triangles[secondTriangleStartIndex + 2] = upperLeft;
            }
        }

        void RenewArrayIfNeeded()
        {
            int numberOfHillPoints = hillSegmentPoints.PointsCount;

            if (vertices == null || vertices.Length != (numberOfHillPoints * 2))
            {
                vertices = new Vector3[numberOfHillPoints * 2];
                uvs = new Vector2[numberOfHillPoints * 2];
                int numberOfTriangles = numberOfHillPoints >= 2 ? (numberOfHillPoints - 1) * 2 : 0;
                triangles = new int[numberOfTriangles * 3];
            }
        }
    }

}