using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture
{
    public interface ITimelineAnimationObject
    {
        float CurrentPosition { get; set; }
    }
}
