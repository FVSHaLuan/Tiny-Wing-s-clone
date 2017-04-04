using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Architecture
{
    public abstract class OneTimeFixedUpdateObject : MonoBehaviour, IOneTimeFixedUpdateObject
    {
        bool IOneTimeFixedUpdateObject.Active
        {
            get
            {
                if (this == null)
                {
                    return false;
                }
                else
                {
                    return isActiveAndEnabled;
                }
            }
        }

        public abstract void OneTimeFixedUpdate();

        protected virtual void Awake()
        {
            OneTimeFixedUpdateService.Instance.AddFixedUpdateObject(this);
        }
    }

}