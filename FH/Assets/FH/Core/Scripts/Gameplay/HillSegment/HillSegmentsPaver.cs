using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FH.Gameplay.Controller;

namespace FH.Gameplay
{
    public class HillSegmentsPaver : MonoBehaviour
    {
        [SerializeField]
        HillSegmentPoints hillSegmentPrototype;
        [SerializeField]
        int numberOfPointsInSegment;
        [SerializeField]
        Vector3 initialPosition;
        [SerializeField]
        Transform followingTransform;
        [SerializeField]
        float viewLeftWidth = 15;
        [SerializeField]
        float viewRightWidth = 15;

        List<HillSegmentPoints> activeSegments = new List<HillSegmentPoints>();
        List<HillSegmentPoints> inactiveSegments = new List<HillSegmentPoints>();
        IHeightModel heightModel;

        public HillSegmentPoints HillSegmentPrototype
        {
            get
            {
                return hillSegmentPrototype;
            }

            set
            {
                hillSegmentPrototype = value;
            }
        }

        public void Awake()
        {
            heightModel = GameplayEntry.Instance.Model.Height;

            ///
            CameraControllersManager cameraControllersManager = FindObjectOfType<CameraControllersManager>();
            if (cameraControllersManager != null)
            {
                cameraControllersManager.OnCameraUpdated += CameraControllersManager_OnCameraUpdated;
            }
        }

        void CameraControllersManager_OnCameraUpdated()
        {
            Cull();
            Pave();
        }

        void Cull()
        {
            while (activeSegments.Count > 0)
            {
                var segment = activeSegments[0];
                if (IsSegmentOutLeft(segment))
                {
                    heightModel.Cull(segment.StartIndex, segment.EndIndex - 1);
                    PushSegment(segment);
                    activeSegments.RemoveAt(0);
                }
                else
                {
                    break;
                }
            }

        }

        void Pave()
        {
            while (true)
            {
                if (ShouldSpawnNewSegment())
                {
                    SpawnNewSegment();
                }
                else
                {
                    break;
                }
            }
        }

        void SpawnNewSegment()
        {
            var segment = GetSegment();
            int startIndex = GetStartIndexForNewSegment();

            heightModel.UpdateByNodes(numberOfPointsInSegment);

            segment.SetPointsFromHeight(startIndex, numberOfPointsInSegment);
            segment.transform.position = GetPositionForNewSegment();
            segment.gameObject.SetActive(true);

            activeSegments.Add(segment);
        }

        Vector3 GetPositionForNewSegment()
        {
            if (activeSegments.Count == 0)
            {
                return initialPosition;
            }
            else
            {
                Vector3 position = initialPosition;
                var lastSegment = activeSegments[activeSegments.Count - 1];
                position.x = lastSegment.transform.position.x + lastSegment.Width;
                return position;
            }
        }

        int GetStartIndexForNewSegment()
        {
            if (activeSegments.Count == 0)
            {
                return 0;
            }
            else
            {
                return activeSegments[activeSegments.Count - 1].EndIndex;
            }
        }

        bool ShouldSpawnNewSegment()
        {
            return activeSegments.Count == 0 || !DoesSegmentFillRight(activeSegments[activeSegments.Count - 1]);
        }

        bool IsSegmentOutLeft(HillSegmentPoints segment)
        {
            return (segment.transform.position.x + segment.Width) < (followingTransform.position.x - viewLeftWidth);
        }

        bool DoesSegmentFillRight(HillSegmentPoints segment)
        {
            return (segment.transform.position.x + segment.Width) > (followingTransform.position.x + viewRightWidth);
        }

        HillSegmentPoints GetSegment()
        {
            if (inactiveSegments.Count == 0)
            {
                return Instantiate(hillSegmentPrototype);
            }
            else
            {
                var segment = inactiveSegments[inactiveSegments.Count - 1];
                inactiveSegments.RemoveAt(inactiveSegments.Count - 1);
                return segment;
            }
        }

        void PushSegment(HillSegmentPoints segment)
        {
            segment.gameObject.SetActive(false);
            inactiveSegments.Add(segment);
        }
    }

}