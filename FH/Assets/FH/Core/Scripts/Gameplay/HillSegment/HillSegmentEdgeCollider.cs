using UnityEngine;
using System.Collections;

namespace FH.Gameplay
{
    [RequireComponent(typeof(EdgeCollider2D), typeof(HillSegmentPoints))]
    public class HillSegmentEdgeCollider : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        EdgeCollider2D edgeCollider2D;
        [SerializeField, HideInInspector]
        HillSegmentPoints hillSegmentPoints;

        Vector2[] points;

        #region MonoB

        public void Awake()
        {
            hillSegmentPoints.OnPointsSet += HillSegmentPoints_OnPointsSet;
            SetEdgeCollider2DContent();
        }

        public void Reset()
        {
            edgeCollider2D = GetComponent<EdgeCollider2D>();
            hillSegmentPoints = GetComponent<HillSegmentPoints>();
        }
        #endregion

        void HillSegmentPoints_OnPointsSet()
        {
            SetEdgeCollider2DContent();
        }

        void SetEdgeCollider2DContent()
        {
            RenewArrayIfNeeded();

            if (points.Length == 0)
            {
                return;
            }

            for (int i = 0; i < points.Length; i++)
            {
                points[i] = hillSegmentPoints.GetPoint(i);
            }

            edgeCollider2D.points = points;
        }

        void RenewArrayIfNeeded()
        {
            if (points == null || points.Length != hillSegmentPoints.PointsCount)
            {
                points = new Vector2[hillSegmentPoints.PointsCount];
            }
        }


    }

}