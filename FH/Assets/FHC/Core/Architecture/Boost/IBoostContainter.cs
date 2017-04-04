using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture
{
    public interface IBoostContainter<T>
    {
        T BoostedValue { get; }

        void AddBoostComponent(IBoostComponent<T> boostComponent);
        void RemoveBoostComponent(IBoostComponent<T> boostComponent);
        void ClearBoostComponents();
    }

}