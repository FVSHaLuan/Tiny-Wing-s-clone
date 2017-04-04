using UnityEngine;
using System.Collections;

namespace FH.Core.Gameplay.HelperComponent
{
    public class Rotator : OutsiteTargetTransform
    {
        [SerializeField]
        Vector3 angularSpeed = new Vector3();

        void Update()
        {
            Rotate(angularSpeed * Time.deltaTime);
        }
    }

}