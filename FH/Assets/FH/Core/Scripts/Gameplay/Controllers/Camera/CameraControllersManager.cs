using UnityEngine;
using System.Collections;

namespace FH.Gameplay.Controller
{
    public class CameraControllersManager : MonoBehaviour
    {
        [SerializeField]
        CameraController currentController;

        public event System.Action OnCameraUpdated;

        public CameraController CurrentController
        {
            get
            {
                return currentController;
            }

            set
            {
                currentController.enabled = false;
                value.enabled = true;
                currentController = value;
            }
        }

        public void LateUpdate()
        {
            if (currentController != null)
            {
                currentController.UpdateCamera();

                ///
                if (OnCameraUpdated!=null)
                {
                    OnCameraUpdated();
                }
            }
        }

    }

}