using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace FH.Core.HelperComponent
{
    [RequireComponent(typeof(Graphic))]
    public class UIBlinker : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        Graphic targetRenderer;
        [SerializeField]
        float interval = 0.2f;

        float timeTracking = 0;
        public void Update()
        {
            timeTracking += Time.deltaTime;
            if (timeTracking >= interval)
            {
                timeTracking = 0;
                targetRenderer.enabled = !targetRenderer.enabled;
            }
        }

        public void Reset()
        {
            targetRenderer = GetComponent<Graphic>();
        }
    }

}