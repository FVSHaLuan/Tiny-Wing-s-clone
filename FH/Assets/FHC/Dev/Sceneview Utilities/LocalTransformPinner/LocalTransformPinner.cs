using UnityEngine;
using System.Collections;

namespace FH.DevTool.SceneViewUtility
{
    [DisallowMultipleComponent]
    public class LocalTransformPinner : MonoBehaviour
    {
#if UNITY_EDITOR
        [Header("Position")]
        [SerializeField]
        public Vector3 position = Vector3.zero;
        [SerializeField]
        public bool pinX = true;
        [SerializeField]
        public bool pinY = true;
        [SerializeField]
        public bool pinZ = true;

        [Header("Rotation")]
        [SerializeField]
        public Vector3 rotation = Vector3.zero;
        [SerializeField]
        public bool pinRotationX = true;
        [SerializeField]
        public bool pinRotationY = true;
        [SerializeField]
        public bool pinRotationZ = true;

        [Header("Scale")]
        [SerializeField]
        public Vector3 scale = Vector3.one;
        [SerializeField]
        public bool pinScaleX = true;
        [SerializeField]
        public bool pinScaleY = true;
        [SerializeField]
        public bool pinScaleZ = true;

        bool active = true;

        public void Awake()
        {
            if (UnityEditor.EditorApplication.isPlaying)
            {
                enabled = false;
                active = false;
            }
        }

        public void OnDrawGizmos()
        {
            if (active)
            {
                PinPosition();
                PinRotation();
                PinScale();
            }
        }

        void PinPosition()
        {
            Vector3 p = transform.localPosition;
            if (pinX)
            {
                p.x = position.x;
            }
            if (pinY)
            {
                p.y = position.y;
            }
            if (pinZ)
            {
                p.z = position.z;
            }
            transform.localPosition = p;
        }

        void PinRotation()
        {
            Vector3 r = transform.rotation.eulerAngles;
            if (pinRotationX)
            {
                r.x = rotation.x;
            }
            if (pinRotationY)
            {
                r.y = rotation.y;
            }
            if (pinRotationZ)
            {
                r.z = rotation.z;
            }
            transform.rotation = Quaternion.Euler(r);
        }

        void PinScale()
        {
            Vector3 s = transform.localScale;
            if (pinScaleX)
            {
                s.x = scale.x;
            }
            if (pinScaleY)
            {
                s.y = scale.y;
            }
            if (pinScaleZ)
            {
                s.z = scale.z;
            }
            transform.localScale = s;
        }

        void Reset()
        {
            GetDefaultValues();
        }

        void GetDefaultValues()
        {
            position = transform.localPosition;
            rotation = transform.localEulerAngles;
            scale = transform.localScale;
        }
#endif
    }


}