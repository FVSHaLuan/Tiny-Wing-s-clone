using UnityEngine;
using System.Collections;
using System;

namespace FH.Gameplay.Controller
{
    public class ConstantMoveHeightSpeedYModifier : ModularHeightSpeedYModifier
    {
        [Header("ConstantMoveHeightSpeedYModifier")]
        [SerializeField]
        float speedY = 0;
        [SerializeField]
        float gravity = 0;


        [Header("Switching condition")]
        [SerializeField]
        float minLength = 50.0f;

        float startX;

        protected override void OnActivate(IHeightModel heightModel)
        {
            heightModel.Gravity = 0;
            heightModel.CurrentSpeedY = 0;
            startX = heightModel.CurrentX;
        }

        public override void OnGetNewSpeedY(IHeightModel heightModel)
        {

        }

        protected override bool ShouldSwitch(IHeightModel heightModel)
        {
            return (heightModel.CurrentX - startX) >= minLength;
        }
    }

}