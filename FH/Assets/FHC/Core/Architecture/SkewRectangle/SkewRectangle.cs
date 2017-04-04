using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture
{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    [ExecuteInEditMode]
    public class SkewRectangle : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        MeshRenderer meshRenderer;
        [SerializeField, HideInInspector]
        MeshFilter meshFilter;

        [SerializeField]
        Vector3 v0 = Vector3.left + Vector3.up;
        [SerializeField]
        Vector3 v1 = Vector3.right + Vector3.up;
        [SerializeField]
        Vector3 v2 = Vector3.right + Vector3.down;
        [SerializeField]
        Vector3 v3 = Vector3.left + Vector3.down;

        [SerializeField]
        Mesh mesh = null;

        Mesh CreateMesh()
        {
            mesh = new Mesh();

            // Vertices
            mesh.vertices = GetVerticesArray();

            // Triangles
            mesh.triangles = new int[]
            {
                0,1,2,
               3,4,5
            };

            // uv
            mesh.uv = new Vector2[]
            {
                new Vector2(0,1),new Vector2(1,1),new Vector2(1,0),
               new Vector2(0,1),new Vector2(1,0), new Vector2(0,0)
            };

            // uv2
            mesh.uv2 = new Vector2[]
            {
                new Vector2(1,0),new Vector2(Mathf.Sqrt(2),0),new Vector2(1,0),
               new Vector2(1,0),new Vector2(1,0), new Vector2(Mathf.Sqrt(2),0)
            };

            // Return
            return mesh;
        }

        void UpdateMesh()
        {
            if (mesh == null)
            {
                return;
            }

            // Vertices
            mesh.vertices = GetVerticesArray();
        }

        Vector3[] GetVerticesArray()
        {
            return new Vector3[]
            {
                v0,v1,v2,
                v0,v2,v3
            };
        }

        public void OnDrawGizmos()
        {
            UpdateMesh();
        }

        public void Reset()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            meshFilter = GetComponent<MeshFilter>();
            mesh = CreateMesh();
            meshFilter.mesh = mesh;
        }
    }

}