using UnityEngine;
using System.Collections;
using System;

namespace FH.Gameplay.Controller
{
    public abstract class HeightSpeedYModifer : MonoBehaviour, IHeightSpeedYModifer
    {
        public virtual void Activate(IHeightModel heightModel) { }

        public abstract void GetNewSpeedY(IHeightModel heightModel);

    }
}
