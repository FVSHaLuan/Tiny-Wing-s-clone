using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture.ParametersTable
{
    public interface IParameterGetter<T>
    {
        string Key { get; }
        bool UseKey { get; }
        T DefaultValue { get; }
        T Value { get; set; }
        T GetValue(IParametersTable parametersTable);
        PrimitiveValueType PrimeValueType { get; }
    }

}