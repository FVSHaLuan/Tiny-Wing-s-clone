using UnityEngine;
using System.Collections;
using System;

namespace FH.DevTool
{
    [ExecuteInEditMode]
    public class LocalTransformChangeDetector : EditorChangeDetector
    {
        Vector3 savedLocalPosition;
        Vector3 savedLocalRotation;
        Vector3 savedLocalScale;

        public override void OnReset()
        {
            SaveState();
        }

        void SaveState()
        {
            savedLocalPosition = transform.localPosition;
            savedLocalRotation = transform.localRotation.eulerAngles;
            savedLocalScale = transform.localScale;
        }

        bool CheckChange()
        {
            if (savedLocalPosition != transform.localPosition)
            {
                return true;
            }

            if (savedLocalRotation != transform.localRotation.eulerAngles)
            {
                return true;
            }

            if (savedLocalScale != transform.localScale)
            {
                return true;
            }

            return false;
        }

        void Awake()
        {
            SaveState();
        }

        void Update()
        {
            if (!Changed && CheckChange())
            {
                DetectedChanged();
            }
        }
    }
}
