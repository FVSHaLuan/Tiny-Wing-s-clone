using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FH.DevTool.SceneViewUtility
{
    [ExecuteInEditMode]
    public abstract class GizmosIllustrator : MonoBehaviour
    {
        [SerializeField]
        Color color = Color.green;
        [SerializeField]
        bool alwaysDraw;
        [SerializeField]
        Transform rootTransform;
        [SerializeField]
        Transform targetTransform;

        protected Transform TargetTransform
        {
            get
            {
                return targetTransform;
            }

            set
            {
                targetTransform = value;
            }
        }

        protected abstract void DrawGizmos();

        bool shouldDrawGizmos = false;

        void Awake()
        {
#if UNITY_EDITOR
            Selection.selectionChanged += OnSelectionChanged;
            CheckShouldDrawGizmos();
#endif
        }

        void Draw()
        {
            Color savedColor = Gizmos.color;
            Gizmos.color = color;
            DrawGizmos();
            Gizmos.color = savedColor;
        }

        void OnSelectionChanged()
        {
            CheckShouldDrawGizmos();
        }

        void CheckShouldDrawGizmos()
        {
#if UNITY_EDITOR
            if (alwaysDraw)
            {
                shouldDrawGizmos = true;
            }
            else
            {
                shouldDrawGizmos = false;
                for (int i = 0; i < Selection.transforms.Length; i++)
                {
                    if (ObjectHelper.IsChildOrSelf(rootTransform, Selection.transforms[i]))
                    {
                        shouldDrawGizmos = true;
                        return;
                    }
                }

            }

#else
            return;
#endif
        }

        public void OnDrawGizmos()
        {
            if (shouldDrawGizmos)
            {
                Draw();
            }
        }

        public void Reset()
        {
            TargetTransform = transform;
            rootTransform = transform;
        }

        protected void ForceDraw()
        {
#if UNITY_EDITOR
            SceneView.RepaintAll();
#endif
        }
    }

}