using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture
{
    public interface ICollabMember
    {
        void OnStartWorking();
        bool IsFinished { get; }
    }

}