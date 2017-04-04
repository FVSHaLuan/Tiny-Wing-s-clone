using UnityEngine;
using System.Collections;

namespace FH.Gameplay.Controller
{
    public class PlayerMouseInput : PlayerInputDispatcher
    {
        void Update()
        {
            inputStatus.Holding = Input.GetMouseButton(0);
        }
    }
}
