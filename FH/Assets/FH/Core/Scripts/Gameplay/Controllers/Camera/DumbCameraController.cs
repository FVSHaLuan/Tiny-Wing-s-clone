using UnityEngine;
using System.Collections;

namespace FH.Gameplay.Controller
{
    public class DumbCameraController : CameraController
    {
        [SerializeField]
        Vector2 cameraOffset = new Vector3(2, 2);

        Transform playerTransform;
        Transform mainCameraTransform;

        public void Awake()
        {
            mainCameraTransform = Camera.main.transform;
            playerTransform = GameplayEntry.Instance.Player.transform;
        }

        public override void UpdateCamera()
        {
            var p = playerTransform.position + (Vector3)cameraOffset;
            p.z = mainCameraTransform.position.z;
            mainCameraTransform.position = p;
        }
    }

}