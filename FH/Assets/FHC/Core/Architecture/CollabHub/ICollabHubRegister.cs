using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture
{
    public interface ICollabHubRegister
    {
        /// <summary>
        /// ICollabMember can only register themselves to this hub when IsOpening = true
        /// ICollabHub can only be finished when IsOpening = false
        /// </summary>
        bool IsOpening { get; }

        event System.Action Finish;
        void Register(ICollabMember member);
    }

}