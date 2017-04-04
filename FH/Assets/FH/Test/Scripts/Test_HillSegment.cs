using UnityEngine;
using System.Collections;
using FH.Gameplay;

namespace FH.Test
{
    public class Test_HillSegment : MonoBehaviour
    {
        [SerializeField]
        int numberOfHillPoints = 100;

        [ContextMenu("Set")]
        void Set()
        {
            GetComponent<HillSegmentPoints>().SetPointsFromHeight(0, numberOfHillPoints);
        }
    }

}