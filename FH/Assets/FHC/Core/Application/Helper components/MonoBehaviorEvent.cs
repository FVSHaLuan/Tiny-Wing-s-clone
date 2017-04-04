using UnityEngine;
using FH.Core.Architecture;

namespace FH.Core.HelperComponent
{
    public class MonoBehaviorEvent : MonoBehaviour
    {

        [Header("Activation")]
        [SerializeField]
        OrderedEventDispatcher onAwake = new OrderedEventDispatcher();
        [SerializeField]
        OrderedEventDispatcher onEnable = new OrderedEventDispatcher();
        [SerializeField]
        OrderedEventDispatcher onStart = new OrderedEventDispatcher();
        [SerializeField]
        OrderedEventDispatcher onDisable = new OrderedEventDispatcher();

        [Header("Others")]
        [SerializeField]
        OrderedEventDispatcher onSceneLoaded = new OrderedEventDispatcher();

        void Awake()
        {
            onAwake.Dispatch();
        }

        void OnEnable()
        {
            onEnable.Dispatch();
        }

        void Start()
        {
            onStart.Dispatch();
        }

        void OnDisable()
        {
            onDisable.Dispatch();
        }

        public void OnLevelWasLoaded(int level)
        {
            onSceneLoaded.Dispatch();
        }
    }

}