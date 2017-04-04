using UnityEngine;
using FH.Core.Architecture;

namespace FH.Core.Gameplay.HelperComponent
{
    public class RandomTimeTriggerer : MonoBehaviour
    {
        [SerializeField]
        float minTimeInterval;
        [SerializeField]
        float maxTimeInterval;

        [SerializeField]
        OrderedEventDispatcher onTrigger = new OrderedEventDispatcher();

        float timeTracker;
        float currentInterval;

        public void Awake()
        {
            StartNewCounter();
        }

        void StartNewCounter()
        {
            timeTracker = 0;
            currentInterval = GetRandomInterval();
        }

        public void Update()
        {
            timeTracker += Time.deltaTime;
            if (timeTracker >= currentInterval)
            {
                StartNewCounter();
                onTrigger.Dispatch();
            }
        }

        float GetRandomInterval()
        {
            return Random.Range(minTimeInterval, maxTimeInterval);
        }
    }

}