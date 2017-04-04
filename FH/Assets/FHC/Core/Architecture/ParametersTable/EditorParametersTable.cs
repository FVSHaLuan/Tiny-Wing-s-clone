using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using FH.Core.Architecture.Helper;

namespace FH.Core.Architecture.ParametersTable
{
    /// <summary>
    /// This should be only used in Editor due to performance concern
    /// </summary>
    public class EditorParametersTable : MonoBehaviour, IParametersTableListableKeys, IParametersTableSetable
    {
        #region Serializable fields
        [SerializeField]
        bool dispatchEventOnChange = true;
        [SerializeField]
        bool dispatchEventToChildren = true;
        [SerializeField]
        bool dispatchEventToInActiveChildren = true;

        [SerializeField]
        List<ParameterSetter> parameterSetters = new List<ParameterSetter>();
        #endregion

        #region Public properties
        public List<ParameterSetter> ParameterSetters
        {
            get
            {
                return parameterSetters;
            }

            set
            {
                parameterSetters = value;
            }
        }
        #endregion

        #region IParametersTable
        public bool GetBool(string key, bool defaultValue)
        {
            ParameterSetter parameter = FindParameter(key, PrimitiveValueType.Bool);
            if (parameter == null)
            {
                return defaultValue;
            }
            return parameter.BoolValue;
        }

        public float GetFloat(string key, float defaultValue)
        {
            ParameterSetter parameter = FindParameter(key, PrimitiveValueType.Float);
            if (parameter == null)
            {
                return defaultValue;
            }
            return parameter.FloatValue;
        }

        public int GetInt(string key, int defaultValue)
        {
            ParameterSetter parameter = FindParameter(key, PrimitiveValueType.Integer);
            if (parameter == null)
            {
                return defaultValue;
            }
            return parameter.IntValue;
        }

        public string GetString(string key, string defaultValue)
        {
            ParameterSetter parameter = FindParameter(key, PrimitiveValueType.String);
            if (parameter == null)
            {
                return defaultValue;
            }
            return parameter.StringValue;
        }

        #endregion

        #region IParametersTableListableKeys
        public string[] GetIntKeys()
        {
            return GetKeys(PrimitiveValueType.Integer);
        }
        public string[] GetFloatKeys()
        {
            return GetKeys(PrimitiveValueType.Float);
        }
        public string[] GetBoolKeys()
        {
            return GetKeys(PrimitiveValueType.Bool);
        }
        public string[] GetStringKeys()
        {
            return GetKeys(PrimitiveValueType.String);
        }
        #endregion

        #region IParametersTableSetable
        public void SetBool(string key, bool value)
        {
            ParameterSetter parameterSetter = FindParameter(key, PrimitiveValueType.Bool);
            if (parameterSetter == null)
            {
                parameterSetters.Add(new ParameterSetter()
                {
                    Key = key,
                    PrimitiveValueType = PrimitiveValueType.Bool,
                    BoolValue = value
                });
            }
            else
            {
                parameterSetter.BoolValue = value;
            }
        }

        public void SetFloat(string key, float value)
        {
            ParameterSetter parameterSetter = FindParameter(key, PrimitiveValueType.Float);
            if (parameterSetter == null)
            {
                parameterSetters.Add(new ParameterSetter()
                {
                    Key = key,
                    PrimitiveValueType = PrimitiveValueType.Float,
                    FloatValue = value
                });
            }
            else
            {
                parameterSetter.FloatValue = value;
            }
        }

        public void SetInt(string key, int value)
        {
            ParameterSetter parameterSetter = FindParameter(key, PrimitiveValueType.Integer);
            if (parameterSetter == null)
            {
                parameterSetters.Add(new ParameterSetter()
                {
                    Key = key,
                    PrimitiveValueType = PrimitiveValueType.Integer,
                    IntValue = value
                });
            }
            else
            {
                parameterSetter.IntValue = value;
            }
        }

        public void SetString(string key, string value)
        {
            ParameterSetter parameterSetter = FindParameter(key, PrimitiveValueType.String);
            if (parameterSetter == null)
            {
                parameterSetters.Add(new ParameterSetter()
                {
                    Key = key,
                    PrimitiveValueType = PrimitiveValueType.String,
                    StringValue = value
                });
            }
            else
            {
                parameterSetter.StringValue = value;
            }
        }

        public void Clear()
        {
            parameterSetters.Clear();
        }
        #endregion

        #region Public methods
        public void DispatchChangeEvent()
        {
            if (dispatchEventOnChange)
            {
                DispatchChangeEvent(dispatchEventToChildren, dispatchEventToInActiveChildren);
            }
        }
        #endregion

        #region Private methods
        void DispatchChangeEvent(bool toChidlren, bool toInactiveChildren)
        {
            IParametersTableChangeListener[] listeners = null;
            if (toChidlren)
            {
                listeners = GetComponentsInChildren<IParametersTableChangeListener>(toInactiveChildren);
            }
            else
            {
                listeners = GetComponents<IParametersTableChangeListener>();
            }

            foreach (var item in listeners)
            {
                item.OnParametersTableChanged(this);
            }
        }

        ParameterSetter FindParameter(string key, PrimitiveValueType primitiveValueType)
        {
            for (int i = 0; i < ParameterSetters.Count; i++)
            {
                var parameterSetter = ParameterSetters[i];
                if (parameterSetter.Key == key && parameterSetter.PrimitiveValueType == primitiveValueType)
                {
                    return ParameterSetters[i];
                }
            }

            return null;
        }

        string[] GetKeys(PrimitiveValueType primitiveValueType)
        {
            List<string> keys = new List<string>();
            foreach (var item in ParameterSetters)
            {
                if (item.PrimitiveValueType == primitiveValueType)
                {
                    keys.Add(item.Key);
                }
            }

            return keys.ToArray();
        }

        #endregion


        void Reset()
        {

        }
    }

}