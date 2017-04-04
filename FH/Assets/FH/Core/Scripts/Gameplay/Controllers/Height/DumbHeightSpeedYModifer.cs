using UnityEngine;

namespace FH.Gameplay.Controller
{
    public class DumbHeightSpeedYModifer : ModularHeightSpeedYModifier
    {
        [Header("DumbHeightSpeedYModifer")]
        [SerializeField]
        float maxY = 3;
        [SerializeField]
        float minY = -3;
        [SerializeField]
        float maxSpeedYMagnitude = 20;
        [SerializeField]
        float gravityMagnitude = 5;
        [SerializeField]
        float criticalGravityMagnitude = 10;
        [SerializeField]
        float minInterval = 1;
        [SerializeField]
        float maxInterval = 2;
        [SerializeField]
        float changeGravityThreshold = 0.1f;

        [Header("Switching condition")]
        [SerializeField]
        float minLength = 20.0f;
        [SerializeField]
        float minHeightWhenSwitch = 4.0f;

        float timeTracking = 0;
        float nextTimeInterval = 1;

        float startX = 0;
        float lastSpeedY;

        protected override void OnActivate(IHeightModel heightModel)
        {
            startX = heightModel.CurrentX;
            lastSpeedY = heightModel.CurrentSpeedY;

            timeTracking = 0;
            nextTimeInterval = Random.Range(minInterval, maxInterval);

            InitializeGravity(heightModel);
        }

        public override void OnGetNewSpeedY(IHeightModel heightModel)
        {
            ///
            if (heightModel.CurrentY <= minY)
            {
                // Must go up
                if (heightModel.CurrentSpeedY < 0)
                {
                    GoUp(heightModel);
                }
            }
            else if (heightModel.CurrentY >= maxY)
            {
                // Must go down
                if (heightModel.CurrentSpeedY > 0)
                {
                    GoDown(heightModel);
                }
            }
            else
            {
                if (Mathf.Abs(heightModel.CurrentSpeedY) > maxSpeedYMagnitude)
                {
                    ClampSpeedY(heightModel);
                }
                else
                {
                    ChangeGravitySign(heightModel);
                }
            }

            ///
            timeTracking += heightModel.DeltaTime;
        }

        void GoUp(IHeightModel heightModel)
        {
            //timeTracking = 0;
            heightModel.Gravity = criticalGravityMagnitude;
        }

        void GoDown(IHeightModel heightModel)
        {
            //timeTracking = 0;
            heightModel.Gravity = -criticalGravityMagnitude;
        }

        void ChangeGravitySign(IHeightModel heightModel)
        {
            // Go randomly
            if (timeTracking >= nextTimeInterval)
            {
                nextTimeInterval = Random.Range(minInterval, maxInterval);
                timeTracking = 0;
                heightModel.Gravity = -Mathf.Sign(heightModel.Gravity) * gravityMagnitude;
            }
        }

        void InitializeGravity(IHeightModel heightModel)
        {
            heightModel.Gravity = Mathf.Sign(Random.Range(-1, 1)) * gravityMagnitude;
            if (heightModel.Gravity == 0)
            {
                heightModel.Gravity = gravityMagnitude;
            }
        }

        void ClampSpeedY(IHeightModel heightModel)
        {
            //timeTracking = 0;
            heightModel.Gravity = -Mathf.Sign(heightModel.CurrentSpeedY) * gravityMagnitude;
        }

        protected override bool ShouldSwitch(IHeightModel heightModel)
        {
            if ((heightModel.CurrentX - startX) < minLength)
            {
                lastSpeedY = heightModel.CurrentSpeedY;
                return false;
            }

            ///
            if (heightModel.CurrentY < minHeightWhenSwitch)
            {
                lastSpeedY = heightModel.CurrentSpeedY;
                return false;
            }

            ///
            if (Mathf.Approximately(heightModel.CurrentSpeedY, 0.0f))
            {
                return true;
            }

            ///
            if (heightModel.CurrentSpeedY * lastSpeedY < 0)
            {
                return true;
            }

            ///
            lastSpeedY = heightModel.CurrentSpeedY;
            return false;
        }
    }

}