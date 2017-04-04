using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Architecture.ParametersTable
{
    [System.Serializable]
    public class FloatParameterGetter : ParameterGetter<float>
    {
        public override PrimitiveValueType PrimeValueType
        {
            get
            {
                return PrimitiveValueType.Float;
            }
        }

        protected override float GetTypedValueFromParametersTable(IParametersTable parametersTable, float defaultValue)
        {
            return parametersTable.GetFloat(Key, defaultValue);
        }
                
    }

}