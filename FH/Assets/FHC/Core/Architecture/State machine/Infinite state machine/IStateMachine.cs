using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture.StateMachine
{
    public interface IStateMachine<T> where T : IStateController<T>
    {
        T currentStateController { get; }
        void SetStateController(T stateController);
    }

}