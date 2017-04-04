using UnityEngine;
using System.Collections;
using System;

namespace FH.DevTool
{
    [ExecuteInEditMode]
    public class GameObjectActiveChangeDetector : EditorChangeDetector
    {
        bool savedState;

        public override void OnReset()
        {
            savedState = gameObject.activeSelf;
        }

        public void OnEnable()
        {
            if (savedState != gameObject.activeSelf)
            {
                DetectedChanged();
            }
        }

        public void OnDisable()
        {
            if (savedState != gameObject.activeSelf)
            {
                DetectedChanged();
            }
        }
    }

}