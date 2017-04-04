using UnityEngine;
using System.Collections;

namespace FH.Gameplay
{
    public class PlayerEntry : MonoBehaviour
    {
        [SerializeField]
        PlayerCurrentNearestNode currentNearestNode;
        [SerializeField]
        PlayerAirState airState;

        public PlayerCurrentNearestNode CurrentNearestNode
        {
            get
            {
                return currentNearestNode;
            }
        }

        public PlayerAirState AirState
        {
            get
            {
                return airState;
            }
        }
    }

}