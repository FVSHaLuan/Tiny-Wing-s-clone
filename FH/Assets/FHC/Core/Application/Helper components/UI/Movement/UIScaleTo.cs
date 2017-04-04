using UnityEngine;
using System.Collections;
using FH.Core.Architecture;

namespace FH.Core.HelperComponent
{
    public class UIScaleTo : OutsiteTargetRectTransform
    {
        [SerializeField]
        OrderedEventDispatcher onFinishScaling;

        [Space]
        [SerializeField]
        Vector3 desireTargetScale;
        [SerializeField]
        float duration = 0.5f;
        [SerializeField]
        bool useX;
        [SerializeField]
        bool useY;
        [SerializeField]
        bool useZ;

        [ContextMenu("Scale")]
        public void Scale()
        {
            StartCoroutine(ScaleAsync());
        }
                
        IEnumerator ScaleAsync()
        {
            float currentTime = 0;

            Vector3 startScale = TargetRectTransform.localScale;
            Vector3 targetScale = startScale;
            if (useX)
            {
                targetScale.x = desireTargetScale.x;
            }
            if (useY)
            {
                targetScale.y = desireTargetScale.y;
            }
            if (useZ)
            {
                targetScale.z = desireTargetScale.z;
            }

            while (currentTime < duration)
            {
                currentTime = Mathf.MoveTowards(currentTime, duration, Time.deltaTime);
                TargetRectTransform.localScale = Vector3.Lerp(startScale, targetScale, currentTime / duration);
                yield return new WaitForEndOfFrame();
            }
            onFinishScaling.Dispatch();
            yield return null;
        }
    }

}