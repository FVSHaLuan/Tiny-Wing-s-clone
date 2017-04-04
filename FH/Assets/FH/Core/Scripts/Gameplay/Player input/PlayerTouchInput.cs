using UnityEngine;
using System.Collections;

namespace FH.Gameplay.Controller
{
    public class PlayerTouchInput : PlayerInputDispatcher
    {
#if !UNITY_EDITOR
        void Update()
        {
            inputStatus.Holding = Input.touchCount > 0;
        } 
#endif
    }

}