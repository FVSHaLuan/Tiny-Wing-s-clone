using UnityEngine;
using System.Collections;

namespace FH.Gameplay
{
    public interface IHeightSpeedYModifer
    {
        void Activate(IHeightModel heightModel);
        void GetNewSpeedY(IHeightModel heightModel);
    }

}