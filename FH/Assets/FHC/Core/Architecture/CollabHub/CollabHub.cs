using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace FH.Core.Architecture
{
    public class CollabHub : ClassEncapsulator<ICollabHub>, ICollabHub
    {
        [Header("Status")]
        [SerializeField, ReadOnly]
        bool isOpening = false;
        [SerializeField, ReadOnly]
        bool isFinished = false;

        [Header("Event")]
        [SerializeField]
        OrderedEventDispatcher onFinished = new OrderedEventDispatcher();

        List<ICollabMember> collabMembers;

        bool working = false;

        event Action finish;

        #region ICollabHubRegister
        event Action ICollabHubRegister.Finish
        {
            add
            {
                finish += value;
            }

            remove
            {
                finish -= value;
            }
        }

        void ICollabHubRegister.Register(ICollabMember member)
        {
            Assert.IsTrue(isOpening, "Registered ICollabMember to a closed hub");
            Assert.IsFalse(collabMembers.Contains(member), "Registered an already-added ICollabMember to a hub");

            collabMembers.Add(member);
        }

        public bool IsOpening
        {
            get
            {
                return isOpening;
            }
        }
        #endregion

        #region ClassEncapsulator<ICollabHub>
        protected override ICollabHub GetEncapsulatedClass()
        {
            return this;
        }
        #endregion

        #region ICollabHub
        void ICollabHub.CloseRegistration()
        {
            isOpening = false;
        }

        void ICollabHub.OpenRegistration()
        {
            isOpening = true;
            isFinished = false;
            collabMembers = new List<ICollabMember>();
        }

        public void StartWorking()
        {
            Assert.IsFalse(IsOpening);
            working = true;
            enabled = true;
            DispatchStartWorkingEvent();
        }

        bool ICollabHub.IsFinished
        {
            get
            {
                return isFinished;
            }
        }
        #endregion

        void DispatchStartWorkingEvent()
        {
            for (int i = 0; i < collabMembers.Count; i++)
            {
                collabMembers[i].OnStartWorking();
            }
        }

        void FinishWorking()
        {
            working = false;
            isFinished = true;
            enabled = false;
            if (finish != null)
            {
                finish();
            }
            onFinished.Dispatch();
        }

        public void OnEnable()
        {
            if (!working)
            {
                enabled = false;
            }
        }

        void Update()
        {
            if (IsAllDone())
            {
                FinishWorking();
            }
        }

        bool IsAllDone()
        {
            for (int i = 0; i < collabMembers.Count; i++)
            {
                if (!collabMembers[i].IsFinished)
                {
                    return false;
                }
            }

            return true;
        }
    }

}