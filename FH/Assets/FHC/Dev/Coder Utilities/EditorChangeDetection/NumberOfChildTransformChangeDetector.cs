using UnityEngine;
using System.Collections;
using System;

namespace FH.DevTool
{
    [ExecuteInEditMode]
    public class NumberOfChildTransformChangeDetector : EditorChangeDetector
    {
        int savedNumberOfChildren = -1;

        public override void OnReset()
        {
            savedNumberOfChildren = transform.childCount;
        }

        void Update()
        {
            if (savedNumberOfChildren != transform.childCount)
            {
                DetectedChanged();
            }
        }
    }

}