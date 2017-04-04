using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace FH.Core.Architecture
{
    [Serializable]
    public abstract class CumulativeBoostContainter<T> : IBoostContainter<T>
    {
        [SerializeField]
        List<IBoostComponent<T>> componentsList = new List<IBoostComponent<T>>();

        public abstract T BoostedValue
        {
            get;
        }

        protected List<IBoostComponent<T>> ComponentsList
        {
            get
            {
                return componentsList;
            }
        }

        public void AddBoostComponent(IBoostComponent<T> boostComponent)
        {
            Assert.IsFalse(ComponentsList.Contains(boostComponent));

            ComponentsList.Add(boostComponent);
        }

        public void RemoveBoostComponent(IBoostComponent<T> boostComponent)
        {
            ComponentsList.Remove(boostComponent);
        }

        public void ClearBoostComponents()
        {
            ComponentsList.Clear();
        }
    }

}