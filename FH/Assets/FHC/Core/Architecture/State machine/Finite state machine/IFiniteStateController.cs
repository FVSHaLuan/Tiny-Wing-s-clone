﻿using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Architecture.StateMachine
{
    public interface IFiniteStateController<T, U> where T : IFiniteStateController<T, U> where U : IConvertible
    {
        IFiniteStateMachine<T, U> StateMachine { get; set; }
        void Initialize();
        void Activate(U previousState);
        void Deactivate(U nextState);
        void StateUpdate();
    }

}