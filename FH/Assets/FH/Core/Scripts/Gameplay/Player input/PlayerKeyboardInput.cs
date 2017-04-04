using UnityEngine;
using System.Collections;

namespace FH.Gameplay.Controller
{
    public class PlayerKeyboardInput : PlayerInputDispatcher
    {
        [SerializeField]
        KeyCode keyCode = KeyCode.Space;

        public void Update()
        {
            inputStatus.Holding = Input.GetKey(keyCode);
        }
    }

}