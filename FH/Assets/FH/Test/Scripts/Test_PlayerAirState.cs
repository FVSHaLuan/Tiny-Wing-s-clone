using FH.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FH.Test
{
    public class Test_PlayerAirState : MonoBehaviour
    {
        [SerializeField]
        UnityEngine.UI.Text displayText;

        PlayerAirState playerAirState;

        public void Awake()
        {
            playerAirState = GameplayEntry.Instance.Player.AirState;
        }

        public void Update()
        {
            displayText.text = playerAirState.IsOnAir.ToString();
        }
    }

}