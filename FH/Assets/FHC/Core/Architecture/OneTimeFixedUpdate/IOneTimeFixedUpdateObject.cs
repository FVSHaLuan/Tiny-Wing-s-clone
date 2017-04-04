using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture
{
    public interface IOneTimeFixedUpdateObject
    {
        bool Active { get; }
        void OneTimeFixedUpdate();
    }

}