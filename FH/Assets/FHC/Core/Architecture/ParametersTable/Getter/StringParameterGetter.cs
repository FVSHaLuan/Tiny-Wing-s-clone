using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Architecture.ParametersTable
{
    [System.Serializable]
    public class StringParameterGetter : ParameterGetter<string>
    {
        public override PrimitiveValueType PrimeValueType
        {
            get
            {
                return PrimitiveValueType.String;
            }
        }

        protected override string GetTypedValueFromParametersTable(IParametersTable parametersTable, string defaultValue)
        {
            return parametersTable.GetString(Key, defaultValue);
        }
    }

}