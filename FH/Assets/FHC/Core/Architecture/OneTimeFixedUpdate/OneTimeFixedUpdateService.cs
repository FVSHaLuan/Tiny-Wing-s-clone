using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace FH.Core.Architecture
{
    public class OneTimeFixedUpdateService : MonoBehaviourSingleton<IOneTimeFixedUpdateService>, IOneTimeFixedUpdateService
    {
        int lastFrameCount = 0;
        List<IOneTimeFixedUpdateObject> fixedUpdateObjects = new List<IOneTimeFixedUpdateObject>();

        public void FixedUpdate()
        {
            if (Time.frameCount != lastFrameCount)
            {
                for (int i = fixedUpdateObjects.Count - 1; i >= 0; i--)
                {
                    var fixedUpdateObject = fixedUpdateObjects[i];
                    if (fixedUpdateObject == null)
                    {
                        fixedUpdateObjects.RemoveAt(i);
                    }
                    else
                    {
                        if (fixedUpdateObject.Active)
                        {
                            fixedUpdateObject.OneTimeFixedUpdate();
                        }
                    }
                }

                lastFrameCount = Time.frameCount;
            }
        }

        #region IOneTimeFixedUpdateService
        void IOneTimeFixedUpdateService.AddFixedUpdateObject(IOneTimeFixedUpdateObject fixedUpdateObject)
        {
            fixedUpdateObjects.Add(fixedUpdateObject);
        }

        void IOneTimeFixedUpdateService.RemoveFixedUpdateObject(IOneTimeFixedUpdateObject fixedUpdateObject)
        {
            fixedUpdateObjects.Remove(fixedUpdateObject);
        }
        #endregion

        #region MonoBehaviourSingleton<IOneTimeFixedUpdateService>
        protected override IOneTimeFixedUpdateService GetInstance()
        {
            return this;
        }
        #endregion
    }

}