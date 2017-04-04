using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Architecture
{
    public class Vector3BoostComponent : IBoostComponent<Vector3>
    {
        Vector3 boostValue;

        public Vector3 BoostValue
        {
            get
            {
                return boostValue;
            }
            set
            {
                boostValue = value;
            }
        }

        public Vector3BoostComponent()
        {

        }

        public Vector3BoostComponent(Vector3 boostValue)
        {
            this.boostValue = boostValue;
        }
    }

}