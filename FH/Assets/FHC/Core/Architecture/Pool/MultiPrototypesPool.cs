using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace FH.Core.Architecture.Pool
{
    public abstract class MultiPrototypesPool<T> : IMultiPrototypesPool<T>
    {
        Dictionary<int, T> prototypesDictionary = new Dictionary<int, T>();
        Dictionary<int, List<T>> instancesDictionary = new Dictionary<int, List<T>>();

        #region IMultiPrototypesPool<T>
        public bool ContainsPrototype(int prototypeId)
        {
            return prototypesDictionary.ContainsKey(prototypeId);
        }

        public void PushInstance<U>(U memberInstance) where U : IMultiPrototypesPoolMember<T>, T
        {
            int prototypeId = memberInstance.PrototypeId;
            Assert.IsTrue(instancesDictionary.ContainsKey(prototypeId));
            memberInstance.Pool = this;
            memberInstance.InPool = true;
            instancesDictionary[prototypeId].Add(memberInstance);
        }

        public void PushPrototype<U>(U memberPrototype) where U : IMultiPrototypesPoolMember<T>, T
        {
            int prototypeId = memberPrototype.PrototypeId;
            Assert.IsFalse(instancesDictionary.ContainsKey(prototypeId));
            prototypesDictionary.Add(prototypeId, memberPrototype);
            instancesDictionary.Add(prototypeId, new List<T>());
        }

        public T TakeInstance(int prototypeId, bool forceCloning)
        {
            Assert.IsTrue(instancesDictionary.ContainsKey(prototypeId), string.Format("prototypeId: {0}", prototypeId));
            if (instancesDictionary[prototypeId].Count > 0)
            {
                return TakeInstanceAvailableInDictionary(prototypeId);
            }
            else
            {
                if (forceCloning)
                {
                    return TakeInstanceByClonning(prototypeId);
                }
                else
                {
                    throw new Exception("Pool memebers are not available to take");
                }
            }
        }

        #endregion

        T TakeInstanceAvailableInDictionary(int prototypeId)
        {
            Assert.IsTrue(instancesDictionary[prototypeId].Count > 0);
            List<T> list = instancesDictionary[prototypeId];
            int lastIndex = list.Count - 1;
            T instance = list[lastIndex];
            (instance as IMultiPrototypesPoolMember<T>).InPool = false;
            list.RemoveAt(lastIndex);
            return instance;
        }

        T TakeInstanceByClonning(int prototypeId)
        {
            T instance = (prototypesDictionary[prototypeId] as ICloneable<T>).Clone();
            (instance as IMultiPrototypesPoolMember<T>).Pool = this;
            (instance as IMultiPrototypesPoolMember<T>).InPool = false;
            return instance;
        }
    }

}