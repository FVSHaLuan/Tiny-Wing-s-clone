using UnityEngine;
using System.Collections;

namespace FH.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerRotationController : MonoBehaviour
    {
        [SerializeField, Range(5, 200)]
        int cachesCountMax = 5;
        [SerializeField]
        float minTanAngle = -45;
        [SerializeField]
        float maxTanAngle = 45;

        new Rigidbody2D rigidbody2D;

        Vector2[] velocitiesCache;
        float minTan;
        float maxTan;

        Vector2 velocitiesCacheSum = Vector2.zero;
        int currentCacheIndex = 0;
        int cachesCount = 0;

        void Awake()
        {
            velocitiesCache = new Vector2[cachesCountMax];
            ResetCache();

            rigidbody2D = GetComponent<Rigidbody2D>();

            minTan = Mathf.Tan(minTanAngle * Mathf.Deg2Rad);
            maxTan = Mathf.Tan(maxTanAngle * Mathf.Deg2Rad);
        }

        void Update()
        {
            UpdateCache();
            UpdateRotation();
        }

        private void UpdateRotation()
        {
            Vector2 averageVelocity = velocitiesCacheSum / cachesCount;
            float tan = Mathf.Clamp(averageVelocity.y / averageVelocity.x, minTan, maxTan);
            float angleDegree = Mathf.Atan(tan) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angleDegree);
        }

        void UpdateCache()
        {
            cachesCount++;

            velocitiesCacheSum += rigidbody2D.velocity;
            if (cachesCount > cachesCountMax)
            {
                cachesCount--;
                velocitiesCacheSum -= velocitiesCache[currentCacheIndex];
            }

            velocitiesCache[currentCacheIndex] = rigidbody2D.velocity;
                       
            currentCacheIndex++;
            if (currentCacheIndex == cachesCountMax)
            {
                currentCacheIndex = 0;
            }
        }

        void ResetCache()
        {
            for (int i = 0; i < cachesCountMax; i++)
            {
                velocitiesCache[i] = Vector2.zero;
            }
        }
    }

}