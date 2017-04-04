using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture.ParametersTable
{
    [System.Serializable]
    public abstract class ParameterGetter<T> : IParameterGetter<T>
    {
        [SerializeField]
        string key;
        [SerializeField]
        bool useKey = true;
        [SerializeField]
        T value;
        [SerializeField]
        T defaultValue;

        #region IParameterGetter
        public string Key
        {
            get
            {
                return key;
            }
        }

        public bool UseKey
        {
            get
            {
                return useKey;
            }
        }
        public T Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }
        public T DefaultValue
        {
            get
            {
                return defaultValue;
            }
        }

        public abstract PrimitiveValueType PrimeValueType { get; }

        public T GetValue(IParametersTable parametersTable)
        {
            return GetTypedValue(parametersTable);
        }
        #endregion

        public T UpdateValue(IParametersTable parametersTable)
        {
            Value = GetTypedValue(parametersTable);
            return Value;
        }

        protected T GetTypedValue(IParametersTable parametersTable)
        {
            if (useKey)
            {
                return GetTypedValueFromParametersTable(parametersTable, defaultValue);
            }
            else
            {
                return value;
            }
        }

        protected abstract T GetTypedValueFromParametersTable(IParametersTable parametersTable, T defaultValue);

        public static implicit operator T(ParameterGetter<T> parameterGetter)
        {
            return parameterGetter.value;
        }

    }

}