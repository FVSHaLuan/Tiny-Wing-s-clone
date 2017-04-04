using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Assertions;

namespace FH.Core.Architecture
{
    public class SimpleRenderer : MonoBehaviour, ISimpleRenderer
    {
        [Header("SimpleRenderer")]

        bool selfVisible = true;
        bool initialized = false;

        protected bool Initialized
        {
            get
            {
                return initialized;
            }
        }

        #region ISimpleRenderer

        public bool SelfVisible
        {
            get
            {
                return selfVisible;
            }

            set
            {
                selfVisible = value;
                gameObject.SetActive(selfVisible);
            }
        }

        public bool Visible
        {
            get
            {
                return gameObject.activeInHierarchy;
            }
        }

        public bool IsInitialized
        {
            get
            {
                return initialized;
            }
        }

        public void Initialize()
        {
            if (!initialized)
            {
                OnInitialize();
                initialized = true;
            }            
        }
        #endregion

        protected virtual void OnInitialize() { }

        #region TEST
#if UNITY_EDITOR

        [ContextMenu("Test_SwitchVisibility")]
        protected void Test_SwitchVisibility()
        {
            SelfVisible = !SelfVisible;
        }
#endif
        #endregion
    }

}