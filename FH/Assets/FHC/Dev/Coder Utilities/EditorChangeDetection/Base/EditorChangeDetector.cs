using UnityEngine;
using System.Collections;
using System;
using System.Linq;

namespace FH.DevTool
{
    public abstract class EditorChangeDetector : MonoBehaviour, IEditorChangeDetector
    {
        event ChangeDetectedEventHandler ChangeDetected;
        bool changed = false;

        protected bool Changed
        {
            get
            {
                return changed;
            }

            set
            {
                changed = value;
            }
        }

        public void AddListenerWithDulicationCheck(ChangeDetectedEventHandler listener)
        {
            if (ChangeDetected != null && ChangeDetected.GetInvocationList().Contains(listener))
            {
                return;
            }
            ChangeDetected += listener;
        }

        public void Reset()
        {
            Changed = false;
            OnReset();
        }

        protected void DetectedChanged()
        {
            if (!Changed)
            {
                Changed = true;
                if (ChangeDetected != null)
                {
                    ChangeDetected(this);
                }
            }
        }

        public abstract void OnReset();
    }

}