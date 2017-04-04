using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Architecture.ParametersTable
{
    [System.Serializable]
    public class BoolParameterGetter : ParameterGetter<bool>
    {
        public override PrimitiveValueType PrimeValueType
        {
            get
            {
                return PrimitiveValueType.Bool;
            }
        }

        protected override bool GetTypedValueFromParametersTable(IParametersTable parametersTable, bool defaultValue)
        {
            return parametersTable.GetBool(Key, defaultValue);
        }
    }

}