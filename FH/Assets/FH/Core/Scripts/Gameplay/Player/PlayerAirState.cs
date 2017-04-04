using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FH.Gameplay
{
    public class PlayerAirState : MonoBehaviour, ISerializationCallbackReceiver
    {
        public event Action OnBeginAir;
        public event Action OnEndAir;

        [SerializeField]
        bool initialState = true;
        [SerializeField]
        float takingOffTimeThreshold = 0.1f;

        float takeOffTimeTracking = 0;
        bool takingOff = false;

        public bool IsOnAir { get; private set; }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == GameObjectTags.Hill)
            {
                IsOnAir = false;
                takingOff = false;

                if (OnEndAir != null)
                {
                    OnEndAir();
                }
            }
        }

        public void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == GameObjectTags.Hill)
            {
                takingOff = true;
                takeOffTimeTracking = 0;
            }
        }

        public void Update()
        {
            if (takingOff)
            {
                takeOffTimeTracking += Time.deltaTime;
                if (takeOffTimeTracking >= takingOffTimeThreshold)
                {
                    takingOff = false;
                    IsOnAir = true;

                    if (OnBeginAir != null)
                    {
                        OnBeginAir();
                    }
                }
            }
        }

        #region ISerializationCallbackReceiver
        public void OnBeforeSerialize()
        {

        }

        public void OnAfterDeserialize()
        {
            IsOnAir = initialState;
        }
        #endregion
    }

}