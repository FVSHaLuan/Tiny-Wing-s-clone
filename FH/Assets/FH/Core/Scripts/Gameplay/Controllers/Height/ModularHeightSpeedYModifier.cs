using UnityEngine;
using System.Collections;
using System;

namespace FH.Gameplay.Controller
{
    public abstract class ModularHeightSpeedYModifier : HeightSpeedYModifer
    {
        [Header("ModularHeightSpeedYModifier")]
        [SerializeField]
        HeightSpeedYModifer nextHighSpeedYModifier;
        [SerializeField]
        bool dispatchChangeTheme = false;

        /// <summary>
        /// Is it time to switch to next HighSpeedYModifier?
        /// </summary>
        /// <returns></returns>

        public sealed override void Activate(IHeightModel heightModel)
        {
            OnActivate(heightModel);

            if (dispatchChangeTheme)
            {
                var gameplayEntry = GameplayEntry.Instance;
                if (gameplayEntry != null)
                {
                    GameplayEntry.Instance.Model.Theme.ChangeTheme(heightModel.CurrentX);
                }
            }
        }

        protected virtual void OnActivate(IHeightModel heightModel) { }

        protected abstract bool ShouldSwitch(IHeightModel heightModel);

        public sealed override void GetNewSpeedY(IHeightModel heightModel)
        {
            if (ShouldSwitch(heightModel))
            {
                heightModel.SpeedYModifier = nextHighSpeedYModifier;
            }
            else
            {
                OnGetNewSpeedY(heightModel);
            }
        }

        public abstract void OnGetNewSpeedY(IHeightModel heightModel);
    }

}