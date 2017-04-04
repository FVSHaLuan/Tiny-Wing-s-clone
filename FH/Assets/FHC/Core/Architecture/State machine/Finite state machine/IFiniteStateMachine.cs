using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Architecture.StateMachine
{
    public interface IFiniteStateMachine<T, U> where T : IFiniteStateController<T, U> where U : IConvertible
    {
        U CurrentState { get; }
        void SetState(U state);
    }
}