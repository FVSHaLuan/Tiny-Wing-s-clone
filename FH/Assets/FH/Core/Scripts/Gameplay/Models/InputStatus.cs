using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FH.Gameplay
{
    public class InputStatus : MonoBehaviour
    {
        public event System.Action OnStartHolding;
        public event System.Action OnEndHolding;

        bool holding = false;

        float heldTime = 0;

        public bool Holding
        {
            get
            {
                return holding;
            }

            set
            {
                if (holding == false && value == true)
                {
                    HeldTime = 0;
                    holding = value;
                    if (OnStartHolding != null)
                    {
                        OnStartHolding();
                    }
                }
                else if (holding == true && value == false)
                {
                    holding = value;
                    if (OnEndHolding != null)
                    {
                        OnEndHolding();
                    }
                }

            }
        }

        public float HeldTime
        {
            get
            {
                return heldTime;
            }

            private set
            {
                heldTime = value;
            }
        }

        void Update()
        {
            if (Holding)
            {
                HeldTime += Time.deltaTime;
            }
        }
    }

}