using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture
{
    public interface IActivatable
    {
        bool IsDestroyed { get; }
        bool ActiveSelf { get; }
        bool ActiveInHierarchy { get; }
        void Initialize();
        void SetActiveSelf(bool activeSelf);
        void Destroy();
        void DestroyImmediately();
    }
}
