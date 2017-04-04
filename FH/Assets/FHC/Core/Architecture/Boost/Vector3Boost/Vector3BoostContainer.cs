using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace FH.Core.Architecture
{
    public class Vector3BoostContainer: CumulativeBoostContainter<Vector3>
    {
        public override Vector3 BoostedValue
        {
            get
            {
                Vector3 value = Vector3.zero;
                for (int i = 0; i < ComponentsList.Count; i++)
                {
                    value += ComponentsList[i].BoostValue;
                }
                return value;
            }
        }
    }

}