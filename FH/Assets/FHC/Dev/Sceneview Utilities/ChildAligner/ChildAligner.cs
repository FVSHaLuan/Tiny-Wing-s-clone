using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FH.DevTool.SceneViewUtility
{
    public class ChildAligner : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField]
        float left = 1;
        [SerializeField]
        float right = 1;
        [SerializeField]
        float up = 1;
        [SerializeField]
        float down = 1;

        public void OnDrawGizmos()
        {
            var selectedTransform = Selection.activeTransform;

            if (selectedTransform == null)
            {
                return;
            }

            if (selectedTransform == transform || selectedTransform.root == transform)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(transform.position + Vector3.left * left, transform.position);
                Gizmos.DrawLine(transform.position + Vector3.right * right, transform.position);
                Gizmos.DrawLine(transform.position + Vector3.up * up, transform.position);
                Gizmos.DrawLine(transform.position + Vector3.down * down, transform.position);
            }
        }
#endif
    }
}
