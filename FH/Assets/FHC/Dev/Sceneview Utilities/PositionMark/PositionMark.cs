using UnityEngine;
using System.Collections;

namespace FH.DevTool.SceneViewUtility
{
    public class PositionMark : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField]
        public string customLabel;
        [SerializeField]
        public bool markWhenNotSelected = false;

        public void OnDrawGizmosSelected()
        {
            if (!markWhenNotSelected)
            {
                Gizmos.DrawIcon(transform.position, "PositionMarker.png", false);
            }
        }

        public void OnDrawGizmos()
        {
            if (markWhenNotSelected)
            {
                Gizmos.DrawIcon(transform.position, "PositionMarker.png", false);
            }
        }
#endif
    }

}