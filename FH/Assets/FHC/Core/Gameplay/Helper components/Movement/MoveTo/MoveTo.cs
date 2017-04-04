using UnityEngine;
using System.Collections;

namespace FH.Core.Gameplay.HelperComponent
{
    public class MoveTo : OutsiteTargetTransform
    {
        [SerializeField]
        float duration;
        [SerializeField]
        PositionProvider destination;

        bool moving = false;
        float currentTime = 0;
        float step;

        void OnEnable()
        {
            if (!moving)
            {
                enabled = false;
            }
        }

        public void StartMove()
        {
            currentTime = 0;
            moving = true;
            step = Vector3.Distance(TargetPosition, destination.Position) / duration;
            enabled = true;
        }

        void Update()
        {
            currentTime = Mathf.MoveTowards(currentTime, duration, Time.deltaTime);
            //Vector3 
        }

    }

}