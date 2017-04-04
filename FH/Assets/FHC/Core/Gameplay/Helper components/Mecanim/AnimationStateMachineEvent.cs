using UnityEngine;
using System.Collections;
using FH.Core.Architecture;
using UnityEngine.Experimental.Director;

namespace FH.Core.Gameplay.HelperComponent
{
    public class AnimationStateMachineEvent : StateMachineBehaviour
    {
        [SerializeField]
        OrderedEventDispatcher onStateEntered = new OrderedEventDispatcher();
        [SerializeField]
        OrderedEventDispatcher onStateExited = new OrderedEventDispatcher();

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
        {
            onStateEntered.Dispatch();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            onStateExited.Dispatch();
        }        
    }

}