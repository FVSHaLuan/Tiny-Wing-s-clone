using UnityEngine;
using System.Collections;
using System;

namespace FH.Gameplay.Controller
{
    public class MainCameraController : CameraController
    {
        [SerializeField]
        float cameraStableY = 0;
        [SerializeField]
        float cameraStableSize = 5;

        [Space]
        [SerializeField]
        float playerStableRangeMax = 3;
        [SerializeField]
        float playerStableRangeMin = -3;       
        [SerializeField]
        float playerMaxHeight = 10;
        [SerializeField]
        float playerMinDistanceToCameraTop = 1;
        [SerializeField]
        float playerDistanceToCameraLeft = 2;

        float cameraMaxSize;
        float playerUnstalbeRange;
        float cameraMaxAdditionalSize;

        Transform playerTransform;
        Transform cameraTransform;
        new Camera camera;

        public void Awake()
        {
            cameraMaxSize = ((playerMaxHeight + playerMinDistanceToCameraTop) - (cameraStableY - cameraStableSize)) / 2.0f;
            cameraMaxAdditionalSize = cameraMaxSize - cameraStableSize;
            playerUnstalbeRange = playerMaxHeight - playerStableRangeMax;

            playerTransform = GameplayEntry.Instance.Player.transform;
            cameraTransform = Camera.main.transform;
            camera = Camera.main;

        }

        public override void UpdateCamera()
        {
            var playerPosition = playerTransform.position;

            if (playerPosition.y > playerStableRangeMax)
            {
                UpdaterPlayerAboveStable(playerPosition);
            }
            else if (playerPosition.y < playerStableRangeMin)
            {
                UpdaterPlayerUnderStable(playerPosition);
            }
            else
            {
                UpdaterPlayerInStable(playerPosition);
            }
        }

        void UpdaterPlayerAboveStable(Vector3 playerPosition)
        {
            float playerUnstableHeight = playerPosition.y - playerStableRangeMax;

            float cameraAditionalSize = Mathf.Lerp(0, cameraMaxAdditionalSize, playerUnstableHeight / playerUnstalbeRange);

            camera.orthographicSize = cameraStableSize + cameraAditionalSize;

            ///
            var p = cameraTransform.position;
            p.y = cameraStableY + cameraAditionalSize;
            float cameraHaftWidth = camera.aspect * camera.orthographicSize;
            p.x = playerPosition.x + cameraHaftWidth - playerDistanceToCameraLeft;
            cameraTransform.position = p;
        }

        void UpdaterPlayerUnderStable(Vector3 playerPosition)
        {
            ///
            camera.orthographicSize = cameraStableSize;

            ///
            var p = cameraTransform.position;
            p.y = cameraStableY + (playerPosition.y - playerStableRangeMin);
            float cameraHaftWidth = camera.aspect * camera.orthographicSize;
            p.x = playerPosition.x + cameraHaftWidth - playerDistanceToCameraLeft;
            cameraTransform.position = p;
        }

        void UpdaterPlayerInStable(Vector3 playerPosition)
        {
            ///
            camera.orthographicSize = cameraStableSize;

            ///
            var p = cameraTransform.position;
            p.y = cameraStableY;
            float cameraHaftWidth = camera.aspect * camera.orthographicSize;
            p.x = playerPosition.x + cameraHaftWidth - playerDistanceToCameraLeft;
            cameraTransform.position = p;
        }
    }
}