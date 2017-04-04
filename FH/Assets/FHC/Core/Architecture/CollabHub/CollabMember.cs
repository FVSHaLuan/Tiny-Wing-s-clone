using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Architecture
{
    public abstract class CollabMember : MonoBehaviour, ICollabMember
    {
        [SerializeField]
        protected CollabHub collabHub;
        [SerializeField]
        bool registerAtWake = false;

        #region ICollabMember
        public abstract bool IsFinished { get; }

        public virtual void OnStartWorking()
        {
            
        }
        #endregion

        public virtual void Awake()
        {
            ICollabHubRegister collabHubRegister = collabHub as ICollabHubRegister;
            if (registerAtWake && collabHubRegister != null && collabHubRegister.IsOpening)
            {
                (collabHub as ICollabHub).Register(this);
            }
        }
    }

}