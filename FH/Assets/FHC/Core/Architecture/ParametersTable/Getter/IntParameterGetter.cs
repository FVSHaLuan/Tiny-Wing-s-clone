using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Architecture.ParametersTable
{
    [System.Serializable]
    public class IntParameterGetter : ParameterGetter<int>
    {
        public override PrimitiveValueType PrimeValueType
        {
            get
            {
                return PrimitiveValueType.Integer;
            }
        }

        protected override int GetTypedValueFromParametersTable(IParametersTable parametersTable, int defaultValue)
        {
            return parametersTable.GetInt(Key, defaultValue);
        }
    }

}