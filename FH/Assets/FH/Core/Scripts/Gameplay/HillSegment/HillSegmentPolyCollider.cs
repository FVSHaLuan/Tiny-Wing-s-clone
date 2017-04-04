using UnityEngine;
using System.Collections;

namespace FH.Gameplay
{
    [RequireComponent(typeof(PolygonCollider2D), typeof(HillSegmentPoints))]
    public class HillSegmentPolyCollider : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        PolygonCollider2D polygonCollider2D;
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
            polygonCollider2D = GetComponent<PolygonCollider2D>();
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

            ///
            if (points.Length == 0)
            {
                return;
            }

            ///
            for (int i = 0; i < points.Length - 2; i++)
            {
                points[i + 1] = hillSegmentPoints.GetPoint(i);
            }

            ///
            {
                var p = points[1];
                p.y = 0;
                points[0] = p;

                p = points[points.Length - 2];
                p.y = 0;
                points[points.Length - 1] = p;
            }

            ///
            polygonCollider2D.SetPath(0, points);
        }

        void RenewArrayIfNeeded()
        {
            if (points == null || points.Length != hillSegmentPoints.PointsCount + 2)
            {
                points = new Vector2[hillSegmentPoints.PointsCount + 2];
            }
        }
    }

}