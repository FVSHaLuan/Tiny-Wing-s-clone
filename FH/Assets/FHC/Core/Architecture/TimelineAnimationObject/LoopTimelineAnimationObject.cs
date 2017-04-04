using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Architecture
{
    public abstract class LoopTimelineAnimationObject : MonoBehaviour, ITimelineAnimationObject
    {
        [SerializeField, Range(0, 1)]
        float currentPosition;

        float lastUpdatedPosition = float.NaN;

        #region ITimelineAnimationObject
        public float CurrentPosition
        {
            get
            {
                return currentPosition;
            }

            set
            {
                currentPosition = value;
            }
        }
        #endregion

        #region MonoB
        public void Update()
        {
            UpdateState(currentPosition, currentPosition != lastUpdatedPosition);
            lastUpdatedPosition = currentPosition;
        }
        #endregion

        protected abstract void UpdateState(float currentPosition, bool newPosition);
    }

}