using UnityEngine;
using System.Collections;

namespace FH.Gameplay.Controller
{
    public abstract class PlayerInputDispatcher : MonoBehaviour
    {
        protected InputStatus inputStatus { get; private set; }

        public void Awake()
        {
            inputStatus = GameplayEntry.Instance.Model.InputStatus;
        }
    }

}