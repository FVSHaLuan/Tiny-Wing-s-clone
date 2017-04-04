using UnityEngine;
using System.Collections;
using System;

namespace FH.DevTool.SceneViewUtility
{
    public class RectangleGizmosIllustrator : GizmosIllustrator
    {
        [SerializeField]
        Vector2 offset = Vector2.zero;
        [SerializeField]
        Vector2 size = Vector2.one;

        protected override void DrawGizmos()
        {
            Gizmos.DrawWireCube(TargetTransform.position + (Vector3)offset, size);
        }
    }

}