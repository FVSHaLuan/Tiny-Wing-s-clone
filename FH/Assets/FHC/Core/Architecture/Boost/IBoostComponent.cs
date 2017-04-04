using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture
{
    public interface IBoostComponent<T>
    {
        T BoostValue { get; }
    }

}