using UnityEngine;
using System.Collections;
using FH.Core.Architecture;
using UnityEngine.Serialization;

namespace FH.Core.Gameplay.HelperComponent
{
    public class TimeTrigger : MonoBehaviour
    {
        [SerializeField]
        float countDownTime = 2;
        [SerializeField, FormerlySerializedAs("timeOutEvenent")]
        OrderedEventDispatcher timeOutEvent = new OrderedEventDispatcher();

        float currentTime;

        public float CountDownTime
        {
            get
            {
                return countDownTime;
            }

            set
            {
                countDownTime = value;
            }
        }

        bool counting = false;

        void Awake()
        {
            if (!counting)
            {
                enabled = false;
            }
        }

        void Update()
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                timeOutEvent.Dispatch();
                enabled = false;
            }
        }

        public void StartCountDown()
        {
            enabled = true;
            currentTime = CountDownTime;
            counting = true;
        }

        public void OnDisable()
        {
            counting = false;
        }
    }

}