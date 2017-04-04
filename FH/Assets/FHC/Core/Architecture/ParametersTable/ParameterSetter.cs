using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture.ParametersTable
{
    [System.Serializable]
    public class ParameterSetter
    {
        [SerializeField]
        string key;

        [SerializeField]
        int intValue;
        [SerializeField]
        float floatValue;
        [SerializeField]
        bool boolValue;
        [SerializeField]
        string stringValue;

        [SerializeField]
        PrimitiveValueType primeValueType;

        [SerializeField]
        private int minIntValue;
        [SerializeField]
        private int maxIntValue;
        [SerializeField]
        private float minFloatValue;
        [SerializeField]
        private float maxFloatValue;

        #region Public properties
        public string Key
        {
            get
            {
                return key;
            }

            set
            {
                key = value;
            }
        }

        public int IntValue
        {
            get
            {
                return intValue;
            }

            set
            {
                intValue = value;
            }
        }

        public float FloatValue
        {
            get
            {
                return floatValue;
            }

            set
            {
                floatValue = value;
            }
        }

        public bool BoolValue
        {
            get
            {
                return boolValue;
            }

            set
            {
                boolValue = value;
            }
        }

        public string StringValue
        {
            get
            {
                return stringValue;
            }

            set
            {
                stringValue = value;
            }
        }

        public PrimitiveValueType PrimitiveValueType
        {
            get
            {
                return primeValueType;
            }

            set
            {
                primeValueType = value;
            }
        }

        public int MinIntValue
        {
            get
            {
                return minIntValue;
            }

            set
            {
                minIntValue = value;
            }
        }

        public int MaxIntValue
        {
            get
            {
                return maxIntValue;
            }

            set
            {
                maxIntValue = value;
            }
        }

        public float MinFloatValue
        {
            get
            {
                return minFloatValue;
            }

            set
            {
                minFloatValue = value;
            }
        }

        public float MaxFloatValue
        {
            get
            {
                return maxFloatValue;
            }

            set
            {
                maxFloatValue = value;
            }
        }
        #endregion
    }

}